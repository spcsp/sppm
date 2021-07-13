using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using NUnit.Framework;

namespace SPPM.Testing
{
    public partial class TestBase
    {
        public V8ScriptEngine engine { get; private set; }

        [SetUp]
        public void Setup()
        {
            engine = new V8ScriptEngine(V8ScriptEngineFlags.EnableDebugging, 1988);
        }

        internal void AssertIsScriptObject(object obj)
        {
            Assert.IsInstanceOf(typeof(ScriptObject), obj);
        }
    }
}