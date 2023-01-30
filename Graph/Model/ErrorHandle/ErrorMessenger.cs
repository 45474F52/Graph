using System.Runtime.InteropServices;
using System;
using System.Security;

namespace Graph.Model.ErrorHandle
{
    internal class ErrorMessenger
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.I4)]
        private static extern int MessageBox(IntPtr hWnd,
                                             [param: MarshalAs(UnmanagedType.LPTStr)] string message,
                                             [param: MarshalAs(UnmanagedType.LPTStr)] string caption,
                                             [param: MarshalAs(UnmanagedType.U4)] uint type);

        internal static Results ShowMessage(string message) => Show(message, string.Empty, Buttons.Ok, Icons.Error);

        internal static Results ShowMessage(string message, string caption) => Show(message, caption, Buttons.Ok, Icons.Error);

        internal static Results ShowMessage(string message, string caption, Buttons buttons) => Show(message, caption, buttons, Icons.Error);

        internal static Results ShowMessage(string message, string caption, Icons icons) => Show(message, caption, Buttons.Ok, icons);

        internal static Results ShowMessage(string message, string caption, Buttons buttons, Icons icons) => Show(message, caption, buttons, icons);

        [SecurityCritical]
        private static Results Show(string message, string caption, Buttons buttons, Icons icons)
        {
            Results result = (Results)MessageBox(GetActiveWindow(), message, caption, (uint)buttons | (uint)icons);

            int error = Marshal.GetLastWin32Error();
            if (error != 0)
            {
                throw new Exception($"Win32 Error: {error}");
            }

            return result;
        }
    }

    internal enum Buttons : int
    {
        Ok = 0x0,
        OkCancel = 0x1,
        AbortRetryIgnore = 0x2,
        YesNoCancel = 0x3,
        YesNo = 0x4,
        RetryCancel = 0x5,
        CancelTryIgnore = 0x6,
        Help = 0x4000
    }

    internal enum Icons : int
    {
        None = 0x0,
        Error = 0x10,
        Question = 0x20,
        Warning = 0x30,
        Information = 0x40
    }

    internal enum Results : int
    {
        None = 0x0,
        Ok = 0x1,
        Cancel = 0x2,
        Abort = 0x3,
        Retry = 0x4,
        Ignore = 0x5,
        Yes = 0x6,
        No = 0x7,
        Close = 0x8,
        Help = 0x9,
        TryAgain = 0xA,
        Continue = 0xB,
        TimeOut = 0x7D00
    }
}