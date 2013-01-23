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
            string[] args = new string[2] { "-Pfad", "c:\\hallo" };
            var parsed = new ArgumentParser<MyArgs>().Parse(args);

            Assert.AreEqual("Pfad", parsed.Pfad);
        }
    }

    public class MyArgs
    {
        [ArgumentDefinition("Pfad","p")]
        public string Pfad { get; set; }
    }
}
