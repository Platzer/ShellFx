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
            string[] args = new string[] { "-Pfad", "c:\\test ordner", "/switch1", "/switch2-", "c:\\test ordner2" };
            var parser = new ArgumentParser<MyArgs>();
            parser.Parse(args);

            Assert.IsTrue(parser.Arguments.Count > 0);
        }
    }

    public class MyArgs
    {
        [ArgumentDefinition("Pfad","p")]
        public string Pfad { get; set; }
    }
}
