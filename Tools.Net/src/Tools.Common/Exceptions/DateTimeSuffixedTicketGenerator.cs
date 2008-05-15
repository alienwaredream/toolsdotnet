using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Tools.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class DateTimeSuffixedTicketGenerator : ITicketGenerator
    {
        #region Attributes
        private string suffix;
        private string dateFormat;
        //private ReaderWriterLock rwLock;
        //private bool suffixAcquired = false;
        #endregion

        public DateTimeSuffixedTicketGenerator()
        {
            //rwLock = new ReaderWriterLock();
            suffix = "TstSfx"; // TODO: Acquire from config
            dateFormat = "ddMMyyHHmmss"; // TODO: Acquire from config
        }
        public DateTimeSuffixedTicketGenerator(string suffix, string dateFormat)
        {
            //rwLock = new ReaderWriterLock();
            this.suffix = suffix;
            this.dateFormat = dateFormat;
        }
        #region ITicketGenerator Members

        public string CreateTicket()
        {
            //try
            //{
                //rwLock.AcquireReaderLock();
                //if (!suffixAcquired)
                //{
                    //LockCookie lc = rwLock.UpgradeToWriterLock();
                    
                //    rwLock.DowngradeFromWriterLock(lc);
                //}


                return DateTime.Now.ToString(dateFormat) + suffix;
            //}
            //finally
            //{
            //    rwLock.
            //}
        }

        #endregion
    }
}
