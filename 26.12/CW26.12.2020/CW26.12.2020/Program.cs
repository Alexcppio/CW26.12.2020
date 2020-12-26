using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CW26._12._2020
{
    public unsafe struct OsVersionInfo
    {
        public uint osVersionInfoSize;
        public uint majorVersion;
        public uint minorVersion;
        public uint buildNumber;
        public uint platformId;
        public fixed byte servicePackVersion[128];
    }
    //ExactSpelling = true
    //CharSet = CharSet.Unicode
    public class DllImportExample
    {
        [DllImport("User32.dll", ExactSpelling = true)]
        public static extern int MessageBoxA(IntPtr hWnd, string text,
            string caption, uint type);
        [DllImport("Kernel32.dll", EntryPoint = "GetVersionEx")]
        public static extern bool GetVersion(ref OsVersionInfo versionInfo);

        [DllImport("User32.dll")]
        public static extern int SetForegroundWindow(IntPtr p);
    }

    class Program
    {
        static void Main(string[] args)
        {
            string processName = "notepad.exe";
            Console.WriteLine("Enter text: ");
            string text = Console.ReadLine();
            Process p = Process.Start(processName);
            p.WaitForInputIdle();
            IntPtr h = p.MainWindowHandle;
            DllImportExample.SetForegroundWindow(h);
            SendKeys.SendWait(text);

            /*
            DllImportExample.MessageBoxA(IntPtr.Zero, "Test is successful", "Test", 0);

            OsVersionInfo vi = new OsVersionInfo();
            vi.osVersionInfoSize = (uint)Marshal.SizeOf(vi);
            bool result = DllImportExample.GetVersion(ref vi);

            if(result)
            {
                Console.WriteLine(vi.buildNumber);
            }*/
        }
    }
}
