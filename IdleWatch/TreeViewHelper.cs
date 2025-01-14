using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Vanara.PInvoke;

public static class SysTreeView32Controller
{
    private static HWND _handle;

    private static class TreeViewConstants
    {
        public const uint TVGN_ROOT = 0x0;
        public const uint TVGN_CHILD = 0x4;
        public const uint TVGN_NEXT = 0x1;
        public const uint TVGN_CARET = 0x9;

        public const uint TVIF_TEXT = 0x0001;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct TVITEM
    {
        public uint mask;
        public IntPtr hItem;
        public uint state;
        public uint stateMask;
        public IntPtr pszText;
        public int cchTextMax;
        public int iImage;
        public int iSelectedImage;
        public int cChildren;
        public IntPtr lParam;
    }

    public static bool NavigateToPath(IntPtr handle, string path)
    {
        if (handle == IntPtr.Zero)
            throw new ArgumentException("Handle cannot be zero.", nameof(handle));
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be null or empty.", nameof(path));

        _handle = new HWND(handle);

        try
        {
            // Step 1: Select the root node
            var rootItem = GetRootNode();
            if (rootItem == IntPtr.Zero || !SelectNode(rootItem))
            {
                Debug.WriteLine("Failed to select the root node.");
                return false;
            }

            Debug.WriteLine("Root node selected successfully.");

            // Step 2: Locate and select the "Computer" node dynamically
            var computerNodeName = GetLocalizedNodeName("Computer");
            if (string.IsNullOrEmpty(computerNodeName))
            {
                Debug.WriteLine("Failed to retrieve the localized name for 'Computer'.");
                return false;
            }

            var computerNode = FindChildNodeByText(rootItem, computerNodeName);
            if (computerNode == IntPtr.Zero || !SelectNode(computerNode))
            {
                Debug.WriteLine($"Failed to locate or select the '{computerNodeName}' node.");
                return false;
            }

            Debug.WriteLine($"'{computerNodeName}' node selected successfully.");

            // Step 3: Navigate to the specified path
            if (!NavigateToPathFromNode(computerNode, path))
            {
                Debug.WriteLine($"Failed to navigate to path: {path}");
                return false;
            }

            Debug.WriteLine($"Successfully navigated to path: {path}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }

    private static IntPtr GetRootNode()
    {
        return User32.SendMessage(
            _handle,
            (User32.WindowMessage)ComCtl32.TreeViewMessage.TVM_GETNEXTITEM,
            (IntPtr)TreeViewConstants.TVGN_ROOT,
            IntPtr.Zero
        );
    }

    private static bool SelectNode(IntPtr node)
    {
        if (node == IntPtr.Zero)
        {
            Debug.WriteLine("Cannot select a null node.");
            return false;
        }

        var result = User32.SendMessage(
            _handle,
            (User32.WindowMessage)ComCtl32.TreeViewMessage.TVM_SELECTITEM,
            (IntPtr)TreeViewConstants.TVGN_CARET,
            node
        );

        if (result == IntPtr.Zero)
        {
            Debug.WriteLine("Failed to select node.");
            return false;
        }

        return true;
    }

    private static IntPtr FindChildNodeByText(IntPtr parentNode, string text)
    {
        if (parentNode == IntPtr.Zero || string.IsNullOrWhiteSpace(text))
        {
            Debug.WriteLine("Invalid parent node or text.");
            return IntPtr.Zero;
        }

        var childNode = User32.SendMessage(
            _handle,
            (User32.WindowMessage)ComCtl32.TreeViewMessage.TVM_GETNEXTITEM,
            (IntPtr)TreeViewConstants.TVGN_CHILD,
            parentNode
        );

        while (childNode != IntPtr.Zero)
        {
            var nodeText = GetNodeText(childNode);
            if (string.Equals(nodeText, text, StringComparison.OrdinalIgnoreCase))
                return childNode;

            childNode = User32.SendMessage(
                _handle,
                (User32.WindowMessage)ComCtl32.TreeViewMessage.TVM_GETNEXTITEM,
                (IntPtr)TreeViewConstants.TVGN_NEXT,
                childNode
            );
        }

        return IntPtr.Zero;
    }

    private static bool NavigateToPathFromNode(IntPtr startNode, string path)
    {
        if (startNode == IntPtr.Zero || string.IsNullOrWhiteSpace(path))
        {
            Debug.WriteLine("Invalid start node or path.");
            return false;
        }

        var pathSegments = path.Split('\\');
        var currentNode = startNode;

        foreach (var segment in pathSegments)
        {
            var childNode = FindChildNodeByText(currentNode, segment);
            if (childNode == IntPtr.Zero || !SelectNode(childNode))
            {
                Debug.WriteLine($"Failed to locate or select segment: {segment}");
                return false;
            }

            currentNode = childNode;
        }

        return true;
    }

    private static string GetNodeText(IntPtr node)
    {
        if (node == IntPtr.Zero)
        {
            Debug.WriteLine("Cannot retrieve text for a null node.");
            return string.Empty;
        }

        const int MaxTextLength = 512;
        var textBuffer = Marshal.AllocHGlobal(MaxTextLength);

        try
        {
            var item = new TVITEM
            {
                mask = TreeViewConstants.TVIF_TEXT,
                hItem = node,
                pszText = textBuffer,
                cchTextMax = MaxTextLength
            };

            var itemPtr = Marshal.AllocHGlobal(Marshal.SizeOf<TVITEM>());
            Marshal.StructureToPtr(item, itemPtr, false);

            if (User32.SendMessage(
                    _handle,
                    (User32.WindowMessage)ComCtl32.TreeViewMessage.TVM_GETITEM,
                    IntPtr.Zero,
                    itemPtr
                ) == IntPtr.Zero)
            {
                Debug.WriteLine("Failed to retrieve node text.");
                return string.Empty;
            }

            return Marshal.PtrToStringAuto(textBuffer) ?? string.Empty;
        }
        finally
        {
            Marshal.FreeHGlobal(textBuffer);
        }
    }

    private static string GetLocalizedNodeName(string defaultName)
    {
        // Example logic for retrieving the localized name dynamically
        if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "hu")
            return "Ez a gép"; // Hungarian for 'Computer'

        return defaultName;
    }
}
