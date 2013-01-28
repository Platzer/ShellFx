using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ShellFx.Arguments;

namespace ShellFxTest
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void ParserTestMultipleValues()
        {
            string[] args = new string[] { "c:\\test ordner2", "-Pfad", "c:\\test ordner", "/switch1", "c:\\test ordner2", "--double=3.2" };
            var parsed = new ArgumentParser<MyArgs>().Parse(args);

            Assert.IsTrue(parsed.Pfad == "c:\\test ordner");
            Assert.IsTrue(parsed.OutPutPfad == "c:\\test ordner2");
            Assert.IsTrue(parsed.Switch1);
            Assert.IsFalse(parsed.Switch2);
            Assert.IsTrue(3.2 == parsed.Double);
        }

        [TestMethod]
        public void ParserTestHelp()
        {
            string[] args = new string[] { "c:\\test ordner2", "-Pfad", "c:\\test ordner", "/switch1", "/switch2-", "c:\\test ordner2", "--double=3.2" };
            var parsed = new ArgumentParser<MyArgs>();
            parsed.Parse(args);

            parsed.PrintPropertyHelp();
        }
    }

    public class MyArgs
    {
        [Argument("Pfad","p")]
        public string Pfad { get; set; }

        [Argument("OutPfad", null)]
        [Position(0)]
        [DefaultValue("c:\\default ordner")]
        public string OutPutPfad { get; set; }

        [Argument("switch2",null)]
        [DefaultValue(false)]
        public bool Switch2 { get; set; }

        [Argument("switch1", null)]
        public bool Switch1 { get; set; }

        [Argument("double", null)]
        public double Double { get; set; }

        public int Int { get; set; }

        public string Text = string.Empty;
    }
}
