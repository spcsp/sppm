using NUnit.Framework;
using System.IO;

namespace SPPM.Testing
{
    public class NpmTests : TestingEngine
    {
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
    }
}