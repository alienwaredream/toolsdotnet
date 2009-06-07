
namespace Tools.Coordination.Ems
{
    public class ServerConfiguration
    {
        public string Url { get; set; }

        public string ClientId { get; set; }

        public int ReconnectAttempts { get; set; }

        public string AuthenticationSectionName { get; set; }
    }
}