(function(spGlobal) {
  const notify = (message, title) => {
      const textInfo = new StrokesPlus.Types.Internal.DisplayTextInfo();

      textInfo.UsePrimaryScreen = true;
      textInfo.Message = message;
      textInfo.Title = title;
      textInfo.TitleFont = new Font("Segoe UI", 18, host.flags(FontStyle.Bold));
      textInfo.TitleAlignment = "Right";
      textInfo.MessageFont = new Font("Segoe UI Semibold", 14);
      textInfo.MessageAlignment = "Right";
      textInfo.Padding = 10;
      textInfo.Duration = 2000;
      textInfo.Location = "bottomright";
      textInfo.Opacity = 0.9;
      textInfo.ForeColor =  "white";
      textInfo.BackColor = "SteelBlue";

      return StrokesPlus.UI.TextOverlay.Show(textInfo);
  }

  const alert = StrokesPlus.UI.Alert;

  const APPDATA = StrokesPlus.OS.Shell.ExpandEnvironmentVariables('%APPDATA%');
  const SP_APPDATA = System.IO.Path.Combine(APPDATA, 'StrokesPlus.net');
  const NODE_MODULES = System.IO.Path.Combine(SP_APPDATA, 'node_modules');
  const PKG_JSON = "package.json";

  function npm(input) {
      const NPM = 'C:\\Program Files\\nodejs\\npm.cmd';
      const CMD = `cd "${SP_APPDATA}" && "${NPM}" ${input}`;
     
      try {
          StrokesPlus.OS.Shell.RunProgram('cmd.exe', '/C ' + CMD, '',  'hidden', true, true, true);
      } catch (err) {
          alert(err.toString(), "Error");
      }
  }

  function resolve(id) {
      const pkgPath = System.IO.Path.Combine(NODE_MODULES, id, PKG_JSON);
      
      if (System.IO.File.Exists(pkgPath)) {
          return pkgPath;
      } else {
          alert(`${pkgPath} was not found`, "Error");
      }
  }

  function readPackage(abspath) {
      try {
          return JSON.parse(System.IO.File.ReadAllText(abspath));
      } catch (error) {
          alert(error, "error");
      }
  }

  class Plugin {
      static load(id) {
          const pkgPath = resolve(id);
          const plugin = new Plugin(pkgPath);
          const src = plugin.source;
          __spEngineWrapper.Engine.Evaluate(src);
      }
      static fromNpm(id) {
          if (typeof id === "string") {
              notify(id, `Installing from npm...`);
              npm(`install --save ${id}`);
          } else {
              npm(`install`);        
          }
      }
      constructor(abspath) {
          this.abspath = abspath;
          this.resolvedPkg = this.abspath.replace("/", "\\").replace("\\\\", "\\");
      }
      get pkgExists() {
          return System.IO.File.Exists(this.resolvedPkg);
      }
      get pkg() {
          const contents = System.IO.File.ReadAllText(this.resolvedPkg);
          try {
              return JSON.parse(contents);
          } catch (error) {
              return {};
          }
      }
      get mainEntry() {
          const path = this.resolvedPkg.replace(PKG_JSON, "");
          return System.IO.Path.Combine(path, this.pkg.main);
      }
      get source() {
          try {
              return System.IO.File.ReadAllText(this.mainEntry);
          } catch (error) {
              return alert(abspath, 'File Read Error');
          }
      }
  }

    const SSPM = class {
        constructor() {
            //
        }

        install(id) {
            if (typeof id === "string") {
                notify(`Installing "${id}" from npmjs`, "StrokesPlus Package Manager");
                npm(`install --save ${id}`);
            } else {
                npm(`install`);
            }
        }
    }

    function once(fn) {
        if (__spEngineWrapper.Engine.Name == StrokesPlus.ScriptEngine.List.Last().Engine.Name) {
            fn();
        }
    }
    
    spGlobal["SPPM"] = SPPM;
    spGlobal["Plugin"] = Plugin;
    spGlobal["sppm"] = new spGlobal["SPPM"]();
})(this);
//
////////////////////////////////////////////////////////////////////
//
//sppm.install('@spcsp/explore-settings-json');
//Plugin.load('@spcsp/explore-settings-json');

//Plugin.load('@spcsp/osd-toast');
//toast("hi");
