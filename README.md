## SPPM - Scripts Plus Plugin Manager

### Enhance the scripting capabilities of StrokesPlus with community made plugins.
Inspired by the simplicity of jQuery, there are 40+ modules that extend and enhance the scripting capabilities within action scripts.
Many common `sp.xxxx` methods are wrapped to simplify their use and some wrapped together to create new tools.

## Install
Download `sppm.dll` from here and place it in `C:\Program Files\StrokesPlus.net\Plug-Ins` to be loaded automatically. You can also load the plugin manually _or_ place it in your own plugins folder and add that path.

## Usage
There are two main Classes provided to S+
 - `SPPM` is used to manage and load plugins into the script engine.
 - `NPM` is used by `SPPM` for the heavy lifting and interacting with the file system.