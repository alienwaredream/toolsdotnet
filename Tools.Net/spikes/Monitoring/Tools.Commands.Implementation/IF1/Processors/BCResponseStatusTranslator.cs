using System;
using Tools.Core.Asserts;
using Tools.Core.Utils;

namespace Tools.Commands.Implementation.IF1.Processors
{
    /// <summary>
    /// Converts response code to the log and command status
    /// </summary>
    internal class BCResponseStatusTranslator : IResponseStatusTranslator
    {
        Tools.Commands.Implementation.IF1.Ibc.res res;

        public void SetResponse(string response)
        {
            ErrorTrap.AddRaisableAssertion<ArgumentException>(!String.IsNullOrEmpty(response), "response argument can't be empty or null!");

            res = SerializationUtility.DeserializeFromString(response, typeof(Tools.Commands.Implementation.IF1.Ibc.res)) as Tools.Commands.Implementation.IF1.Ibc.res;

            ErrorTrap.AddRaisableAssertion<ArgumentException>(res != null, "Invalid response type for this translator! Expected type is " + typeof(Tools.Commands.Implementation.IF1.Ibc.res).FullName + "\r\nSource:\r\n" + response);
        }

        #region IResponseStatusTranslator Members

        /// <summary>
        /// Returns status as should be witten in the command table.
        /// Command table allows to have longer status names that include 
        /// DONE, FAILED, BAD, etc.
        /// </summary>
        /// <param name="response">A response as per IncreaseBC schema.</param>
        /// <returns>Status code to written to the commmand table</returns>
        public string CommandStatus
        {
            get
            {
                return (res.code == "ok") ? "DONE" : "FAILED";
            }
        }
        /// <summary>
        /// Returns status as should be written in the response log table.
        /// This is limited to one letter and is "E" for error and "P" for Processed.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public string LogStatus
        {
            get
            {
                return (res.code == "ok") ? "P" : "E";
            }
        }
        public string Description
        {
            get
            {
                return res.desc;
            }
        }
        #endregion
    }
}