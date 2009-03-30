using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tools.TeamBuild.Tasks.UnitTests
{
    [TestFixture()]
    public class StatePersistorTests
    {
        [Test()]
        public void BreakTimeStamp_Should_ReturnTimeStamp()
        {
            IStateProvider stateProvider = MockRepository.GenerateStub<IStateProvider>();
            stateProvider.Stub<IStateProvider>((p) => p.AcquireState()).Return("30-III-2009T21:58:07;Dvoychenko Stanislav;SD@sd.com;Failure");
            StatePersistor persistor = new StatePersistor("filePath", stateProvider);

            Assert.AreEqual("30-III-2009T21:58:07", persistor.BreakDate);
            Assert.AreEqual("Dvoychenko Stanislav", persistor.BreakerDisplayName);
            Assert.AreEqual("SD@sd.com", persistor.BreakerEmailAddress);
        }
    }
}
