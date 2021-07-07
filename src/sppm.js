var sppm = (() => {
    const APPDATA = StrokesPlus.OS.Shell.ExpandEnvironmentVariables('%APPDATA%');
    const SP_APPDATA = System.IO.Path.Combine(APPDATA, 'StrokesPlus.net');
    const NODE_MODULES = System.IO.Path.Combine(SP_APPDATA, 'node_modules');
    const PKG_JSON = "package.json";

    const sppm = {
        readPackage(abspath) {
            try {
                return JSON.parse(System.IO.File.ReadAllText(abspath));
            } catch (error) {
                sppm.notify(error, "error");
            }
        },
        install(id, opts) {
            if (typeof id === "string") {
                sppm.notify(`Installing "${id}" from npmjs`, "StrokesPlus Package Manager");
                sppm.npm(`install --save ${id}`, opts);
            } else {
                sppm.npm(`install`, opts);
            }
        }
    };

    class Plugin {
        static load(id) {
            const pkgPath = SPPM.resolve(id);
            const plugin = new Plugin(pkgPath);
            const src = plugin.source;
            __spEngineWrapper.Engine.Evaluate(src);
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
                return sppm.notify(abspath, 'File Read Error');
            }
        }
    }

    sppm.Plugin = Plugin;

    return sppm;
})();