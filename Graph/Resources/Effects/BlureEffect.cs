using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows;
using System.Security;

namespace SchoolLearn.Resources.Effects
{
    internal class WindowBlureEffect
    {
        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        private readonly uint _blurOpacity = 10;//почему-то не влияет на размытие (или очень незначительно)
        private readonly uint _blurBackgroundColor = 0x990000;

        private Window Window { get; set; }

        [SecurityCritical]
        private void EnableBlur()
        {
            AccentPolicy accent = new AccentPolicy
            {
                AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND,
                GradientColor = (_blurOpacity << 24) | (_blurBackgroundColor & 0xFFFFFF)
            };

            int accentStructSize = Marshal.SizeOf(accent);

            IntPtr accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            SetWindowCompositionAttribute(new WindowInteropHelper(Window).EnsureHandle(), ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        internal WindowBlureEffect(Window window)
        {
            Window = window;
            EnableBlur();
        }
    }
}