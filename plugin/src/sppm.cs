﻿using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SPPM
{
    public static class SPPM
    {
        //public static ScriptEngine Engine { get; set; }

        public static ScriptEngine Engine => ScriptEngine.Current;

        //public static void StrokesPlusInitStaticPlugin(ScriptEngine engine) => Engine = engine;

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
                Engine.Evaluate(GetSource(pkgId));
            }
            else
            {
                ShowError($"{pkgId} was not found.");
            }
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

        public static void Notify(string message, string title)
        {
            var textInfo = new StrokesPlus.net.Engine.StrokesPlusClasses.Types.Internal.DisplayTextInfo();

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

            StrokesPlus.net.Engine.StrokesPlusClasses.UI.TextOverlay.Show(textInfo);
        }

        public static void ShowError(object what)
        {
            MessageBox.Show(what.ToString(), "SPPM | Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}