using System;
using System.Diagnostics;

namespace SPPM
{
    public static class NPM
    {
        public static int Exec(string input)
        {
            Environment.CurrentDirectory = Paths.SP_APPDATA;

            var info = new ProcessStartInfo("C:\\Program Files\\nodejs\\npm.cmd")
            {
                Arguments = input,
                CreateNoWindow = true,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            var exitCode = 0;

            using (var proc = Process.Start(info))
            {
                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }

            return exitCode;
        }
    }
}
