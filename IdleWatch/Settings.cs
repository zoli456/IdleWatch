using System.Diagnostics;
using System.Reflection;
using Microsoft.Win32;
using SharpConfig;
using Vortice.XInput;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace IdleWatch;

internal partial class Settings : Form
{
    internal static readonly string path = Path.GetDirectoryName(Application.ExecutablePath);
    internal static int max_inaktivty = 3600;
    internal static int warning_time = 300;
    internal static readonly string setttings_file_name = "settings.ini";
    internal static readonly Configuration config_parser = Configuration.LoadFromFile($"{path}\\{setttings_file_name}");
    internal static Section Leállítás_section = config_parser["Leállítás"];
    internal static Section Figyelmeztetés_section = config_parser["Figyelmeztetés"];
    internal static Section Indítás_section = config_parser["Indítás"];
    internal static Section Halózat_section = config_parser["Halózat"];
    internal static Section Log_section = config_parser["Log"];
    internal static Section Hang_section = config_parser["Hang"];

    internal static CpuUsageMonitor cpuUsageMonitor;

    private readonly TransparentOverlay overlayform = new();

    private readonly List<Timer_que_item> timer_que = new();

    internal bool ActionAtinactivity, ActionAtTime, ShowWarning, ActionatLowCPUUsage, ActionatNetworkUsage;
    internal bool allowshowdisplay;

    private uint? connectedController;
    internal int cpu_usage, allowed_low_usage, under_cpu_usage_percent;
    private int elozo_pos_x = Cursor.Position.X;
    private int elozo_pos_y = Cursor.Position.Y;
    private DateTime jelenlegi_ido;
    private bool kiírva;
    private int legkorabbi_leallitas;
    internal int masodpercek, masodpercek_cpu, masodpercek_network;
    private Nevjegy nevjegy;
    internal DateTime next_shutdown;
    private State previousControllerState;
    internal string selected_adapter = "";
    internal bool teszt, Hatástalan, Automatikus;
    internal int undernetworkusage, lownetworkusage_time;

    public Settings()
    {
        InitializeComponent();
        Directory.SetCurrentDirectory(path);
        Ertesito_ikon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

        if (File.Exists($"{path}\\{setttings_file_name}"))
        {
            max_inaktivty = Leállítás_section["Inaktivitási_korlát"].IntValue;
            warning_time = Figyelmeztetés_section["Idő"].IntValue;
            teszt = Leállítás_section["Teszt"].BoolValue;
            Hatástalan = Indítás_section["Hatástalan"].BoolValue;
            Automatikus = Indítás_section["Automatikus_indítás"].BoolValue;
            Windows_startup.Checked = Automatikus;
            Ertesito_ikon.Visible = Indítás_section["Menü"].BoolValue;
            ActionAtinactivity = Leállítás_section["Inaktivitáskor"].BoolValue;
            ShowWarning = Figyelmeztetés_section["Bekapcsolva"].BoolValue;
            ActionatLowCPUUsage = Leállítás_section["CPU_használat_alatt"].BoolValue;
            allowed_low_usage = Leállítás_section["Alacsony_CPU_korlát"].IntValue;
            under_cpu_usage_percent = Leállítás_section["Alacsony_CPU_százalék"].IntValue;
            ActionatNetworkUsage = Halózat_section["Bekapcsolva"].BoolValue;
            undernetworkusage = Halózat_section["Korlát"].IntValue;
            lownetworkusage_time = Halózat_section["Idő"].IntValue;

            var networkAdatpers = NetworkUsage.ListAvailableNetworkAdapters();
            for (var i = 0; i < networkAdatpers.Count; i++)
                if (networkAdatpers[i] == Halózat_section["Interface"].StringValue)
                {
                    selected_adapter = networkAdatpers[i];
                    break;
                }

            if (Leállítás_section["CPU_használat_alatt"].BoolValue) cpuUsageMonitor = new CpuUsageMonitor();
        }

        if (Hatástalan) Application.Exit();

        if (Environment.GetCommandLineArgs().Length > 0)
            if (Environment.GetCommandLineArgs().Any(x => x == "auto"))
                return;
        Update_form();
        allowshowdisplay = true;
        Show();
    }

    private void Settings_Shown(object sender, EventArgs e)
    {
        Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
    }

