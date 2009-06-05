using System;
using System.Threading;
using Tools.Core.Context;
using Tools.Coordination.Core;

namespace Tools.Coordination.Sample.Implementation
{
    public class JobProvider : IJobProvider<Job>
    {
        #region IJobProvider<Job> Members

        public Job GetNextItem()
        {
            Thread.Sleep(new Random().Next(100, 5000));
            return new Job
                       {
                           ContextIdentifier = new ContextIdentifier
                                                   {InternalId = new Random().Next(1000000)}
                       };
        }

        #endregion
    }
}