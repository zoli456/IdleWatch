using System.Runtime.InteropServices;

namespace IdleWatch;

public class NewComboBox : ComboBox
{
    private const int GWL_STYLE = -16;
    private const int ES_LEFT = 0x0000;
    private const int ES_CENTER = 0x0001;
    private const int ES_RIGHT = 0x0002;
    private int buttonWidth = SystemInformation.HorizontalScrollBarArrowWidth;

    public NewComboBox()
    {
        DrawMode = DrawMode.OwnerDrawFixed;
    }

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    public static extern bool GetComboBoxInfo(IntPtr hWnd, ref COMBOBOXINFO pcbi);

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        SetupEdit();
    }

    private void SetupEdit()
    {
        var info = new COMBOBOXINFO();
        info.cbSize = Marshal.SizeOf(info);
        GetComboBoxInfo(Handle, ref info);
        var style = GetWindowLong(info.hwndEdit, GWL_STYLE);
        style |= 1;
        SetWindowLong(info.hwndEdit, GWL_STYLE, style);
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        base.OnDrawItem(e);
        e.DrawBackground();
        var txt = "";
        if (e.Index >= 0)
            txt = GetItemText(Items[e.Index]);
        TextRenderer.DrawText(e.Graphics, txt, Font, e.Bounds,
            ForeColor, TextFormatFlags.Left | TextFormatFlags.HorizontalCenter);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
        public int Width => Right - Left;
        public int Height => Bottom - Top;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COMBOBOXINFO
    {
        public int cbSize;
        public RECT rcItem;
        public RECT rcButton;
        public int stateButton;
        public IntPtr hwndCombo;
        public IntPtr hwndEdit;
        public IntPtr hwndList;
    }
}