    private void Update_form()
    {
        lastshutdown_label.Text = lastshutdown_label.Text.Split(':')[0];
        lastwarning_label.Text = lastwarning_label.Text.Split(':')[0];
        Warningtime_textbox.Text = warning_time.ToString();
        shutdowntime_textbox.Text = max_inaktivty.ToString();
        startwithwindow_checkBox.Checked = Automatikus;
        notificationicon_checkBox.Checked = Indítás_section["Menü"].BoolValue;
        lastshutdown_label.Text =
            $"{lastshutdown_label.Text}: {Log_section["Utolsó_leállítás"].StringValue}";
        lastwarning_label.Text =
            $"{lastwarning_label.Text}: {Log_section["Utolsó_figyelmeztetés"].StringValue}";
        AtInactivity_checkBox.Checked = ActionAtinactivity;
        showwarning_checkBox.Checked = ShowWarning;
        cpu_usage_checkBox.Checked = ActionatLowCPUUsage;
        cpu_textBox.Text = allowed_low_usage.ToString();
        cpu_trackBar.Value = under_cpu_usage_percent;
        cpupercent_label.Text = $"{cpu_trackBar.Value}%";
        lownetworkusage_checkBox.Checked = ActionatNetworkUsage;
        interfaces_newComboBox.SelectedIndex =
            interfaces_newComboBox.FindStringExact(Halózat_section["Interface"].StringValue);
        bandwith_textBox.Text = undernetworkusage.ToString();
        lownetwork_textBox.Text = lownetworkusage_time.ToString();

        Volume_trackBar.Value = Hang_section["Hangerő"].IntValue;
        SoundcheckBox.Checked = Hang_section["Bekapcsolva"].BoolValue;
        Volumelabel.Text = $"{Volume_trackBar.Value}%";
        update_gui();
    }

    internal void change_progresbar(int value)
    {
        SafeInvoke(cpu_progressBar, () => cpu_progressBar.Value = value);
    }

    internal void change_current_cpu_progresbar(int value)
    {
        SafeInvoke(current_cpu_label, () => current_cpu_label.Text = $"{value}%");
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (Tools.Is_higher_than_one(Warningtime_textbox.Text.Trim()))
                warning_time = int.Parse(Warningtime_textbox.Text.Trim());
            else
                throw new Exception();

            if (Tools.Is_higher_than_one(shutdowntime_textbox.Text.Trim()))
                max_inaktivty = int.Parse(shutdowntime_textbox.Text.Trim());
            else
                throw new Exception();

            if (AtTime_checkBox.Checked)
            {
                if (Tools.Is_higher_than_one(AtTime_textBox.Text.Trim()))
                    next_shutdown = DateTime.Now.AddMinutes(int.Parse(AtTime_textBox.Text.Trim()));
                else
                    throw new Exception();
            }

            if (Tools.Is_higher_than_one(cpu_textBox.Text.Trim()))
                allowed_low_usage = int.Parse(cpu_textBox.Text.Trim());
            else
                throw new Exception();

            if (Tools.Is_higher_than_one(bandwith_textBox.Text.Trim()))
                undernetworkusage = int.Parse(bandwith_textBox.Text.Trim());
            else
                throw new Exception();

            if (Tools.Is_higher_than_one(lownetwork_textBox.Text.Trim()))
                lownetworkusage_time = int.Parse(lownetwork_textBox.Text.Trim());
            else
                throw new Exception();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Hibás adatokat adtál meg!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Figyelmeztetés_section["Bekapcsolva"].BoolValue = showwarning_checkBox.Checked;
        Figyelmeztetés_section["Idő"].IntValue = int.Parse(Warningtime_textbox.Text.Trim());
        Indítás_section["Automatikus_indítás"].BoolValue = startwithwindow_checkBox.Checked;
        Indítás_section["Menü"].BoolValue = notificationicon_checkBox.Checked;
        Leállítás_section["Inaktivitási_korlát"].IntValue =
            int.Parse(shutdowntime_textbox.Text.Trim());
        Leállítás_section["Inaktivitáskor"].BoolValue = AtInactivity_checkBox.Checked;
        Leállítás_section["CPU_használat_alatt"].BoolValue = cpu_usage_checkBox.Checked;
        Leállítás_section["Alacsony_CPU_korlát"].IntValue = int.Parse(cpu_textBox.Text.Trim());
        Leállítás_section["Alacsony_CPU_százalék"].IntValue = cpu_trackBar.Value;
        Halózat_section["Bekapcsolva"].BoolValue = lownetworkusage_checkBox.Checked;
        Halózat_section["Interface"].StringValue = interfaces_newComboBox.Text;
        Halózat_section["Korlát"].IntValue = int.Parse(bandwith_textBox.Text.Trim());
        Halózat_section["Idő"].IntValue = int.Parse(lownetwork_textBox.Text.Trim());
        Hang_section["Bekapcsolva"].BoolValue = SoundcheckBox.Checked;
        Hang_section["Hangerő"].IntValue = Volume_trackBar.Value;
        config_parser.SaveToFile(
            $"{path}\\{setttings_file_name}");
        Ertesito_ikon.Visible = notificationicon_checkBox.Checked;
        ActionAtinactivity = AtInactivity_checkBox.Checked;
        ActionAtTime = AtTime_checkBox.Checked;
        ShowWarning = showwarning_checkBox.Checked;
        Automatikus = startwithwindow_checkBox.Checked;
        ActionatLowCPUUsage = cpu_usage_checkBox.Checked;

        under_cpu_usage_percent = cpu_trackBar.Value;
        masodpercek_cpu = 0;
        masodpercek = 0;
        masodpercek_network = 0;

        undernetworkusage = Halózat_section["Korlát"].IntValue;
        lownetworkusage_time = Halózat_section["Idő"].IntValue;
        ActionatNetworkUsage = lownetworkusage_checkBox.Checked;

        Windows_startup.Checked = startwithwindow_checkBox.Checked;

        if (ActionatNetworkUsage)
        {
            selected_adapter = interfaces_newComboBox.SelectedItem.ToString();
        }
        else
        {
            selected_adapter = "";
            interfaces_newComboBox.SelectedIndex = -1;
            interfaces_newComboBox.ResetText();
        }

        if (cpu_usage_checkBox.Checked)
        {
            if (cpuUsageMonitor == null) cpuUsageMonitor = new CpuUsageMonitor();
        }
        else
        {
            cpuUsageMonitor = null;
        }

        SetStartup();
        /*allowshowdisplay = false;
        Hide();*/
    }

