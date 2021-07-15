using NUnit.Framework;
using System.IO;

namespace SPPM.Testing
{
    public class NpmTests : TestBase
    {
        [Test]
        public void TestResolvingRootPackageJson()
        {
            var pkg = SPPM.GetRootPackageJson();

            Assert.IsInstanceOf(typeof(PackageJson), pkg);
        }

        [Test]
        public void TestResolvingStrokesPlusNodeModulesPath()
        {
            string modules = Paths.StrokesPlusModules();
            string resolved = SPPM.Resolve();

            Assert.AreEqual(modules, resolved);
        }

        [Test]
        public void TestResolvingPackageIdToAbspath()
        {
            string pkgId = "@spcsp/osd-toast";
            string abspath = SPPM.Resolve(pkgId);
            string resolved = Path.Combine(Paths.StrokesPlusModules(), "@spcsp", "osd-toast");

            Assert.AreEqual(resolved, abspath);
        }

        [Test]
        public void TestResolvingModuleFromDisk()
        {
            string contents = SPPM.GetSource("@spcsp/osd-toast");

            engine.Evaluate(contents);

            AssertIsScriptObject(engine.Script.toast);
        }

        [Test]
        public void TestResolvingAndEvaluatingWithEngine()
        {
            SPPM.Load("@spcsp/osd-toast");

            AssertIsScriptObject(SPPM.Engine.Script.toast);
        }
    }
}