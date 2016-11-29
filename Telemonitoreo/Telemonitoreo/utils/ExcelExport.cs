using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Telemonitoreo.Utils
{
    public class ExcelExport
    {
        /// <summary>
        /// This method export classic csv file.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        public static void ExportToSpreadsheet(DataTable table, string name)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            var separator = (char)9;

            foreach (DataColumn column in table.Columns)
            {
                context.Response.Write(String.Concat(column.ColumnName.Replace("_", " "), separator));
            }
            context.Response.Write(Environment.NewLine);
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    context.Response.Write(string.Concat(row[i].ToString().Replace(separator.ToString(), string.Empty), separator));
                }
                context.Response.Write(Environment.NewLine);
            }
            context.Response.ContentType = "text/csv";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + name + ".xls");
            context.Response.End();
        }
    }
}