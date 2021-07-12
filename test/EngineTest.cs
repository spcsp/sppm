using NUnit.Framework;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;

namespace SPPM.Testing
{
    public class EngineTest
    {
        private const int debugPort = 1988;

        public V8ScriptEngine engine { get; private set; }

        [SetUp]
        public void Setup()
        {
            engine = new V8ScriptEngine(V8ScriptEngineFlags.EnableDebugging, debugPort);
        }

        [Test]
        public void TestEvaluatingSimpleFunctionDeclaration()
        {            
            engine.Evaluate(@"function foo() { return `bar`; }");

            Assert.IsInstanceOf(typeof(ScriptObject), engine.Script.foo);
        }

        [Test]
        public void TestExecutingArrowFunctionReturningString()
        {
            string res = engine.Evaluate(@"(() => `bar`)();").ToString();

            Assert.AreEqual("bar", res);
        }
    }
}