using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.ClearScript;
using Newtonsoft.Json;
using SPPM.NpmPackages;

namespace SPPM
{
    public static class SPPM
    {
        public static ScriptEngine Engine => ScriptEngine.Current;

        public static string RootPackagePath => Paths.JoinSpAppData("package.json");

        public static PackageJson RootPackageJson => ReadPackageJson(RootPackagePath);

        public static void StrokesPlusInitStaticPlugin()
        {
            //MessageBox.Show("Init Plugin", "");
        }

        public static void StrokesPlusEngineReload()
        {
            //MessageBox.Show("Reloading Plugin ", "");
        }

        public static void Install(string pkgId = "") => NPM.Exec($"install {pkgId}");

        public static void Load(string[] pkgIds) => Array.ForEach(pkgIds, Load);

        public static void Load(string pkgId)
        {
            try
            {
                if (Exists(pkgId))
                {
                    Engine.Evaluate(GetModuleSource(pkgId));
                }
                else
                {
                    Notify($"{pkgId} was not found.");
                }
            } catch (Exception err)
            {
                Notify(err.ToString());
            }
        }
        public static void LoadAll()
        {
            RootPackageJson.dependencies.Select(x => x.Key).ToList().ForEach(Load);
        }

        public static bool Exists(string pkgId) => File.Exists(ResolveModulePackage(pkgId));

        public static string Resolve(string pkgId = "") => Paths.FullJoin(Paths.NODE_MODULES, pkgId);

        public static string GetModuleSource(string pkgId) => File.ReadAllText(ResolveMain(pkgId));

        public static string ResolveModulePackage(string pkgId) => Paths.FullJoin(Resolve(pkgId), "package.json");

        public static string ResolveMain(string pkgId) => Paths.FullJoin(Resolve(pkgId), ReadModulePackage(pkgId).main);

        public static PackageJson ReadPackageJson(string path) => ParsePackageJson(File.ReadAllText(path));

        public static PackageJson ReadModulePackage(string pkgId) => ReadPackageJson(ResolveModulePackage(pkgId));

        public static PackageJson ParsePackageJson(string json) => JsonConvert.DeserializeObject<PackageJson>(json);

        private static void Notify(string message)
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
    }
}
