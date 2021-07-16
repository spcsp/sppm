using System.Collections.Generic;

namespace SPPM.NpmPackages
{
    public class ModuleDependencies : Dictionary<string, string> { }

    public class PackageJson
    {
        public string name { get; set; }
        public string main { get; set; }
        public string version { get; set; }
        public string license { get; set; }
        public string description { get; set; }
        public ModuleDependencies dependencies { get; set; }
        public ModuleDependencies devDependencies { get; set; }
    }
}