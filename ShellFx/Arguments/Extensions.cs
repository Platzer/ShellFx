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
        internal static List<T> GetNamedArguments<T>(this Type t) where T: MemberInfo
        {
            return (from memb in t.GetMembers(BindingFlags.Instance | BindingFlags.Public)
                    where memb.GetCustomAttribute(typeof(NamedArgumentAttribute), true) != null && memb is T
                    select memb as T).ToList();
        }
    }
}
