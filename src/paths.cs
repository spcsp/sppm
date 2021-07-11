using System;
using System.IO;

namespace SPPM
{
    public static class Paths
    {
        public static string APPDATA { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string SP_APPDATA { get; } = Path.Combine(APPDATA, "StrokesPlus.net");
        public static string NODE_MODULES { get; } = Path.Combine(SP_APPDATA, "node_modules");
    }
}
