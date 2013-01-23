using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace ShellFx.Arguments
{
    public abstract class ParserBase<T> where T : new()
    {
        protected Type Type { get; private set; }

        public Dictionary<string, string> Arguments { get; set; }

        public ParserBase()
        {
            Type = typeof(T);
            Arguments = new Dictionary<string, string>();
        }
    }

    public class ArgumentParser<T> : ParserBase<T> where T : new()
    {
        public ArgumentParser() : base() { }

        public T Parse(string[] args)
        {
            T Ergebnis = new T();

            var ArgumentsToSet = Type.GetArguments();

            ParseInternalArguments(args);

            return Ergebnis;
        }

        private void ParseInternalArguments(string[] args)
        {
            //(?<sign>--|-|/)(?<arg>[\w]{1,})(((?<sep>:|=)(?<val>.*))|(?<val>\+|\-))? - Pattern
            Regex RegexCurrentArgNull = new Regex(@"(?<sign>--|-|/)(?<arg>[\w]{1,})(((?<sep>:|=)(?<val>.*))|(?<val>\+|\-))?");

            string currentArg = null;
            foreach (var arg in args)
            {
                var matches = RegexCurrentArgNull.Matches(arg);
                if (matches.Count >= 1)
                {
                    if (matches.Count == 1)
                    {
                        currentArg = matches[0].Groups["arg"].Value;
                        Arguments.Add(currentArg, null);

                        if (!string.IsNullOrEmpty(matches[0].Groups["val"].Value))
                        {
                            Arguments[currentArg] = matches[0].Groups["val"].Value;
                            currentArg = null;
                        }
                    }
                    else
                    {
                        Arguments.Add((Arguments.Count).ToString(), arg);
                        continue;
                    }
                }
                else
                {
                    if (currentArg == null)
                        Arguments.Add((Arguments.Count).ToString(), arg);
                    else
                    {
                        Arguments[currentArg] = arg;
                        currentArg = null;
                    }
                    continue;
                }
            }
        }
    }
}
