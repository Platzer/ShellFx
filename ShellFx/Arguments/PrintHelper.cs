using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellFx.Arguments
{
    public class PrintHelper
    {
        protected int Width { get; private set; }

        public PrintHelper() : this(Console.WindowWidth) { }

        public PrintHelper(int width)
        {
            Width = width;
        }

        public void PrintPropertiesHelp(TextWriter writer, List<PropertyData> properties)
        {
            var tabelle = (from p in properties
                           select new string[] { string.IsNullOrEmpty(p.ShortCut) ? p.Name : string.Format("-{0},(-{1})",p.Name,p.ShortCut), 
                                                 p.Data.PropertyType.ToString(),
                                                 p.Position.ToString(),
                                                 p.Description }).ToArray();

            int name = (from t in tabelle
                        orderby t[0].Length
                        select t[0].Length).FirstOrDefault();
        }

        private void WritePropertyHelp(TextWriter writer, PropertyData property)
        {
        }
    }
}
