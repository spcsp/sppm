using Microsoft.ClearScript;
using NUnit.Framework;

namespace SPPM.Testing
{
    public class EngineTest : TestBase
    {
        [Test]
        public void TestEvaluatingFunctionDeclaration()
        {
            engine.Evaluate(@"function foo() { return `bar`; }");

            Assert.IsInstanceOf(typeof(ScriptObject), engine.Script.foo);
        }

        [Test]
        public void TestEvaluatingIIFEReturningString()
        {
            string res = engine.Evaluate(@"(() => `bar`)();").ToString();

            Assert.AreEqual("bar", res);
        }

        [Test]
        public void TestEvaluatingLodashModule()
        {
            engine.Evaluate(Properties.Resources.lodash_min);
            Assert.IsInstanceOf(typeof(ScriptObject), engine.Script._);

            engine.Evaluate(@"var one = _.min([1, 2, 3]);");
            Assert.AreEqual(1, engine.Script.one);

            engine.Evaluate(@"var three = _.max([1, 2, 3]);");
            Assert.AreEqual(3, engine.Script.three);
        }
    }
}