using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static StrokesPlus.net.Engine.StrokesPlusClasses.Types.Internal;
using static StrokesPlus.net.Engine.StrokesPlusClasses.UI;

namespace SPPM
{
    public static class SPPM
    {
        public static ScriptEngine Engine { get; set; }

        public static void StrokesPlusInitStaticPlugin(ScriptEngine engine) => Engine = engine;

        public static void Install(string pkgId = "")
        {
            NPM.Exec($"install {pkgId}");
        }

        public static bool Exists(string pkgId)
        {
            return File.Exists(ResolvePackage(pkgId));
        }

        public static string GetSource(string pkgId)
        {
            return File.ReadAllText(ResolveMain(pkgId));
        }

        public static void Load(string pkgId)
        {
            if (Exists(pkgId))
            {
                Execute(@"" + GetSource(pkgId));
            }
            else
            {
                ShowError($"{pkgId} was not found.");
            }
        }

        public static void Load(string[] pkgIds)
        {
            Array.ForEach(pkgIds, Load);
        }

        public static string Resolve(string pkgId = "")
        {
            return Path.GetFullPath(Path.Combine(Paths.NODE_MODULES, pkgId));
        }

        public static string ResolvePackage(string pkgId)
        {
            return Path.Combine(Resolve(pkgId), "package.json");
        }

        public static string ResolveMain(string pkgId)
        {
            JObject packgage = ReadPackage(pkgId);
            string mainEntry = packgage.GetValue("main").ToString();

            return Path.GetFullPath(Path.Combine(Resolve(pkgId), mainEntry));
        }

        public static JObject ReadPackage(string pkgId)
        {
            using (StreamReader reader = File.OpenText(ResolvePackage(pkgId)))
            {
                return (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            }
        }

        public static void Notify(string message, string title)
        {
            DisplayTextInfo textInfo = new DisplayTextInfo();

            textInfo.UsePrimaryScreen = true;
            textInfo.Message = message;
            textInfo.Title = title;
            textInfo.TitleFont = new Font("Segoe UI", 18, FontStyle.Bold);
            textInfo.TitleAlignment = "Right";
            textInfo.MessageFont = new Font("Segoe UI Semibold", 14);
            textInfo.MessageAlignment = "Right";
            textInfo.Padding = 10;
            textInfo.Duration = 2000;
            textInfo.Location = "topright";
            textInfo.Opacity = 1;
            textInfo.ForeColor = "SteelBlue";
            textInfo.BackColor = "White";

            TextOverlay.Show(textInfo);
        }

        public static void ShowError(object what)
        {
            MessageBox.Show(what.ToString(), "SPPM | Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void Execute(string pkgId)
        {

            var commonJsDocInfo = new DocumentInfo() {
                Category = ModuleCategory.CommonJS
            };

            Engine.DocumentSettings.SearchPath = Path.GetFullPath(Paths.NODE_MODULES);
            Engine.DocumentSettings.AccessFlags = DocumentAccessFlags.EnableFileLoading;

            Engine.Execute(commonJsDocInfo, GetSource(pkgId));
        }
        /*
        private static void Execute(string script)
        {
            var docInfo = new DocumentInfo() {
                Category = ModuleCategory.CommonJS
            };
            Engine.DocumentSettings.SearchPath = Paths.NODE_MODULES;
            Engine.DocumentSettings.AccessFlags = DocumentAccessFlags.EnableFileLoading;

            Engine.Execute(, script);
        }
        */
    }
}