    private void Settings_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            allowshowdisplay = false;
            Hide();
            e.Cancel = true;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void update_gui()
    {
        testmode_label.Visible = teszt;
        label1.Enabled = showwarning_checkBox.Checked;
        Warningtime_textbox.Enabled = showwarning_checkBox.Checked;
        label3.Enabled = showwarning_checkBox.Checked;
        label2.Enabled = AtInactivity_checkBox.Checked;
        shutdowntime_textbox.Enabled = AtInactivity_checkBox.Checked;
        label4.Enabled = AtInactivity_checkBox.Checked;
        label6.Enabled = AtTime_checkBox.Checked;
        AtTime_textBox.Enabled = AtTime_checkBox.Checked;
        label5.Enabled = AtTime_checkBox.Checked;

        cpu_progressBar.Enabled = cpu_usage_checkBox.Checked;
        cpu_trackBar.Enabled = cpu_usage_checkBox.Checked;
        label8.Enabled = cpu_usage_checkBox.Checked;
        cpu_textBox.Enabled = cpu_usage_checkBox.Checked;
        label7.Enabled = cpu_usage_checkBox.Checked;
        current_cpu_label.Enabled = cpu_usage_checkBox.Checked;
        cpupercent_label.Enabled = cpu_usage_checkBox.Checked;


        networkusage_label.Enabled = lownetworkusage_checkBox.Checked;
        interfaces_newComboBox.Enabled = lownetworkusage_checkBox.Checked;
        label9.Enabled = lownetworkusage_checkBox.Checked;
        label10.Enabled = lownetworkusage_checkBox.Checked;
        label11.Enabled = lownetworkusage_checkBox.Checked;
        label12.Enabled = lownetworkusage_checkBox.Checked;
        bandwith_textBox.Enabled = lownetworkusage_checkBox.Checked;
        lownetwork_textBox.Enabled = lownetworkusage_checkBox.Checked;

        Volume_trackBar.Enabled = showwarning_checkBox.Checked;
        Testbutton.Enabled = showwarning_checkBox.Checked;
        SoundcheckBox.Enabled = showwarning_checkBox.Checked;
        Soundlabel.Enabled = showwarning_checkBox.Checked;
        Volumelabel.Enabled = showwarning_checkBox.Checked;
    }

    private void AtInactivity_checkBox_CheckedChanged(object sender, EventArgs e)
    {
        update_gui();
    }

    private void AtTime_checkBox_CheckedChanged(object sender, EventArgs e)
    {
        update_gui();
    }

