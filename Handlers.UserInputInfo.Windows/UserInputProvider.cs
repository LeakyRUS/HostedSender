using Handlers.Abstractions.UserInputInfo;
using System.Runtime.InteropServices;

namespace Handlers.UserInputInfo.Windows;

internal class UserInputProvider : IUserInputProvider
{
    [DllImport("user32.dll", SetLastError = false)]
    private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

    [StructLayout(LayoutKind.Sequential)]
    private struct LASTINPUTINFO
    {
        public uint cbSize;
        public int dwTime;
    }

    public InputInfo GetUserInputInfo()
    {
        var lii = new LASTINPUTINFO
        {
            cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO))
        };
        GetLastInputInfo(ref lii);

        return new InputInfo(lii.dwTime);
    }
}
