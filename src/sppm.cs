using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.ClearScript;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static StrokesPlus.net.Engine.StrokesPlusClasses.Types.Internal;
using static StrokesPlus.net.Engine.StrokesPlusClasses.UI;

namespace SPPM
{
    public static class NPM
    {
        public static void Exec(string input) => Run(input, true);
        public static void ExecAsync(string input) => Run(input, false);
        private static int Run(string input, bool waitForExit)
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
            
            if (waitForExit)
            {
                using (var proc = Process.Start(info))
                {
                    proc.WaitForExit();
                    exitCode = proc.ExitCode;
                }
            }
            else
            {
                Process.Start(info);
                return 0;
            }

            return exitCode;
        }
    }
    public static class SPPM
    {
        public static string JS_SOURCE { get; } = Properties.Resources.sppm;
        public static string PACKAGE_JSON { get; } = "package.json";
        public static ScriptEngine Engine { get; set; }
        public static void StrokesPlusInitStaticPlugin(ScriptEngine engine) => Engine = engine;
        public static void Install(string pkgId)
        {
            //
        }
        public static void LoadClearScriptModule()
        {
            try {
                Engine.Execute(JS_SOURCE);
            } catch (Exception err) {
                ShowError(err.Message);
            }
        }
        public static string Resolve(string pkgId)
        {
            string pkgPath = Path.Combine(Paths.NODE_MODULES, pkgId, PACKAGE_JSON);

            return Path.GetFullPath(pkgPath);
        }
        public static bool Exists(string pkgId)
        {
            return File.Exists(ResolvePackage(pkgId));
        }
        public static void Load(string pkgId) {
            if (Exists(pkgId)) {
                Engine.Evaluate(ResolveMain(pkgId));
            } else {
                ShowError($"{pkgId} was not found.");
            }
        }
        public static void Load(string[] pkgIds)
        {
            Array.ForEach(pkgIds, Load);
        }

        public static string ResolveMain(string pkgId)
        {
            JObject packgage = ReadPackage(pkgId);
            string mainEntry = packgage.GetValue("main").ToString();

            return Path.GetFullPath(Path.Combine(Resolve(pkgId), mainEntry));
        }
        public static string ResolvePackage(string pkgId)
        {
            return Path.Combine(Resolve(pkgId), PACKAGE_JSON);
        }
        public static JObject ReadPackage(string pkgId)
        {
            using (StreamReader reader = File.OpenText(ResolvePackage(pkgId)))
            {
                return (JObject) JToken.ReadFrom(new JsonTextReader(reader));
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
    }
    public static class Paths
    {
        public static string APPDATA { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string SP_APPDATA { get; } = Path.Combine(APPDATA, "StrokesPlus.net");
        public static string NODE_MODULES { get; } = Path.Combine(SP_APPDATA, "node_modules");
    }
}
