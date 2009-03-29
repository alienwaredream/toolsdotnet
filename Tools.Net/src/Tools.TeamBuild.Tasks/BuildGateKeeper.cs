using System;
using System.IO;
using System.Diagnostics;

namespace Tools.TeamBuild.Tasks
{
    public class BuildGateKeeper : Microsoft.Build.Utilities.Task
    {
        #region Input Properties

        [Microsoft.Build.Framework.Required()]
        public BuildStatus BuildStatus { get; set; }

        [Microsoft.Build.Framework.Required()]
        public string RequestorMailAddress { get; set; }
        [Microsoft.Build.Framework.Required()]
        public string RequestorDisplayName { get; set; }

        public string DateFormat { get; set; }
        /// <summary>
        /// Full path to the file where the breaker status is kept.
        /// </summary>
        /// <remarks>If file doesn't exist it will be created.</remarks>
        public string StateFilePath { get; set; }

        internal IStatePersistor StatePersistor { get; set; }

        #endregion

        #region Output Properties

        [Microsoft.Build.Framework.Output()]
        public string BreakerMailAddress { get; set; }
        [Microsoft.Build.Framework.Output()]
        public string BreakerDisplayName { get; set; }
        [Microsoft.Build.Framework.Output()]
        public string BreakTimeStamp { get; set; }

        #endregion

        #region Properties

        internal IDateProvider DateProvider { get; set; }

        #endregion

        #region Constructors

        public BuildGateKeeper()
            : base()
        {

            DateProvider = new DateProvider();
            DateFormat = "dd-MMM-yyyyTHH:mm:ss";
        }

        #endregion

        public override bool Execute()
        {
            try
            {
                if (StatePersistor == null)
                {
                    StatePersistor = new StatePersistor(StateFilePath);
                }
                // If build is a success, then clean the state
                if (BuildStatus == BuildStatus.Success)
                {
                    
                    StatePersistor.CleanState();

                    Debug.WriteLine("**State cleaned");

                    return true;
                }
                // There is a break that happened before, set the original breaker details
                if (BuildStatus == BuildStatus.Failure && StatePersistor.ContainsBreak)
                {
                    BreakerDisplayName = StatePersistor.BreakerDisplayName;
                    BreakerMailAddress = StatePersistor.BreakerEmailAddress;
                    BreakTimeStamp = StatePersistor.BreakDate;
                    return true;
                }
                // If there is no state, then it means that this break is the first,
                // persist it in the state and set breaker details to the requestor.
                if (BuildStatus == BuildStatus.Failure && !StatePersistor.ContainsBreak)
                {
                    string stateTemp = String.Format("{0};{1};{2};{3}",
                        DateProvider.GetTimeStamp().ToString(DateFormat), RequestorDisplayName, RequestorMailAddress,
                        BuildStatus);
                    Debug.WriteLine("**State:" + stateTemp);
                    StatePersistor.WriteState(stateTemp);

                    BreakerDisplayName = RequestorDisplayName;
                    BreakerMailAddress = RequestorMailAddress;

                }

                return true;

            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex, true);
                return false;
            }
        }


    }
}
