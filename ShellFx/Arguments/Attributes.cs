using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellFx.Arguments
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ArgumentDefinitionAttribute : Attribute
    {
        public string Name { get; private set; }
        public string ShortCut { get; private set; }

        public ArgumentDefinitionAttribute(string name, string shortCut)
        {
            Name = name;
            ShortCut = shortCut;
        }
    }
}
