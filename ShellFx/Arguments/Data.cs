using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShellFx.Arguments
{
    public class DataBase
    {
        public string Name { get; protected set; }

        public string ShortCut { get; protected set; }

        public string Description { get; protected set; }

        public int? Position { get; protected set; }

        public object Object { get; protected set; }

        public bool IsAnonymous
        {
            get
            {
                return ShortCut == null && Name == null;
            }
        }

        public bool IsRequired { get; protected set; }
    }

    public class PropertyData : DataBase
    {
        public PropertyInfo Data { get; private set; }

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
    }

    public class ActionData : DataBase
    {
        public ActionData(string name, string shortCut, MethodInfo method, object obj, string description = null, int? position = null, bool isRequired = false)
        {
            Data = method;
            Name = name;
            ShortCut = shortCut;
            Object = obj;
            Description = description;
            Position = position;
            IsRequired = isRequired;
        }

        public void Invoke()
        {
            Data.Invoke(Object, null);
        }

        public MethodInfo Data { get; private set; }
    }
}