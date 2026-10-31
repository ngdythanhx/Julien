using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class ProcessHelper
{
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr OpenProcess(
        uint dwDesiredAccess,
        bool bInheritHandle,
        int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool TerminateProcess(
        IntPtr hProcess,
        uint uExitCode);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CloseHandle(IntPtr hObject);

    const uint PROCESS_TERMINATE = 0x0001;

    public static void KillProcess(int pid)
    {
        IntPtr hProcess = OpenProcess(
            PROCESS_TERMINATE,
            false,
            pid);

        if (hProcess == IntPtr.Zero)
            return;

        try
        {
            TerminateProcess(hProcess, 1);
        }
        finally
        {
            CloseHandle(hProcess);
        }
    }
}