    private void showwarning_checkBox_CheckedChanged(object sender, EventArgs e)
    {
        update_gui();
    }

    private void cpu_usage_checkBox_CheckedChanged(object sender, EventArgs e)
    {
        update_gui();
    }

    private void cpu_trackBar_Scroll(object sender, EventArgs e)
    {
        cpupercent_label.Text = $"{cpu_trackBar.Value.ToString()}%";
    }

    private void lownetworkusage_checkBox_CheckedChanged(object sender, EventArgs e)
    {
        update_gui();
        interfaces_newComboBox.Items.Clear();
        foreach (var a in NetworkUsage.ListAvailableNetworkAdapters()) interfaces_newComboBox.Items.Add(a);
    }

    internal void changenetwork_usage(double value)
    {
        SafeInvoke(networkusage_label,
            () => networkusage_label.Text = $"{networkusage_label.Text.Split(':')[0]}: {Math.Truncate(value)} Kbps");
    }

    internal void change_title(int timer)
    {
        Text = $"Beállítások - Leállítás {timer} mp múlva";
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (nevjegy == null || nevjegy.IsDisposed) nevjegy = new Nevjegy();

        nevjegy.Show();
    }

    public static bool SafeInvoke(Control control, MethodInvoker method)
    {
        if (control != null && !control.IsDisposed && control.IsHandleCreated && control.FindForm().IsHandleCreated)
        {
            if (control.InvokeRequired)
                control.Invoke(method);
            else
                method();

            return true;
        }

        return false;
    }

    private void szamlalo_Tick(object sender, EventArgs e)
    {
        if (!overlayform.isVisible) kiírva = false;

        jelenlegi_ido = DateTime.Now;


        if (ActionAtinactivity)
        {
            // Detect the first connected XInput controller
            for (var index = 0; index < 4; index++) // XInput supports indices 0-3
                if (XInput.GetCapabilities((uint)index, DeviceQueryType.Any, out _))
                {
                    connectedController = (uint)index;
                    break;
                }

            // Check if a controller was detected
            if (connectedController != null)
            {
                Controller_timer.Enabled = true;
                Debug.WriteLine($"Controller {connectedController} is connected.");
            }
            else
            {
                Debug.WriteLine("No XInput controller is connected.");
                Controller_timer.Enabled = false;
            }

            // Check inactivity (mouse movement)
            var cursorMoved = Math.Abs(elozo_pos_x - Cursor.Position.X) > 10 ||
                              Math.Abs(elozo_pos_y - Cursor.Position.Y) > 10;
            masodpercek = cursorMoved ? 0 : masodpercek + 1;

            Debug.WriteLine(cursorMoved ? "" : $"Inaktivitás {masodpercek} mp óta.");

            elozo_pos_x = Cursor.Position.X;
            elozo_pos_y = Cursor.Position.Y;
        }

        if (ActionatLowCPUUsage)
            Task.Run(() =>
            {
                cpu_usage = Convert.ToInt32(CpuUsageMonitor.GetCpuUsage());
                change_progresbar(cpu_usage);
                change_current_cpu_progresbar(cpu_usage);

                masodpercek_cpu = cpu_usage > under_cpu_usage_percent ? 0 : masodpercek_cpu + 1;
                Debug.WriteLine(cpu_usage > under_cpu_usage_percent
                    ? ""
                    : $"Alacsony CPU használat {masodpercek_cpu} mp óta.");
            });

        if (ActionatNetworkUsage && selected_adapter != "")
            Task.Run(() =>
            {
                var temp = NetworkUsage.GetNetworkDownloadSpeedKbps(selected_adapter);

                masodpercek_network = temp > undernetworkusage ? 0 : masodpercek_network + 1;
                changenetwork_usage(temp);
                Debug.WriteLine(temp > undernetworkusage
                    ? ""
                    : $"Alacsony hálózathasználat {masodpercek_network} mp óta.");
            });

        Action_Que();
    }

