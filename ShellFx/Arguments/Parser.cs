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

        protected Dictionary<string, string> Arguments { get; set; }

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



            return Ergebnis;
        }

        private void ParseInternalArguments(string[] args)
        {
            //(?<sign>--|-|/)(?<arg>[\w]{1,})((?<sep>:|=)(?<val>.*))? - Pattern
            Regex RegexCurrentArgNull = new Regex(string.Format("(?<sign>{2}|{1}|{0})(?<arg>[\\w]{1,})((?<sep>{3}|{4})(?<val>.*))?", 
                                                                Constants.ArgumentPräfixWindows, 
                                                                Constants.ArgumentPräfixUnixShort, 
                                                                Constants.ArgumentPräfixUnixLong,
                                                                Constants.ArgumentSplitterWindows,
                                                                Constants.ArgumentSplitterUnix));
            string currentArg = null;
            foreach (var arg in args)
            {
                if (currentArg == null)
                {
                    var matches = RegexCurrentArgNull.Matches(arg);
                    if (matches.Count == 1)
                    {
                        currentArg = matches[0].Groups["arg"].Value;
                        Arguments.Add(currentArg, null);

                        if (!string.IsNullOrEmpty(matches[0].Groups["value"].Value))
                        {
                            Arguments[currentArg] = matches[0].Groups["value"].Value;
                            currentArg = null;
                        }
                    }
                    else
                    {
                        Arguments.Add(null, arg);
                        continue;
                    }
                }
                else
                {

                }
            }
        }
    }
}
