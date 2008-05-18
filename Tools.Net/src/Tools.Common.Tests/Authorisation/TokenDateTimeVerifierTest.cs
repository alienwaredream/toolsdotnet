using Tools.Common.Authorisation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.Common.Config;
using System;
using Rhino.Mocks;
using System.Globalization;
using Tools.Common.Utils;

namespace Tools.Common.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for TokenDateTimeVerifierTest and is intended
    ///to contain all TokenDateTimeVerifierTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TokenDateTimeVerifierTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for VerifyVolatileData
        ///</summary>
        [TestMethod()]
        public void VerifyFailedVolatileDataTest()
        {       
            MockRepository mocks = new MockRepository();

            IConfigurationValueProvider configProvider = 
                mocks.StrictMock<IConfigurationValueProvider>();

            Expect.Call(configProvider["timeWindowSeconds"]).Return("200");

            mocks.ReplayAll();

            TokenDateTimeVerifier target = new TokenDateTimeVerifier(configProvider);

            string volatileData = 
                DateTime.UtcNow.AddSeconds(-300).ToString(CultureInfo.InvariantCulture);

            TokenVerificationResult actual = target.VerifyVolatileData(volatileData);

            mocks.VerifyAll();

            Assert.IsTrue(BinaryOperatorUtility.CheckIfContains(VerificationResultType.Failure, actual.ResultType)); 
        }
        public void VerifySuccessfulVolatileDataTest()
        {
            MockRepository mocks = new MockRepository();

            IConfigurationValueProvider configProvider =
                mocks.StrictMock<IConfigurationValueProvider>();

            Expect.Call(configProvider["timeWindowSeconds"]).Return("300");

            mocks.ReplayAll();

            TokenDateTimeVerifier target = new TokenDateTimeVerifier(configProvider);

            string volatileData =
                DateTime.UtcNow.AddSeconds(-200).ToString(CultureInfo.InvariantCulture);

            TokenVerificationResult actual = target.VerifyVolatileData(volatileData);

            mocks.VerifyAll();

            Assert.IsTrue(BinaryOperatorUtility.CheckIfContains(VerificationResultType.Success,
                actual.ResultType));
        }


        /// <summary>
        ///A test for TokenDateTimeVerifier Constructor
        ///</summary>
        [TestMethod()]
        public void TokenDateTimeVerifierConstructorTest()
        {
            IConfigurationValueProvider configProvider = null;
            TokenDateTimeVerifier target = new TokenDateTimeVerifier(configProvider);
        }
    }
}
