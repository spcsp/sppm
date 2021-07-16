using NUnit.Framework;
using System.IO;

namespace SPPM.Testing
{
    public class NpmTests : TestBase
    {
        [Test]
        public void TestResolvingRootPackageJson()
        {
            AssertIsPackageJson(SPPM.RootPackageJson);
        }

        [Test]
        public void TestResolvingStrokesPlusNodeModulesPath()
        {
            string modules = Paths.StrokesPlusModules;
            string resolved = SPPM.Resolve();

            Assert.AreEqual(modules, resolved);
        }

        [Test]
        public void TestResolvingPackageIdToAbspath()
        {
            string abspath = SPPM.Resolve("@spcsp/osd-toast");
            string resolved = Path.Combine(Paths.StrokesPlusModules, "@spcsp", "osd-toast");

            Assert.AreEqual(resolved, abspath);
        }

        [Test]
        public void TestResolvingModuleFromDisk()
        {
            string contents = SPPM.GetModuleSource("@spcsp/osd-toast");

            engine.Evaluate(contents);

            AssertIsScriptObject(engine.Script.toast);
        }

        public void TestResolvingAndEvaluatingWithEngine()
        {
            SPPM.Load("@spcsp/osd-toast");

            AssertIsScriptObject(SPPM.Engine.Script.toast);
        }
    }
}