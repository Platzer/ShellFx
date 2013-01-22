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
            var parser = new ArgumentParser(typeof(MyArgs));

            foreach (var item in parser.Parse())
            {
                Console.WriteLine(item);
            }

        }
    }

    public class MyArgs
    {
        [ArgumentDefinition("Pfad","p")]
        public string Pfad { get; set; }
    }
}
