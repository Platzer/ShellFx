using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShellFx.Arguments
{
    public interface INamedArgument
    {
        string Name { get; }
        string ShortCut { get; }
    }

    public class NamedPropertyData :INamedArgument
    {
        public PropertyInfo Data { get; private set; }

        public object Object { get; private set; }

        private ConverterEngine Converter { get; set; }

        public NamedPropertyData(string name, string shortCut, PropertyInfo property,object obj)
        {
            Data = property;
            Name = name;
            ShortCut = shortCut;
            Object = obj;
            Converter = new ConverterEngine();
        }

        public void SetValue(string value)
        {
            Data.SetValue(Object, Converter.Convert(Data,value));
        }

        public string Name { get; private set; }

        public string ShortCut { get; private set; }
    }
}
