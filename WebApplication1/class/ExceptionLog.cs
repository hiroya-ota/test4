using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace WebApplication1
{

    class ExceptionLog
    {
        public void writeLod(string log)
        {
            ////string filePass = @"C:\Users\h2-ota\source\repos\WebApplication1\Log.txt";
            //var filePass = System.Configuration.ConfigurationManager.AppSettings["LogFilePath"].ToString();
            //if(File.Exists(filePass) == false)
            //{
            //    filePass = @"C:\Users\h2-ota\source\repos\WebApplication1\Log.txt";
            //}

            //using (FileStream fs = new FileStream(filePass, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            //using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
            //{
            //    string fmt = @"{0} {1}";
            //    sw.WriteLine(string.Format(fmt, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), log));
            //}
        }
    }
}