namespace IdleWatch
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            lastshutdown_label = new Label();
            lastwarning_label = new Label();
            startwithwindow_checkBox = new CheckBox();
            notificationicon_checkBox = new CheckBox();
            SaveButton = new Button();
            Warningtime_textbox = new TextBox();
            shutdowntime_textbox = new TextBox();
            button1 = new Button();
            label3 = new Label();
            label4 = new Label();
            AtInactivity_checkBox = new CheckBox();
            AtTime_checkBox = new CheckBox();
            label5 = new Label();
            AtTime_textBox = new TextBox();
            label6 = new Label();
            showwarning_checkBox = new CheckBox();
            cpu_progressBar = new ProgressBar();
            cpu_trackBar = new TrackBar();
            cpu_usage_checkBox = new CheckBox();
            label7 = new Label();
            cpu_textBox = new TextBox();
            label8 = new Label();
            cpupercent_label = new Label();
            current_cpu_label = new Label();
            lownetworkusage_checkBox = new CheckBox();
            networkusage_label = new Label();
            testmode_label = new Label();
            interfaces_newComboBox = new NewComboBox();
            label9 = new Label();
            bandwith_textBox = new TextBox();
            label10 = new Label();
            label11 = new Label();
            lownetwork_textBox = new TextBox();
            label12 = new Label();
            button2 = new Button();
            Ertesito_ikon = new NotifyIcon(components);
            Kis_menu = new ContextMenuStrip(components);
            Windows_startup = new ToolStripMenuItem();
            Setttings_menustrip = new ToolStripMenuItem();
            openini_menustrip = new ToolStripMenuItem();
            ApplicationExit = new ToolStripMenuItem();
            szamlalo = new System.Windows.Forms.Timer(components);
            Controller_timer = new System.Windows.Forms.Timer(components);
            SoundcheckBox = new CheckBox();
            Volume_trackBar = new TrackBar();
            Testbutton = new Button();
            Soundlabel = new Label();
            Volumelabel = new Label();
            ((System.ComponentModel.ISupportInitialize)cpu_trackBar).BeginInit();
            Kis_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Volume_trackBar).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label1.Location = new Point(151, 189);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 0;
            label1.Text = "Leállítás előtt";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label2.Location = new Point(130, 47);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(90, 15);
            label2.TabIndex = 1;
            label2.Text = "Leállítási idő";
            // 
            // lastshutdown_label
            // 
            lastshutdown_label.AutoSize = true;
            lastshutdown_label.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lastshutdown_label.Location = new Point(5, 531);
            lastshutdown_label.Margin = new Padding(4, 0, 4, 0);
            lastshutdown_label.Name = "lastshutdown_label";
            lastshutdown_label.Size = new Size(143, 15);
            lastshutdown_label.TabIndex = 2;
            lastshutdown_label.Text = "Utolsó leállítás ideje:";
            // 
            // lastwarning_label
            // 
            lastwarning_label.AutoSize = true;
            lastwarning_label.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lastwarning_label.Location = new Point(5, 559);
            lastwarning_label.Margin = new Padding(4, 0, 4, 0);
            lastwarning_label.Name = "lastwarning_label";
            lastwarning_label.Size = new Size(184, 15);
            lastwarning_label.TabIndex = 3;
            lastwarning_label.Text = "Utolsó figyelmeztetés ideje:";
            // 
            // startwithwindow_checkBox
            // 
            startwithwindow_checkBox.AutoSize = true;
            startwithwindow_checkBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            startwithwindow_checkBox.Location = new Point(5, 509);
            startwithwindow_checkBox.Margin = new Padding(4, 3, 4, 3);
            startwithwindow_checkBox.Name = "startwithwindow_checkBox";
            startwithwindow_checkBox.Size = new Size(158, 19);
            startwithwindow_checkBox.TabIndex = 4;
            startwithwindow_checkBox.Text = "Indítás a rendszerrel";
            startwithwindow_checkBox.UseVisualStyleBackColor = true;
            // 
            // notificationicon_checkBox
            // 
            notificationicon_checkBox.AutoSize = true;
            notificationicon_checkBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            notificationicon_checkBox.Location = new Point(370, 509);
            notificationicon_checkBox.Margin = new Padding(4, 3, 4, 3);
            notificationicon_checkBox.Name = "notificationicon_checkBox";
            notificationicon_checkBox.Size = new Size(92, 19);
            notificationicon_checkBox.TabIndex = 5;
            notificationicon_checkBox.Text = "Tálca ikon";
            notificationicon_checkBox.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(5, 593);
            SaveButton.Margin = new Padding(4, 3, 4, 3);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(88, 27);
            SaveButton.TabIndex = 6;
            SaveButton.Text = "Mentés";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // Warningtime_textbox
            // 
            Warningtime_textbox.Location = new Point(253, 187);
            Warningtime_textbox.Margin = new Padding(4, 3, 4, 3);
            Warningtime_textbox.MaxLength = 8;
            Warningtime_textbox.Name = "Warningtime_textbox";
            Warningtime_textbox.Size = new Size(178, 23);
            Warningtime_textbox.TabIndex = 7;
            Warningtime_textbox.TextAlign = HorizontalAlignment.Center;
            // 
            // shutdowntime_textbox
            // 
            shutdowntime_textbox.Location = new Point(237, 40);
            shutdowntime_textbox.Margin = new Padding(4, 3, 4, 3);
            shutdowntime_textbox.MaxLength = 8;
            shutdowntime_textbox.Name = "shutdowntime_textbox";
            shutdowntime_textbox.Size = new Size(178, 23);
            shutdowntime_textbox.TabIndex = 8;
            shutdowntime_textbox.TextAlign = HorizontalAlignment.Center;
            // 
            // button1
            // 
            button1.Location = new Point(381, 592);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(81, 25);
            button1.TabIndex = 9;
            button1.Text = "Leállítás";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label3.Location = new Point(435, 189);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(27, 15);
            label3.TabIndex = 10;
            label3.Text = "mp";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label4.Location = new Point(418, 47);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(27, 15);
            label4.TabIndex = 11;
            label4.Text = "mp";
            // 
            // AtInactivity_checkBox
            // 
            AtInactivity_checkBox.AutoSize = true;
            AtInactivity_checkBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            AtInactivity_checkBox.Location = new Point(14, 14);
            AtInactivity_checkBox.Margin = new Padding(4, 3, 4, 3);
            AtInactivity_checkBox.Name = "AtInactivity_checkBox";
            AtInactivity_checkBox.Size = new Size(181, 19);
            AtInactivity_checkBox.TabIndex = 12;
            AtInactivity_checkBox.Text = "Leállítás inaktivitás után";
            AtInactivity_checkBox.UseVisualStyleBackColor = true;
            AtInactivity_checkBox.CheckedChanged += AtInactivity_checkBox_CheckedChanged;
            // 
            // AtTime_checkBox
            // 
            AtTime_checkBox.AutoSize = true;
            AtTime_checkBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            AtTime_checkBox.Location = new Point(14, 73);
            AtTime_checkBox.Margin = new Padding(4, 3, 4, 3);
            AtTime_checkBox.Name = "AtTime_checkBox";
            AtTime_checkBox.Size = new Size(163, 19);
            AtTime_checkBox.TabIndex = 13;
            AtTime_checkBox.Text = "Leállítás egy idő után";
            AtTime_checkBox.UseVisualStyleBackColor = true;
            AtTime_checkBox.CheckedChanged += AtTime_checkBox_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label5.Location = new Point(418, 100);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(35, 15);
            label5.TabIndex = 16;
            label5.Text = "perc";
            // 
            // AtTime_textBox
            // 
            AtTime_textBox.Location = new Point(232, 96);
            AtTime_textBox.Margin = new Padding(4, 3, 4, 3);
            AtTime_textBox.MaxLength = 8;
            AtTime_textBox.Name = "AtTime_textBox";
            AtTime_textBox.Size = new Size(178, 23);
            AtTime_textBox.TabIndex = 15;
            AtTime_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label6.Location = new Point(151, 104);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(69, 15);
            label6.TabIndex = 14;
            label6.Text = "Idő múlva";
            // 
            // showwarning_checkBox
            // 
            showwarning_checkBox.AutoSize = true;
            showwarning_checkBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            showwarning_checkBox.Location = new Point(15, 152);
            showwarning_checkBox.Margin = new Padding(4, 3, 4, 3);
            showwarning_checkBox.Name = "showwarning_checkBox";
            showwarning_checkBox.Size = new Size(217, 19);
            showwarning_checkBox.TabIndex = 17;
            showwarning_checkBox.Text = "Figyelmeztetés megjelenítése";
            showwarning_checkBox.UseVisualStyleBackColor = true;
            showwarning_checkBox.CheckedChanged += showwarning_checkBox_CheckedChanged;
            // 
            // cpu_progressBar
            // 
            cpu_progressBar.Location = new Point(50, 293);
            cpu_progressBar.Margin = new Padding(4, 3, 4, 3);
            cpu_progressBar.Name = "cpu_progressBar";
            cpu_progressBar.Size = new Size(336, 14);
            cpu_progressBar.TabIndex = 19;
            // 
            // cpu_trackBar
            // 
            cpu_trackBar.Location = new Point(93, 314);
            cpu_trackBar.Margin = new Padding(4, 3, 4, 3);
            cpu_trackBar.Maximum = 100;
            cpu_trackBar.Minimum = 1;
            cpu_trackBar.Name = "cpu_trackBar";
            cpu_trackBar.Size = new Size(369, 45);
            cpu_trackBar.TabIndex = 20;
            cpu_trackBar.Value = 1;
            cpu_trackBar.Scroll += cpu_trackBar_Scroll;
            // 
            // cpu_usage_checkBox
            // 
            cpu_usage_checkBox.AutoSize = true;
            cpu_usage_checkBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            cpu_usage_checkBox.Location = new Point(14, 264);
            cpu_usage_checkBox.Margin = new Padding(4, 3, 4, 3);
            cpu_usage_checkBox.Name = "cpu_usage_checkBox";
            cpu_usage_checkBox.Size = new Size(152, 19);
            cpu_usage_checkBox.TabIndex = 21;
            cpu_usage_checkBox.Text = "CPU használat alatt";
            cpu_usage_checkBox.UseVisualStyleBackColor = true;
            cpu_usage_checkBox.CheckedChanged += cpu_usage_checkBox_CheckedChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label7.Location = new Point(426, 362);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(27, 15);
            label7.TabIndex = 24;
            label7.Text = "mp";
            // 
            // cpu_textBox
            // 
            cpu_textBox.Location = new Point(240, 360);
            cpu_textBox.Margin = new Padding(4, 3, 4, 3);
            cpu_textBox.MaxLength = 8;
            cpu_textBox.Name = "cpu_textBox";
            cpu_textBox.Size = new Size(178, 23);
            cpu_textBox.TabIndex = 23;
            cpu_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label8.Location = new Point(173, 368);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(59, 15);
            label8.TabIndex = 22;
            label8.Text = "Idő után";
            // 
            // cpupercent_label
            // 
            cpupercent_label.AutoSize = true;
            cpupercent_label.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            cpupercent_label.Location = new Point(35, 326);
            cpupercent_label.Margin = new Padding(4, 0, 4, 0);
            cpupercent_label.Name = "cpupercent_label";
            cpupercent_label.Size = new Size(50, 15);
            cpupercent_label.TabIndex = 25;
            cpupercent_label.Text = "XXX %";
            // 
            // current_cpu_label
            // 
            current_cpu_label.AutoSize = true;
            current_cpu_label.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            current_cpu_label.Location = new Point(394, 293);
            current_cpu_label.Margin = new Padding(4, 0, 4, 0);
            current_cpu_label.Name = "current_cpu_label";
            current_cpu_label.Size = new Size(50, 15);
            current_cpu_label.TabIndex = 26;
            current_cpu_label.Text = "XXX %";
            // 
            // lownetworkusage_checkBox
            // 
            lownetworkusage_checkBox.AutoSize = true;
            lownetworkusage_checkBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lownetworkusage_checkBox.Location = new Point(10, 386);
            lownetworkusage_checkBox.Margin = new Padding(4, 3, 4, 3);
            lownetworkusage_checkBox.Name = "lownetworkusage_checkBox";
            lownetworkusage_checkBox.Size = new Size(173, 19);
            lownetworkusage_checkBox.TabIndex = 28;
            lownetworkusage_checkBox.Text = "Halózat használat alatt";
            lownetworkusage_checkBox.UseVisualStyleBackColor = true;
            lownetworkusage_checkBox.CheckedChanged += lownetworkusage_checkBox_CheckedChanged;
            // 
            // networkusage_label
            // 
            networkusage_label.AutoSize = true;
            networkusage_label.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            networkusage_label.Location = new Point(12, 411);
            networkusage_label.Margin = new Padding(4, 0, 4, 0);
            networkusage_label.Name = "networkusage_label";
            networkusage_label.Size = new Size(143, 15);
            networkusage_label.TabIndex = 30;
            networkusage_label.Text = "Jelenlegi: XXXX KB/s";
            // 
            // testmode_label
            // 
            testmode_label.AutoSize = true;
            testmode_label.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            testmode_label.ForeColor = Color.FromArgb(0, 192, 0);
            testmode_label.Location = new Point(200, 598);
            testmode_label.Margin = new Padding(4, 0, 4, 0);
            testmode_label.Name = "testmode_label";
            testmode_label.Size = new Size(106, 15);
            testmode_label.TabIndex = 31;
            testmode_label.Text = "Teszt mód aktív";
            // 
            // interfaces_newComboBox
            // 
            interfaces_newComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            interfaces_newComboBox.FormattingEnabled = true;
            interfaces_newComboBox.Location = new Point(202, 410);
            interfaces_newComboBox.Margin = new Padding(4, 3, 4, 3);
            interfaces_newComboBox.Name = "interfaces_newComboBox";
            interfaces_newComboBox.Size = new Size(260, 24);
            interfaces_newComboBox.TabIndex = 27;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label9.Location = new Point(426, 441);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(36, 15);
            label9.TabIndex = 34;
            label9.Text = "KB/s";
            // 
            // bandwith_textBox
            // 
            bandwith_textBox.Location = new Point(200, 439);
            bandwith_textBox.Margin = new Padding(4, 3, 4, 3);
            bandwith_textBox.MaxLength = 8;
            bandwith_textBox.Name = "bandwith_textBox";
            bandwith_textBox.Size = new Size(218, 23);
            bandwith_textBox.TabIndex = 33;
            bandwith_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label10.Location = new Point(42, 441);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(124, 15);
            label10.TabIndex = 32;
            label10.Text = "Adatforgalom alatt";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label11.Location = new Point(426, 474);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(27, 15);
            label11.TabIndex = 37;
            label11.Text = "mp";
            // 
            // lownetwork_textBox
            // 
            lownetwork_textBox.Location = new Point(200, 474);
            lownetwork_textBox.Margin = new Padding(4, 3, 4, 3);
            lownetwork_textBox.MaxLength = 8;
            lownetwork_textBox.Name = "lownetwork_textBox";
            lownetwork_textBox.Size = new Size(218, 23);
            lownetwork_textBox.TabIndex = 36;
            lownetwork_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label12.Location = new Point(110, 476);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(59, 15);
            label12.TabIndex = 35;
            label12.Text = "Idő után";
            // 
            // button2
            // 
            button2.Location = new Point(439, 10);
            button2.Name = "button2";
            button2.Size = new Size(24, 23);
            button2.TabIndex = 38;
            button2.Text = "?";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Ertesito_ikon
            // 
            Ertesito_ikon.ContextMenuStrip = Kis_menu;
            Ertesito_ikon.Text = "IdleWatch";
            // 
            // Kis_menu
            // 
            Kis_menu.Items.AddRange(new ToolStripItem[] { Windows_startup, Setttings_menustrip, openini_menustrip, ApplicationExit });
            Kis_menu.Name = "Kis_menu";
            Kis_menu.Size = new Size(185, 92);
            Kis_menu.Text = "IdleWatch";
            // 
            // Windows_startup
            // 
            Windows_startup.Name = "Windows_startup";
            Windows_startup.Size = new Size(184, 22);
            Windows_startup.Text = "Indítás a Windowssal";
            Windows_startup.Click += Windows_startup_Click;
            // 
            // Setttings_menustrip
            // 
            Setttings_menustrip.Name = "Setttings_menustrip";
            Setttings_menustrip.Size = new Size(184, 22);
            Setttings_menustrip.Text = "Beállítások";
            Setttings_menustrip.Click += Setttings_menustrip_Click;
            // 
            // openini_menustrip
            // 
            openini_menustrip.Name = "openini_menustrip";
            openini_menustrip.Size = new Size(184, 22);
            openini_menustrip.Text = "Ini fájl megnyitása";
            openini_menustrip.Click += openini_menustrip_Click;
            // 
            // ApplicationExit
            // 
            ApplicationExit.Name = "ApplicationExit";
            ApplicationExit.Size = new Size(184, 22);
            ApplicationExit.Text = "Bezárás";
            ApplicationExit.Click += ApplicationExit_Click;
            // 
            // szamlalo
            // 
            szamlalo.Enabled = true;
            szamlalo.Interval = 1000;
            szamlalo.Tick += szamlalo_Tick;
            // 
            // Controller_timer
            // 
            Controller_timer.Interval = 10;
            Controller_timer.Tick += Controller_timer_Tick_1;
            // 
            // SoundcheckBox
            // 
            SoundcheckBox.AutoSize = true;
            SoundcheckBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            SoundcheckBox.Location = new Point(306, 152);
            SoundcheckBox.Margin = new Padding(4, 3, 4, 3);
            SoundcheckBox.Name = "SoundcheckBox";
            SoundcheckBox.Size = new Size(156, 19);
            SoundcheckBox.TabIndex = 39;
            SoundcheckBox.Text = "Hang figyelmeztetés";
            SoundcheckBox.UseVisualStyleBackColor = true;
            // 
            // Volume_trackBar
            // 
            Volume_trackBar.Location = new Point(130, 220);
            Volume_trackBar.Maximum = 100;
            Volume_trackBar.Minimum = 1;
            Volume_trackBar.Name = "Volume_trackBar";
            Volume_trackBar.Size = new Size(186, 45);
            Volume_trackBar.TabIndex = 40;
            Volume_trackBar.Value = 1;
            Volume_trackBar.Scroll += Volume_trackBar_Scroll;
            // 
            // Testbutton
            // 
            Testbutton.Location = new Point(383, 220);
            Testbutton.Name = "Testbutton";
            Testbutton.Size = new Size(75, 23);
            Testbutton.TabIndex = 41;
            Testbutton.Text = "Teszt";
            Testbutton.UseVisualStyleBackColor = true;
            Testbutton.Click += button3_Click;
            // 
            // Soundlabel
            // 
            Soundlabel.AutoSize = true;
            Soundlabel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            Soundlabel.Location = new Point(57, 228);
            Soundlabel.Margin = new Padding(4, 0, 4, 0);
            Soundlabel.Name = "Soundlabel";
            Soundlabel.Size = new Size(66, 15);
            Soundlabel.TabIndex = 42;
            Soundlabel.Text = "Hangerő:";
            // 
            // Volumelabel
            // 
            Volumelabel.AutoSize = true;
            Volumelabel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            Volumelabel.Location = new Point(323, 223);
            Volumelabel.Margin = new Padding(4, 0, 4, 0);
            Volumelabel.Name = "Volumelabel";
            Volumelabel.Size = new Size(43, 15);
            Volumelabel.TabIndex = 43;
            Volumelabel.Text = "100%";
            // 
            // Settings
            // 
            AcceptButton = SaveButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(475, 625);
            Controls.Add(Volumelabel);
            Controls.Add(Soundlabel);
            Controls.Add(Testbutton);
            Controls.Add(Volume_trackBar);
            Controls.Add(SoundcheckBox);
            Controls.Add(button2);
            Controls.Add(label11);
            Controls.Add(lownetwork_textBox);
            Controls.Add(label12);
            Controls.Add(label9);
            Controls.Add(bandwith_textBox);
            Controls.Add(label10);
            Controls.Add(testmode_label);
            Controls.Add(networkusage_label);
            Controls.Add(lownetworkusage_checkBox);
            Controls.Add(interfaces_newComboBox);
            Controls.Add(current_cpu_label);
            Controls.Add(cpupercent_label);
            Controls.Add(label7);
            Controls.Add(cpu_textBox);
            Controls.Add(label8);
            Controls.Add(cpu_usage_checkBox);
            Controls.Add(cpu_trackBar);
            Controls.Add(cpu_progressBar);
            Controls.Add(showwarning_checkBox);
            Controls.Add(label5);
            Controls.Add(AtTime_textBox);
            Controls.Add(label6);
            Controls.Add(AtTime_checkBox);
            Controls.Add(AtInactivity_checkBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(shutdowntime_textbox);
            Controls.Add(Warningtime_textbox);
            Controls.Add(SaveButton);
            Controls.Add(notificationicon_checkBox);
            Controls.Add(startwithwindow_checkBox);
            Controls.Add(lastwarning_label);
            Controls.Add(lastshutdown_label);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Settings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Beállítások";
            TopMost = true;
            FormClosing += Settings_FormClosing;
            Shown += Settings_Shown;
            ((System.ComponentModel.ISupportInitialize)cpu_trackBar).EndInit();
            Kis_menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Volume_trackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lastshutdown_label;
        private System.Windows.Forms.Label lastwarning_label;
        private System.Windows.Forms.CheckBox startwithwindow_checkBox;
        private System.Windows.Forms.CheckBox notificationicon_checkBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox Warningtime_textbox;
        private System.Windows.Forms.TextBox shutdowntime_textbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox AtInactivity_checkBox;
        private System.Windows.Forms.CheckBox AtTime_checkBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox AtTime_textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox showwarning_checkBox;
        private System.Windows.Forms.ProgressBar cpu_progressBar;
        private System.Windows.Forms.TrackBar cpu_trackBar;
        private System.Windows.Forms.CheckBox cpu_usage_checkBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox cpu_textBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label cpupercent_label;
        private System.Windows.Forms.Label current_cpu_label;
        private NewComboBox interfaces_newComboBox;
        private System.Windows.Forms.CheckBox lownetworkusage_checkBox;
        private System.Windows.Forms.Label networkusage_label;
        private System.Windows.Forms.Label testmode_label;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox bandwith_textBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox lownetwork_textBox;
        private System.Windows.Forms.Label label12;
        private Button button2;
        internal NotifyIcon Ertesito_ikon;
        private System.Windows.Forms.Timer szamlalo;
        private ContextMenuStrip Kis_menu;
        private ToolStripMenuItem Windows_startup;
        private ToolStripMenuItem Setttings_menustrip;
        private ToolStripMenuItem openini_menustrip;
        private ToolStripMenuItem ApplicationExit;
        private System.Windows.Forms.Timer Controller_timer;
        private CheckBox SoundcheckBox;
        private TrackBar Volume_trackBar;
        private Button Testbutton;
        private Label Soundlabel;
        private Label Volumelabel;
    }
}