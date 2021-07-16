using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SPPM.NpmPackages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SPPM
{
    public static class SPPM
    {
        public static ScriptEngine Engine => ScriptEngine.Current;

        public static string RootPackagePath => Paths.JoinSpAppData("package.json");

        public static PackageJson RootPackageJson => ReadPackageJson(RootPackagePath);

        public static ModuleDependencies Dependencies => ReadPackageJson(RootPackagePath).dependencies;

        public static List<string> InstalledModules => Dependencies.Select(x => x.Key).ToList();

        public static void StrokesPlusInitStaticPlugin() { }

        public static void StrokesPlusEngineReload() { }

        public static void Use(params string[] pkgIds) => Array.ForEach(pkgIds, Load);

        public static void Autoload() => InstalledModules.ForEach(Load);

        public static void Install(string pkgId = "")
        {
            NPM.Exec($"install {pkgId}");
        }

        public static void Load(string pkgId)
        {
            if (Exists(pkgId))
            {
                Engine.Evaluate(GetModuleSource(pkgId));
            }
            else
            {
                Notify($"{pkgId} was not found.");
            }
        }

        public static string Resolve(string pkgId = "")
        {
            return Path.GetFullPath(Path.Combine(Paths.NODE_MODULES, pkgId));
        }

        public static string ResolveModulePackage(string pkgId)
        {
            return Path.Combine(Resolve(pkgId), "package.json");
        }

        public static bool Exists(string pkgId)
        {
            return File.Exists(ResolveModulePackage(pkgId));
        }

        public static string GetModuleSource(string pkgId)
        {
            return File.ReadAllText(ResolveMain(pkgId));
        }

        public static string ResolveMain(string pkgId)
        {
            return Path.GetFullPath(Path.Combine(Resolve(pkgId), ReadModulePackage(pkgId).main));
        }

        public static PackageJson ReadModulePackage(string pkgId)
        {
            return ReadPackageJson(ResolveModulePackage(pkgId));
        }

        public static PackageJson ReadPackageJson(string path)
        {
            return ParsePackageJson(File.ReadAllText(path));
        }

        public static PackageJson ParsePackageJson(string json)
        {
            return JsonConvert.DeserializeObject<PackageJson>(json);
        }

        internal static void Notify(string message)
        {
            var textInfo = new StrokesPlus.net.Engine.StrokesPlusClasses.Types.Internal.DisplayTextInfo()
            {
                Message = message,
                UsePrimaryScreen = true,
                Title = "StrokesPlus Plugin Manager",
                TitleFont = new Font("Segoe UI", 18, FontStyle.Bold),
                TitleAlignment = "Right",
                MessageFont = new Font("Segoe UI Semibold", 14),
                MessageAlignment = "Right",
                Padding = 10,
                Duration = 3000,
                Location = "topright",
                Opacity = 1,
                ForeColor = "SteelBlue",
                BackColor = "White",
            };

            StrokesPlus.net.Engine.StrokesPlusClasses.UI.TextOverlay.Show(textInfo);
        }
        
        /*
        public static void Evaluate(string pkgId)
        {
            var commonJsDocInfo = new DocumentInfo()
            {
                Category = ModuleCategory.CommonJS
            };

            Engine.DocumentSettings.SearchPath = Path.GetFullPath(Paths.NODE_MODULES);
            Engine.DocumentSettings.AccessFlags = DocumentAccessFlags.EnableFileLoading;

            Engine.Evaluate(commonJsDocInfo, GetSource(pkgId));
        }
        */
    }
}
