using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellFx.Arguments
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ArgumentAttribute : Attribute
    {
        public ArgumentAttribute(string name, string shortCut)
        {
            Name = name;
            ShortCut = shortCut;
        }

        public string Name { get; private set; }

        public string ShortCut { get; private set; }
    }

    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string description)
        {
            Description = description;
        }
        public string Description { get; private set; }
    }
}
