using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShellFx.Arguments;

namespace TestConsole
{
    [Description("Test überschrift.")]
    public class MyArgs
    {
        [Argument("Pfad", "p")]
        [Description("Der Pfad zum testen einer sehr langen beschreibung und Zeilenumbruch mit meherern Zeilen in der Beschreibung. Plus selbst eingebundene.")]
        public string Pfad { get; set; }

        [Argument("switch2", null)]
        [Description("Zweiter Test für eine lange beschreibung")]
        public bool Switch2 { get; set; }

        [Argument("switch1", null)]
        public bool Switch1 { get; set; }

        [Argument("double", null)]
        public double Double { get; set; }

        public int Int { get; set; }

        public string Text = string.Empty;

        [Action("Action1",null)]
        public void Action1()
        {
            Console.WriteLine("Action1 invoked...");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var p = new ArgumentParser<MyArgs>();
            p.PrintHelp();
            p.Invoke(args);
        }
    }
}
