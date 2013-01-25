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
        internal static List<T> GetArguments<T>(this Type t) where T: MemberInfo
        {
            return (from memb in t.GetMembers(BindingFlags.Instance | BindingFlags.Public)
                    where memb.GetCustomAttribute(typeof(ArgumentAttribute), true) != null && memb is T
                    select memb as T).ToList();
        }

        internal static List<PropertyData> GetPropertyData(this object obj)
        {
            var members = obj.GetType().GetArguments<PropertyInfo>();

            return (from m in members
                    where m.MemberType == MemberTypes.Property
                    let att = m.GetCustomAttribute<ArgumentAttribute>(true)
                    select new PropertyData(att.Name,
                                                 att.ShortCut,
                                                 m as PropertyInfo,
                                                 obj)).ToList();
        }
    }
}
