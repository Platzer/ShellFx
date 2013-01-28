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

    public class DefaultValueAttribute : Attribute
    {
        public object Value { get; private set; }
        public DefaultValueAttribute(object value)
        {
            Value = value;
        }
    }

    public class PositionAttribute : Attribute
    {
        public int Position { get; private set; }
        public PositionAttribute(int position)
        {
            Position = position;
        }
    }

    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string description)
        {
            Description = description;
        }
        public string Description { get; private set; }
    }

    public class ActionAttribute : Attribute
    {
        public ActionAttribute(string name, string shortCut)
        {
            Name = name;
            ShortCut = shortCut;
        }

        public string Name { get; private set; }

        public string ShortCut { get; private set; }
    }
}