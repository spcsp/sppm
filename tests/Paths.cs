using System;
using System.IO;

namespace SPPM.Testing
{
    public static class Paths
    {
        public static string StrokesPlusConfigRoot
        {
            get
            {
                return Path.Combine("C:\\", "Users", Environment.UserName, "AppData", "Roaming", "StrokesPlus.net");
            }
        }

        public static string StrokesPlusModules
        {
            get
            {
                return Path.Combine(StrokesPlusConfigRoot, "node_modules");
            }
        }
    }
}