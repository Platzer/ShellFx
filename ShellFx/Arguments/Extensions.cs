using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace ShellFx.Arguments
{
    static class Extensions
    {
        internal static List<PropertyInfo> GetArguments(this Type t)
        {
            return (from prop in t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    where prop.GetCustomAttribute(typeof(ArgumentDefinitionAttribute),true) != null
                    select prop).ToList();
        }

        internal static List<T> GetAttributes<T>(this MemberInfo info)
        {
            return (from a in info.GetCustomAttributes(typeof(T), true)
                    select (T)a).ToList();
        }
    }
}
