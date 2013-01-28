using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShellFx.Arguments
{
    public interface IConverter
    {
        object Convert(MemberInfo member, object value);
        Type Type {get;}
    }

    public class ConverterEngine
    {
        public ConverterEngine()
        {
            Converter = new Dictionary<Type, IConverter>();
            //Adding standard Converter...
            Add(new BooleanConverter());
            Add(new StringConverter());
            Add(new DoubleConverter());
        }

        Dictionary<Type, IConverter> Converter { get; set; }

        public object Convert(MemberInfo member, object value)
        {
            //TODO: den MemberTypen kontrollieren...
            return Converter[((PropertyInfo)member).PropertyType].Convert(member, value);
        }

        public void Add(IConverter converter)
        {
            if (Converter.ContainsKey(converter.Type))
                Converter[converter.Type] = converter;
            else
                Converter.Add(converter.Type, converter);
        }
    }

    #region Converter
    class BooleanConverter : IConverter
    {
        public BooleanConverter()
        {
            Type = typeof(Boolean);
        }

        public object Convert(MemberInfo member, object value)
        {
            bool ergebnis = false;

            var prop = member as PropertyInfo;
            if (prop != null && prop.PropertyType == typeof(Boolean))
            {
                string StringValue = null;
                if (value == null)
                    StringValue = null;
                else
                    StringValue = value.ToString();
                if (StringValue == null || StringValue == string.Empty || StringValue == "+" || StringValue == "True")
                    ergebnis = true;
                else if (StringValue == "-" || StringValue == "False")
                    ergebnis = false;
            }

            return ergebnis;
        }
        public Type Type { get; private set; }
    }

    class StringConverter : IConverter
    {
        public StringConverter()
        {
            Type = typeof(String);
        }

        public object Convert(MemberInfo member, object value)
        {
            return value.ToString();
        }

        public Type Type { get; private set; }
    }

    class DoubleConverter : IConverter
    {
        public DoubleConverter()
        {
            Type = typeof(Double);
        }

        public object Convert(MemberInfo member, object value)
        {
            double Result;
            if (value.GetType() == typeof(Double))
            {
                Result = (Double)value;
            }
            else
            {
                Result = System.Convert.ToDouble(value.ToString().Replace(".", ","));
            }
            return Result;
        }

        public Type Type { get; private set; }
    }
    #endregion Converter
}