    private void Action_Que()
    {
        timer_que.Clear();
        if (ActionAtTime)
            timer_que.Add(new Timer_que_item((int)Math.Truncate((next_shutdown - jelenlegi_ido).TotalSeconds),
                Action_At_Time));
        if (ActionAtinactivity) timer_que.Add(new Timer_que_item(max_inaktivty - masodpercek, Action_At_Inactivity));
        if (ActionatLowCPUUsage)
            timer_que.Add(new Timer_que_item(allowed_low_usage - masodpercek_cpu, Action_At_lowCPU));
        if (ActionatNetworkUsage)
            timer_que.Add(new Timer_que_item(lownetworkusage_time - masodpercek_network, Action_At_LowNetwork));
        if (timer_que.Count > 1)
        {
            legkorabbi_leallitas = timer_que.Min(x => x.time);
            foreach (var tqi in timer_que)
                if (tqi.time == legkorabbi_leallitas)
                {
                    change_title(legkorabbi_leallitas);
                    tqi.action.DynamicInvoke();
                    break;
                }
        }
        else
        {
            if (timer_que.Count != 0)
            {
                change_title(timer_que[0].time);
                timer_que[0].action.DynamicInvoke();
            }
            else
            {
                Text = "Beállítások";
            }
        }
    }

    private void shutdown_computer()
    {
        Log_section["Utolsó_leállítás"].StringValue = DateTime.Now.ToString();
        config_parser.SaveToFile($"{path}\\{setttings_file_name}");
        var startInfo = new ProcessStartInfo();
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.FileName = "cmd";
        startInfo.Arguments = "/C shutdown -f -s -t 0";
        Process.Start(startInfo);
    }

    protected override void SetVisibleCore(bool value)
    {
        base.SetVisibleCore(allowshowdisplay ? value : allowshowdisplay);
    }

    internal void SetStartup()
    {
        var rk = Registry.CurrentUser.OpenSubKey
            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        if (Automatikus)
            rk.SetValue(Assembly.GetExecutingAssembly().GetName().Name, $"{Application.ExecutablePath} auto");
        else
            rk.DeleteValue(Assembly.GetExecutingAssembly().GetName().Name, false);
        rk.Dispose();
    }

    private void ShowOverlay(OverlayMode mode)
    {
        switch (mode)
        {
            case OverlayMode.inactivity:
            {
                overlayform.SetOverlayText("Inaktivitási figyelmeztetés!",
                    $"A rendszer leáll {max_inaktivty - masodpercek} mp múlva.");
                break;
            }
            case OverlayMode.scheduled:
            {
                overlayform.SetOverlayText("Ütemezett leállítás!",
                    $"A rendszer leáll {Math.Truncate((next_shutdown - jelenlegi_ido).TotalSeconds)} mp múlva.");
                break;
            }
            case OverlayMode.lowcpu:
            {
                overlayform.SetOverlayText("Alacsony CPU használat!",
                    $"A rendszer leáll {allowed_low_usage - masodpercek_cpu} mp múlva.");
                break;
            }
            case OverlayMode.lownetwork:
            {
                overlayform.SetOverlayText("Alacsony halózathasználat!",
                    $"A rendszer leáll {lownetworkusage_time - masodpercek_network} mp múlva.");
                break;
            }
        }

        overlayform.ShowOverlay();
        Testbutton.Enabled = false;
        if (Hang_section["Bekapcsolva"].BoolValue && !AudioPlayer.Looping)
            Task.Run(AudioPlayer.PlayAlarmLoop);
    }

    private void HideOverlay()
    {
        AudioPlayer.StopAlarmLoop();
        if (showwarning_checkBox.Checked)
            Testbutton.Enabled = true;
        overlayform.HideOverlay();
    }

    private void Action_At_Time()
    {
        if (warning_time < (next_shutdown - jelenlegi_ido).TotalSeconds)
        {
            HideOverlay();
        }
        else
        {
            if (ShowWarning)
            {
                ShowOverlay(OverlayMode.scheduled);
                if (!kiírva)
                {
                    Debug.WriteLine("Visszaszámlálás megjelenítve.");
                    kiírva = true;
                    Log_section["Utolsó_figyelmeztetés"].StringValue = DateTime.Now.ToString();
                    config_parser.SaveToFile($"{path}\\{setttings_file_name}");
                }
            }
        }

        if (jelenlegi_ido > next_shutdown)
        {
            Debug.WriteLine("Leállítás elindítva egy bizonyos időben.");
            if (!teszt) shutdown_computer();
        }
    }

