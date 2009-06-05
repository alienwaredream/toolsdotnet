using Spring.Context.Support;
using Tools.Coordination.ProducerConsumer;
using Tools.Processes.Core;
using Spring.Context;
using System.Threading;

namespace Tools.Coordination.Core
{
    /// <summary>
    /// Summary description for ProducerFactory.
    /// </summary>
    public static class ProcessorFactory
    {
        private static readonly object containerSyncLock = new object();

        private static IApplicationContext containerContext;

        public static IApplicationContext ContainerContext
        {
            get
            {
                lock (containerSyncLock)
                {
                    return containerContext;
                }
            }
            set
            {
                lock (containerSyncLock)
                {
                    containerContext = value;
                }
            }
        }

        static ProcessorFactory()
        {
            containerContext = ContextRegistry.GetContext();
        }

        public static Producer CreateProducer
            (
            string name
            )
        {
            lock (containerSyncLock)
            {
                var ret = ContainerContext.GetObject(name) as Producer;
                Thread.MemoryBarrier();
                return ret;
            }
        }

        public static IProcess CreateProcess
            (
            string objectName
            )
        {
            lock (containerSyncLock)
            {
                var ret = ContainerContext.GetObject(objectName) as IProcess;
                Thread.MemoryBarrier();
                return ret;
            }
        }
    }
}