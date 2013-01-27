﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;

namespace ShellFx.Arguments
{
    public abstract class ParserBase<T> where T : new()
    {
        protected Type Type { get; private set; }

        protected T Result { get; private set; }

        protected Dictionary<string, string> Parameter { get; set; }

        private List<PropertyData> properties = null;
        protected List<PropertyData> Properties
        {
            get
            {
                if (properties == null)
                {
                    properties = new List<PropertyData>();
                    ParseInternalArguments();
                }
                return properties;
            }
            set
            {
                properties = value;
            }
        }

        public ParserBase()
        {
            Type = typeof(T);
            Parameter = new Dictionary<string, string>();
            Result = new T();
        }

        protected void ParseInternalArguments()
        {
            var members = Type.GetArguments<MemberInfo>();

            Properties = Result.GetPropertyData();
        }

        protected virtual void ParseInternalParameter(string[] args)
        {
        }
    }

    public class ArgumentParser<T> : ParserBase<T> where T : new()
    {
        public ArgumentParser() : base() { }

        public T Parse(string[] args)
        {
            ParseInternalParameter(args);

            SetInternalProperties();

            return Result;
        }

        private PrintHelper printHelper;
        private PrintHelper PrintHelper
        {
            get
            {
                if (printHelper == null) { printHelper = new PrintHelper(); }
                return printHelper;
            }
            set
            {
                printHelper = value;
            }
        }

        public void PrintPropertyHelp()
        {
            PrintPropertyHelp(Console.Out);
        }

        public void PrintPropertyHelp(TextWriter writer)
        {
            PrintHelper.PrintPropertiesHelp(writer, Properties);
        }

        public void PrintHead()
        {
            PrintHead(Console.Out);
        }

        public void PrintHead(TextWriter writer)
        {
            var Attr = Type.GetCustomAttribute<DescriptionAttribute>();
            if (Attr != null)
                PrintHelper.PrintHead(writer, Attr.Description);
        }

        public void PrintHelp()
        {
            PrintHelp(Console.Out);
        }

        public void PrintHelp(TextWriter writer)
        {
            PrintHead(writer);
            PrintPropertyHelp(writer);
        }

        private void SetInternalProperties()
        {
            foreach (var item in Properties)
            {
                //TODO: Casesensetivity einbauen...
                var Werte = from d in Parameter
                            where string.Compare(d.Key, item.Name, true) == 0 || string.Compare(d.Key, item.ShortCut, true) == 0
                            select d.Value;
                //TODO: Wert mehrmals gesetzt...
                if (Werte.Count() == 1)
                {
                    item.SetValue(Werte.First());
                }
            }
        }

        protected override void ParseInternalParameter(string[] args)
        {
            Regex RegexCurrentArgNull = new Regex(@"(?<sign>--|-|/)(?<arg>[\w]{1,})(((?<sep>:|=)(?<val>.*))|(?<val>\+|\-))?");

            string currentArg = null;
            foreach (var arg in args)
            {
                var matches = RegexCurrentArgNull.Matches(arg);
                if (matches.Count >= 1)
                {
                    if (matches.Count == 1)
                    {
                        currentArg = matches[0].Groups["arg"].Value.ToLower();
                        Parameter.Add(currentArg, null);

                        if (!string.IsNullOrEmpty(matches[0].Groups["val"].Value))
                        {
                            Parameter[currentArg] = matches[0].Groups["val"].Value;
                            currentArg = null;
                        }
                    }
                    else
                    {
                        Parameter.Add((Parameter.Count).ToString(), arg);
                        continue;
                    }
                }
                else
                {
                    if (currentArg == null)
                        Parameter.Add((Parameter.Count).ToString(), arg);
                    else
                    {
                        Parameter[currentArg] = arg;
                        currentArg = null;
                    }
                    continue;
                }
            }
        }
    }
}