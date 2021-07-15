using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SPPM
{
    public static class SPPM
    {
        public static ScriptEngine Engine => ScriptEngine.Current;

        public static string RootPackagePath => Paths.JoinSpAppData("package.json");

        public static PackageJson RootPackageJson => ReadPackageJson(File.ReadAllText(RootPackagePath));

        public static void StrokesPlusInitStaticPlugin() { }

        public static void StrokesPlusEngineReload() { }

        public static void Install(string pkgId = "")
        {
            NPM.Exec($"install {pkgId}");
        }

        public static void Load(string pkgId)
        {
            if (Exists(pkgId))
            {
                Engine.Evaluate(GetSource(pkgId));
            }
            else
            {
                Notify($"{pkgId} was not found.");
            }
        }

        public static bool Exists(string pkgId)
        {
            return File.Exists(ResolvePackage(pkgId));
        }

        public static string GetSource(string pkgId)
        {
            return File.ReadAllText(ResolveMain(pkgId));
        }

        public static string Resolve(string pkgId = "")
        {
            return Path.GetFullPath(Path.Combine(Paths.NODE_MODULES, pkgId));
        }

        public static string ResolveMain(string pkgId)
        {
            return Path.GetFullPath(Path.Combine(Resolve(pkgId), ReadPackage(pkgId).main));
        }

        public static string ResolvePackage(string pkgId)
        {
            return Path.Combine(Resolve(pkgId), "package.json");
        }

        public static PackageJson ReadPackage(string pkgId)
        {
            return ReadPackageJson(ResolvePackage(pkgId));
        }

        public static PackageJson ReadPackageJson(string path)
        {
            return JsonConvert.DeserializeObject<PackageJson>(File.ReadAllText(path));
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
