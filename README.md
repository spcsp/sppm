## ScriptsPlus, a StrokesPlus.net Plugin

### Enhance the scripting capabilities of S+ with a rich module library.
Inspired by the simplicity of jQuery, there are 40+ modules that extend and enhance the scripting capabilities within action scripts.
Many common `sp.xxxx` methods are wrapped to simplify their use and some wrapped together to create new tools.

# Install
Download `ScriptsPlusPlugin.dll` from here and place it in `C:\Program Files\StrokesPlus.net\Plug-Ins` to be automatically picked up by S+
You can also add it manually, or place it in your own plugins folder and add that path.

# Loading
 - Head to `Global Actions`
 - Open the `Load/Unload` tab
 - Check the box to enable the load script
 - Paste in this snippet: `var $ = ScriptsPlus();`
 - Profit!

# Examples

### Wrapped Methods
```javascript
// wraps `sp.MessageBox()`
$.alert("Hello World!");

// wraps `new DisplayTextInfo()`
$.toast("Hello World!");

// wraps `sp.sp.ShowBalloonTip()`
$.balloon("Hello World!");
```

### Dialogs
```javascript
function getUserInput() {
  var modal = $.dialog.create("Text Input Demo");

  modal.show(input => $.alert(input, "Input"));
}

getUserInput();
```

### Popup Menus
```javascript
var { addToMenu, addToSubMenu, menuItem, show } = $.popup;

var popup = $.popup.create();
var addToMainMenu = $.popup.addToMenu(popup);

var mastercam = menuItem("Mastercam");
var addToDemo = $.popup.addToSubMenu(mastercam);

addToDemo(menuItem("Hello World", `$.alert('Hello World')`));
addToDemo(menuItem("Taco Bell", `$.alert('Taco Bell')`));
addToDemo(menuItem("Is Good", `$.alert('Is Good')`));

addToMainMenu(mastercam);
addToMainMenu($.popup.spacer);
addToMainMenu($.popup.cancel);

show(popup);
```