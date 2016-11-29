using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioTelemonitoreo
{
    public static class Library
    {
        public static void WriteErrorLog(Exception ex)
        {
            try
            {
                var sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString(CultureInfo.CurrentCulture) + ": " + ex.Source.Trim() + "; " + ex.Message.Trim());
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void WriteErrorLog(string message)
        {
            try
            {
                var sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString(CultureInfo.CurrentCulture) + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
