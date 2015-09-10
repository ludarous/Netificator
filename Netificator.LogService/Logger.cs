using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Netificator.LogService
{
    public class Logger
    {
        /// <summary>
        /// Logs a message to xml file.
        /// </summary>
        /// <param name="message">Message text.</param>
        /// <param name="exception">Exception reference.</param>
        /// <param name="severity">Message severity</param>
        public static void LogMessageToXml(string message = "", Exception exception = null, LogMessageSeverities severity = LogMessageSeverities.Information)
        {
            try
            {

                var filename = string.Format("Log.xml");

                var logMessage = new LogMessage();
                logMessage.MessageId = Guid.NewGuid();
                logMessage.Message = string.IsNullOrWhiteSpace(message) ? string.Empty : message;
                if (exception != null)
                {
                    logMessage.Exception = new LogMessageExceptionItem(exception);
                    if (exception.InnerException != null)
                    {
                        Exception innerEx = exception.InnerException;
                        //Fraus.FlexiBooks.Business.Services.LogService.Current.LogMessageToXml(innerEx.Message, innerEx, Business.LogMessageSeverities.Error);
                    }
                }
                logMessage.Severity = severity;
                logMessage.DateAddedAtUser = DateTime.Now;

                FileInfo fileInf = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename));
                FileStream str;
                if (fileInf.Exists)
                {
                    str = new FileStream(fileInf.FullName, FileMode.Append);
                }
                else
                {
                    str = new FileStream(fileInf.FullName, FileMode.CreateNew);
                }

                using (var stream = str)
                {
                    //var formatter = new BinaryFormatter();
                    var formatter = new XmlSerializer(typeof(LogMessage));
                    formatter.Serialize(stream, logMessage);
                }
            }
            catch { }
        }
    }
}
