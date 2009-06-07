
using TIBCO.EMS;
namespace Tools.Coordination.Ems
{
    public class SessionConfiguration
    {
        public bool IsTransactional { get; set; }

        private SessionMode mode = SessionMode.AutoAcknowledge;

        public SessionMode Mode { get { return mode; } set { mode = value; } }
    }
}