    private void Action_At_Inactivity()
    {
        if (masodpercek < max_inaktivty - warning_time)
        {
            HideOverlay();
        }

        else
        {
            if (ShowWarning)
            {
                ShowOverlay(OverlayMode.inactivity);
                if (!kiírva)
                {
                    Debug.WriteLine("Visszaszámlálás megjelenítve inaktitivás miatt.");
                    kiírva = true;
                    Log_section["Utolsó_figyelmeztetés"].StringValue = DateTime.Now.ToString();
                    config_parser.SaveToFile($"{path}\\{setttings_file_name}");
                }
            }
        }

        if (max_inaktivty <= masodpercek)
        {
            Debug.WriteLine("Leállítás elindítva inaktivitás után.");
            if (!teszt) shutdown_computer();
        }
    }

    private void Action_At_lowCPU()
    {
        if (masodpercek_cpu <= allowed_low_usage - warning_time)
        {
            HideOverlay();
        }
        else
        {
            if (ShowWarning)
            {
                ShowOverlay(OverlayMode.lowcpu);
                if (!kiírva)
                {
                    Debug.WriteLine("Visszaszámlálás megjelenítve alacsony cpu használat miatt.");
                    kiírva = true;
                    Log_section["Utolsó_figyelmeztetés"].StringValue = DateTime.Now.ToString();
                    config_parser.SaveToFile($"{path}\\{setttings_file_name}");
                }
            }
        }

        if (max_inaktivty <= masodpercek_cpu)
        {
            Debug.WriteLine("Leállítás elindítva inaktivitás után.");
            if (!teszt) shutdown_computer();
        }
    }

    private void Action_At_LowNetwork()
    {
        if (masodpercek_network <= lownetworkusage_time - warning_time)
        {
            HideOverlay();
        }

        else
        {
            if (ShowWarning)
            {
                ShowOverlay(OverlayMode.lownetwork);
                if (!kiírva)
                {
                    Debug.WriteLine("Visszaszámlálás megjelenítve alacsony hálózathasználat miatt.");
                    kiírva = true;
                    Log_section["Utolsó_figyelmeztetés"].StringValue = DateTime.Now.ToString();
                    config_parser.SaveToFile($"{path}\\{setttings_file_name}");
                }
            }
        }

        if (lownetworkusage_time <= masodpercek_network)
        {
            Debug.WriteLine("Leállítás elindítva alacsony hálózathasználat után.");
            if (!teszt) shutdown_computer();
        }
    }

    private void Controller_timer_Tick_1(object sender, EventArgs e)
    {
        if (connectedController == null)
            return;

        if (XInput.GetState((uint)connectedController, out var controllerState))
        {
            if (previousControllerState.PacketNumber != controllerState.PacketNumber)
            {
                Debug.WriteLine(controllerState.Gamepad);
                masodpercek = 0;
            }

            previousControllerState = controllerState;
        }
    }

    private void ApplicationExit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void openini_menustrip_Click(object sender, EventArgs e)
    {
        new Process
        {
            StartInfo = new ProcessStartInfo($"{path}\\{setttings_file_name}")
            {
                UseShellExecute = true
            }
        }.Start();
    }

    private void Setttings_menustrip_Click(object sender, EventArgs e)
    {
        allowshowdisplay = true;
        Show();
        Update_form();
    }

    private void Windows_startup_Click(object sender, EventArgs e)
    {
        Windows_startup.Checked = !Windows_startup.Checked;
        Automatikus = Windows_startup.Checked;
        Indítás_section["Automatikus_indítás"].BoolValue = Windows_startup.Checked;
        config_parser.SaveToFile($"{path}\\{setttings_file_name}");
        SetStartup();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        Task.Run(async () =>
            {
                if (Testbutton.Text == "Teszt")
                {
                    SafeInvoke(Testbutton, () => Testbutton.Text = "Megállít");
                    SafeInvoke(this, () => Refresh());
                    await AudioPlayer.PlayAlarmSingle();
                    SafeInvoke(Testbutton, () => Testbutton.Text = "Teszt");
                }
                else
                {
                    AudioPlayer.waveOut.Stop();
                    AudioPlayer.waveOut.Dispose();
                    AudioPlayer.mp3Reader.Dispose();
                    SafeInvoke(Testbutton, () => Testbutton.Text = "Teszt");
                }
            }
        );
    }

    private void Volume_trackBar_Scroll(object sender, EventArgs e)
    {
        Volumelabel.Text = $"{Volume_trackBar.Value}%";
    }

    internal enum OverlayMode
    {
        inactivity,
        scheduled,
        lowcpu,
        lownetwork
    }
}

internal class Timer_que_item
{
    internal Action action;
    internal int time;

    public Timer_que_item(int time, Action action)
    {
        this.time = time;
        this.action = action;
    }
}