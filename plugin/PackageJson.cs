using System.Collections.Generic;

namespace SPPM
{
    public class PackageJson
    {
        public string name { get; set; }
        public string main { get; set; }
        public string version { get; set; }
        public string license { get; set; }
        public string description { get; set; }
        public Dictionary<string, string> dependencies { get; set; }
        public Dictionary<string, string> devDependencies { get; set; }
    }
}