using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShellFx.Arguments;

namespace TestConsole
{
    public class MyArgs
    {
        [Argument("Pfad", "p")]
        [ArgumentDescription("Der Pfad zum testen einer sehr langen beschreibung und Zeilenumbruch")]
        public string Pfad { get; set; }

        [Argument("switch2", null)]
        public bool Switch2 { get; set; }

        [Argument("switch1", null)]
        public bool Switch1 { get; set; }

        [Argument("double", null)]
        public double Double { get; set; }

        public int Int { get; set; }

        public string Text = string.Empty;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var p = new ArgumentParser<MyArgs>();
            p.PrintPropertyHelp();
        }
    }
}
