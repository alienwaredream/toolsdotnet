
namespace Tools.Coordination.Ems
{
    public class EmsReaderQueueConfiguration
    {
        public string Name { get; set; }

        public QueueType Type { get; set; }

        public string MessageSelector { get; set; }

        public bool NoLocal { get; set; }
    }
}