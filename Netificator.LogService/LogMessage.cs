using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.LogService
{

    [Serializable]
    public class LogMessage
    {
        public Guid MessageId { get; set; }

        public string Message { get; set; }

        public LogMessageExceptionItem Exception { get; set; }

        public LogMessageSeverities Severity { get; set; }

        public DateTime DateAddedAtUser { get; set; }
    }

    [Serializable]
    public class LogMessageExceptionItem
    {
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public LogMessageExceptionItem InnerException { get; set; }

        public LogMessageExceptionItem(Exception ex)
        {
            Message = ex.Message;
            StackTrace = ex.StackTrace;

            if (ex.InnerException != null)
            {
                InnerException = new LogMessageExceptionItem(ex.InnerException);
            }
        }

        public LogMessageExceptionItem()
        {

        }
    }

    public enum LogMessageSeverities
    {
        /// <summary>
        /// Information.
        /// </summary>
        Information = 1,

        /// <summary>
        /// Warning.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Error.
        /// </summary>
        Error = 3,

        /// <summary>
        /// A catastrophic failure. Should represent errors that had cause the application to close.
        /// </summary>
        CatastrophicFailure = 4
    }

}
