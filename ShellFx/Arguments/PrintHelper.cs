using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellFx.Arguments
{
    class PrintHelper
    {
        protected int Width { get; private set; }

        public PrintHelper() : this(Console.WindowWidth) { }

        public PrintHelper(int width)
        {
            Width = width;
        }

        public void PrintHead(TextWriter writer, string head)
        {
            writer.WriteLine(head);
        }

        public void PrintPropertiesHelp(TextWriter writer, List<PropertyData> properties)
        {
            string[][] tabelle = new string[properties.Count() + 1][];
            tabelle[0] = new string[] { "OPTION", "TYPE", "POSITION", "DESCRIPTION" };

            var temptabelle = from p in properties
                              select new string[] { string.IsNullOrEmpty(p.ShortCut) ? string.Format("-{0}",p.Name) : string.Format("-{0},(-{1})",p.Name,p.ShortCut), 
                                                    p.Data.PropertyType.Name,
                                                    p.Position.HasValue ? p.Position.Value.ToString() : string.Empty,
                                                    p.Description != null ? p.Description : string.Empty};
            int i = 1;
            foreach (var item in temptabelle)
            {
                tabelle[i] = item;
                i++;
            }

            int name = (from t in tabelle
                        orderby t[0].Length descending
                        select t[0].Length).FirstOrDefault();
            int type = (from t in tabelle
                        orderby t[1].Length descending
                        select t[1].Length).FirstOrDefault();
            int position = (from t in tabelle
                            orderby t[2].Length descending
                            select t[2].Length).FirstOrDefault();
            int description = (from t in tabelle
                               orderby t[3].Length descending
                               select t[3].Length).FirstOrDefault();

            int Buffer = 3;
            var formatString = GetPropertiesHelpFormatString(name, type, position, description, Buffer);
            
            foreach (var item in tabelle)
            {
                if (name + type + position + description > Width - 1)
                {
                    var words = item[3].Split(' ');
                    var SB = new StringBuilder();
                    int wrapPos = name + type + position + Buffer * 4;
                    int aktPos = wrapPos;
                    foreach (var word in words)
                    {
                        if (aktPos + word.Length < Width - 1)
                        {
                            SB.Append(word);
                            SB.Append(" ");
                            aktPos += word.Length + 1;
                        }
                        else
                        {
                            SB.AppendLine();
                            SB.Append(new String(' ', wrapPos));
                            SB.Append(word);
                            SB.Append(" ");
                            aktPos = wrapPos + word.Length + 1;
                        }
                    }

                    writer.WriteLine(string.Format(formatString, item[0], item[1], item[2], SB.ToString()));
                }
                else
                {
                    writer.WriteLine(string.Format(formatString, item[0], item[1], item[2], item[3]));
                }
            }
        }

        string GetPropertiesHelpFormatString(int nameLength, int typeLength, int positionLength, int descriptionLength, int buffer)
        {
            int Buffer = buffer;
            
            var SB = new StringBuilder();
            SB.Append(new String(' ', Buffer));
            SB.Append("{0,-");
            SB.Append(nameLength + Buffer);
            SB.Append("}{1,-");
            SB.Append(typeLength + Buffer);
            SB.Append("}{2,-");
            SB.Append(positionLength + Buffer);
            SB.Append("}{3");
            SB.Append("}");

            return SB.ToString();
        }
    }
}
