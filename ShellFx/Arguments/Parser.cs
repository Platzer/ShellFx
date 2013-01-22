using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellFx.Arguments
{
    public abstract class ParserBase
    {
        protected Type Type {get; private set;}

        protected Dictionary<string, string> Arguments { get; set; }

        public ParserBase(Type type)
        {
            Type = type;
        }
    }

    public class ArgumentParser : ParserBase
    {
        public ArgumentParser(Type type) : base(type) { }

        public IEnumerable<string> Parse()
        {
            List<string> Ergebnis = new List<string>();

            foreach (var item in Type.GetArguments())
            {
                Ergebnis.Add(string.Format("{0} - {1} - {2} - {3}", item.Name, item.PropertyType, item.GetAttributes<ArgumentDefinitionAttribute>()[0].ShortCut, item.GetAttributes<ArgumentDefinitionAttribute>()[0].Name));
            }

            return Ergebnis;
        }
    }
}
