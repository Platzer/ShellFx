using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.Reflection;

namespace ShellFx.Arguments
{
    public abstract class ParserBase<T> where T : new()
    {
        protected Type Type { get; private set; }

        protected T Result { get; private set; }

        protected Dictionary<string, string> Parameter { get; set; }

        protected List<NamedPropertyData> NamedProperties { get; set; }

        public ParserBase()
        {
            Type = typeof(T);
            Parameter = new Dictionary<string, string>();
            Result = new T();
        }

        protected void ParseInternalNamedArguments()
        {
            var members = Type.GetNamedArguments<MemberInfo>();

            NamedProperties = Result.GetNamedPropertyData();
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
            ParseInternalNamedArguments();

            SetInternalNamedProperties();

            return Result;
        }

        private void SetInternalNamedProperties()
        {
            foreach (var item in NamedProperties)
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