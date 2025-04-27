using System.ComponentModel;
using System.Runtime.InteropServices;
using Vanara.PInvoke;

namespace IdleWatch;

public class NewComboBox : ComboBox
{
    public NewComboBox()
    {
        DrawMode = DrawMode.OwnerDrawFixed;
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        SetupEdit();
    }

    private void SetupEdit()
    {
        var info = new COMBOBOXINFO
        {
            cbSize = (uint)Marshal.SizeOf(typeof(COMBOBOXINFO))
        };

        if (!GetComboBoxInfo(Handle, ref info)) throw new Win32Exception();

        var style = User32.GetWindowLong(info.hwndEdit, User32.WindowLongFlags.GWL_STYLE);
        style |= 1;
        User32.SetWindowLong(info.hwndEdit, User32.WindowLongFlags.GWL_STYLE, style);
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        base.OnDrawItem(e);
        e.DrawBackground();

        var txt = e.Index >= 0 ? GetItemText(Items[e.Index]) : string.Empty;
        TextRenderer.DrawText(
            e.Graphics,
            txt,
            Font,
            e.Bounds,
            ForeColor,
            TextFormatFlags.Left | TextFormatFlags.HorizontalCenter);
    }

    // Import the specific function we need from user32.dll
    [DllImport("user32.dll")]
    private static extern bool GetComboBoxInfo(IntPtr hWnd, ref COMBOBOXINFO pcbi);

    [StructLayout(LayoutKind.Sequential)]
    private struct COMBOBOXINFO
    {
        public uint cbSize;
        public RECT rcItem;
        public RECT rcButton;
        public uint stateButton;
        public IntPtr hwndCombo;
        public IntPtr hwndEdit;
        public IntPtr hwndList;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}