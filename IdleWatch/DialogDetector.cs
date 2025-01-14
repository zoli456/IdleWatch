using System.Diagnostics;
using System.Text;
using IdleWatch;
using SharpDX.XInput;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;

public class DialogDetector
{
    private const int WH_MOUSE_LL = 14;
    private const int WM_RBUTTONDOWN = 0x0204;
    private const uint WM_SETTEXT = 0x000C;

    /*private static readonly List<string> FileDialogClassNames = new()
    {
        "DirectUIHWND", "DUIViewWndClassName", "ReBarWindow32", "ComboBox", "Edit"
    };*/

    private static SafeHHOOK mouseHook = new(IntPtr.Zero);
    private static readonly HookProc mouseProc = HookCallback;

    // Set mouse hook
    public static void SetHook()
    {
        var hInstance = Kernel32.GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
        mouseHook = SetWindowsHookEx(HookType.WH_MOUSE_LL, mouseProc, hInstance);
    }

    // Remove mouse hook
    public static void Unhook()
    {
        UnhookWindowsHookEx(mouseHook);
    }

    // Mouse hook callback
    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == WM_RBUTTONDOWN)
        {
            // Get the window under the cursor
            var cursorPos = Cursor.Position;
            var hWnd = (IntPtr)WindowFromPoint(new Point(cursorPos.X, cursorPos.Y));

            // Check if it's a file dialog

            var parentElement = (IntPtr)GetParent(hWnd);
            if (IsFileDialogWindow(hWnd))
            {
                Debug.WriteLine("Right-click detected on a file dialog window.");
                SetDialogLocation(@"");
                return CallNextHookEx(mouseHook, nCode, wParam, lParam);
            }
            if (IsTreeFileDialog(parentElement))
            {
                Debug.WriteLine("Right-click detected on a file dialog window.");
                SetTreeLocation(@"Desktop\\Computer");
            }
        }

        return CallNextHookEx(mouseHook, nCode, wParam, lParam);
    }

    private static bool IsFileDialogWindow(IntPtr hWnd)
    {
        var className = new StringBuilder(256);
        GetClassName(hWnd, className, className.Capacity);


        if (className.ToString() == "#32770") 
        {
            Debug.WriteLine($"Osztály név: {className.ToString()}");
            var hasDialogStructure = false;

            EnumChildWindows(hWnd, (childHWnd, lParam) =>
            {
                var childClassName = new StringBuilder(256);
                GetClassName(childHWnd, childClassName, childClassName.Capacity);

                if (childClassName.ToString()== "ComboBox")
                {
                    hasDialogStructure = true;
                    return false;
                }

                return true;
            }, IntPtr.Zero);

            return hasDialogStructure;
        }

        return false;
    }

    private static bool IsTreeFileDialog(IntPtr hWnd)
    {
        var className = new StringBuilder(256);
        GetClassName(hWnd, className, className.Capacity);
        Debug.WriteLine($"Osztály név: {className.ToString()}");
        if (className.ToString() == "#32770")
        {
            var hasTreeView = false;

            EnumChildWindows(hWnd, (childHWnd, lParam) =>
            {
                var childClassName = new StringBuilder(256);
                GetClassName(childHWnd, childClassName, childClassName.Capacity);
                Debug.WriteLine(childClassName.ToString());
                if (childClassName.ToString() == "SysTreeView32")
                {
                    hasTreeView = true;
                    return false;
                }

                return true;
            }, IntPtr.Zero);

            return hasTreeView;
        }

        return false;
    }

    private static IntPtr FindPathControl(IntPtr hWnd)
    {
        var pathControl = IntPtr.Zero;

        EnumChildWindows(hWnd, (childHWnd, lParam) =>
        {
            var className = new StringBuilder(256);
            GetClassName(childHWnd, className, className.Capacity);

            if (className.ToString() == "Edit")
            {
                pathControl = (IntPtr)childHWnd;
                return false;
            }

            if (className.ToString() == "ComboBox" || className.ToString() == "ComboBoxEx32")
            {
                var editControl = (IntPtr)FindWindowEx(childHWnd, IntPtr.Zero, "Edit");
                if (editControl != IntPtr.Zero)
                {
                    pathControl = editControl;
                    return false;
                }
            }

            return true;
        }, IntPtr.Zero);

        return pathControl;
    }

    public static void SetDialogLocation(string newPath)
    {
        var fileDialogHandle = IntPtr.Zero;
        EnumWindows((hWnd, lParam) =>
        {
            if (IsWindowVisible(hWnd) && IsFileDialogWindow((IntPtr)hWnd))
            {
                fileDialogHandle = (IntPtr)hWnd;
                return false;
            }

            return true;
        }, IntPtr.Zero);

        if (fileDialogHandle == IntPtr.Zero)
        {
            Console.WriteLine("No file dialog found.");
            return;
        }

        SetForegroundWindow(fileDialogHandle);

        Thread.Sleep(500);

        var pathControl = FindPathControl(fileDialogHandle);

        if (pathControl == IntPtr.Zero)
        {
            Console.WriteLine("Path input control not found in the file dialog.");
            return;
        }

        SendMessage(pathControl, WM_SETTEXT, IntPtr.Zero, newPath);
        SendKeys.SendWait("{ENTER}");

        Console.WriteLine($"Set dialog location to: {newPath}");
    }

    public static void SetTreeLocation(string newPath)
    {
        var fileDialogHandle = IntPtr.Zero;
        var TreeviewHandler = IntPtr.Zero;
        EnumWindows((hWnd, lParam) =>
        {
            if (IsWindowVisible(hWnd) && IsTreeFileDialog((IntPtr)hWnd))
            {
                fileDialogHandle = (IntPtr)hWnd;
                EnumChildWindows(hWnd, (childHWnd, lParam) =>
                {
                    var childClassName = new StringBuilder(256);
                    GetClassName(childHWnd, childClassName, childClassName.Capacity);
                    if (childClassName.ToString() == "SysTreeView32")
                    {
                        TreeviewHandler = (IntPtr)childHWnd;
                        return false;
                    }

                    return true;
                }, IntPtr.Zero);
                return false;
            }

            return true;
        }, IntPtr.Zero);

        if (TreeviewHandler == IntPtr.Zero)
        {
            Console.WriteLine("No file dialog found.");
            return;
        }

        SetForegroundWindow(TreeviewHandler);

        Thread.Sleep(500);

        //Add TreeView logic
        if (SysTreeView32Controller.NavigateToPath(TreeviewHandler, @"C:"))
        {
            Debug.WriteLine("First node selected successfully.");
        }
        else
        {
            Debug.WriteLine("Failed to select the first node.");
        }

        Console.WriteLine($"Set treeview location to: {newPath}");
    }

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
}