using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginControl.Utilities;
using System.IO;

namespace LoginControl.Logs
{
    public class ExceptionLog
    {

        #region Variables

        static private DateTime dtCurrentDate = DateTime.Now;
        static private string strErrorLogDateTime = dtCurrentDate.ToLongDateString() + " " + dtCurrentDate.ToLongTimeString();
        static private string strDateFormat = dtCurrentDate.Year.ToString() + dtCurrentDate.Month.ToString() + dtCurrentDate.Day.ToString();


        #endregion

        #region ExceptionLog Constructor

        /// <summary>
        /// 
        ///  ExceptionLog Constructor
        ///  strErroLogDateTime - Error Created Time
        ///  strDateFormat - Error Filename as ErrorDate
        /// </summary>
        public ExceptionLog()
        {
            strErrorLogDateTime = dtCurrentDate.ToLongDateString() + " " + dtCurrentDate.ToLongTimeString();
            strDateFormat = dtCurrentDate.Year.ToString() + dtCurrentDate.Month.ToString() + dtCurrentDate.Day.ToString();
        }

        #endregion

        #region Write Log file to FileSystem
        /// <summary>
        /// Writes log file to Filesystem
        /// </summary>
        /// <param name="strErrorMethodName"></param>
        /// <param name="strErrorMessage"></param>
        static public void WriteLog(string pageName, string error)
        {
            string savepath = CommonUtils.GetExceptionPath();
            if (savepath.Equals(CommonUtils.DIRECTORY_NOT_EXISTS))
            {
                //savepath = "C:/Exceptions";
                if (!Directory.Exists(savepath))
                    Directory.CreateDirectory(savepath);

            }
            else if (!Directory.Exists(savepath))
                Directory.CreateDirectory(savepath);
            StreamWriter sw = new StreamWriter(savepath + strDateFormat + ".txt", true);

            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine(strErrorLogDateTime);
            sw.WriteLine(pageName + " : ");
            sw.WriteLine(error);
            sw.WriteLine("");
            sw.Flush();
            sw.Close();

        }

        #endregion
    }
}