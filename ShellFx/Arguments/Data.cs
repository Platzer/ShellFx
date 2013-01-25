using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShellFx.Arguments
{
    public class PropertyData
    {
        public PropertyInfo Data { get; private set; }

        public object Object { get; private set; }

        private ConverterEngine Converter { get; set; }

        public PropertyData(string name, string shortCut, PropertyInfo property, object obj, string description = null, int? position = null, bool isRequired = false)
        {
            Data = property;
            Name = name;
            ShortCut = shortCut;
            Object = obj;
            Converter = new ConverterEngine();
            Description = description;
            Position = position;
            IsRequired = isRequired;
        }

        public void SetValue(string value)
        {
            Data.SetValue(Object, Converter.Convert(Data,value));
        }

        public string Name { get; private set; }

        public string ShortCut { get; private set; }

        public string Description { get; private set; }

        public int? Position { get; private set; }

        public bool IsAnonymous
        {
            get
            {
                return ShortCut == null && Name == null;
            }
        }

        public bool IsRequired { get; private set; }
    }
}
