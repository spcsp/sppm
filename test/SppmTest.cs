using NUnit.Framework;
using System;
using System.IO;

namespace SPPM.Testing
{
    public class NpmTests
    {
        [SetUp]
        public void Setup()
        {
            //
        }

        [Test]
        public void TestResolvingStrokesPlusNodeModulesPath()
        {
            string modules = PathHelpers.StrokesPlusModules();
            string resolved = SPPM.Resolve();

            Assert.AreEqual(modules, resolved);
        }

        [Test]
        public void TestResolvingPackageIdToAbspath()
        {
            string pkgId = "@spcsp/osd-toast";
            string abspath = SPPM.Resolve(pkgId);
            string resolved = Path.Combine(PathHelpers.StrokesPlusModules(), "@spcsp", "osd-toast");

            Assert.AreEqual(resolved, abspath);
        }
    }

    public static class PathHelpers
    {
        public static string StrokesPlusConfigRoot()
        {
            return Path.Combine("C:\\", "Users", Environment.UserName, "AppData", "Roaming", "StrokesPlus.net");
        }

        public static string StrokesPlusModules()
        {
            return Path.Combine(StrokesPlusConfigRoot(), "node_modules");
        }
    }
}