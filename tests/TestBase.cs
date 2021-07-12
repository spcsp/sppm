using Microsoft.ClearScript.V8;
using NUnit.Framework;

namespace SPPM.Testing
{
    public partial class TestingEngine
    {
        public V8ScriptEngine engine { get; private set; }

        [SetUp]
        public void Setup()
        {
            engine = new V8ScriptEngine(V8ScriptEngineFlags.EnableDebugging, 1988);
        }
    }
}