using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ShellFx.Arguments;

namespace ShellFxTest
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string[] args = new string[] {"c:\\test ordner2", "-Pfad", "c:\\test ordner", "/switch1", "/switch2-", "c:\\test ordner2", "--double=3.2" };
            var parsed = new ArgumentParser<MyArgs>().Parse(args);

            Assert.IsTrue(parsed.Pfad == "c:\\test ordner");
            Assert.IsTrue(parsed.Switch1);
            Assert.IsFalse(parsed.Switch2);
            Assert.IsTrue(3.2 == parsed.Double);
        }
    }

    public class MyArgs
    {
        [NamedArgument("Pfad","p")]
        public string Pfad { get; set; }

        [NamedArgument("switch2",null)]
        public bool Switch2 { get; set; }

        [NamedArgument("switch1", null)]
        public bool Switch1 { get; set; }

        [NamedArgument("double", null)]
        public double Double { get; set; }
    }
}
