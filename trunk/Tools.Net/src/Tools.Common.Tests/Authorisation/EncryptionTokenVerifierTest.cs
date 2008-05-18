using Tools.Common.Authorisation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.Common.Cryptography;
using Tools.Common.Config;
using Rhino.Mocks;
using System.Diagnostics;
using Tools.Common.Utils;

namespace Tools.Common.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for EncryptionTokenVerifierTest and is intended
    ///to contain all EncryptionTokenVerifierTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EncryptionTokenVerifierTest
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
        ///A test for VerifyToken
        ///</summary>
        [TestMethod()]
        public void VerifyTokenPositiveTest()
        {
            IStringCryptoTransformer crypto = new StringCryptoTransformer(
                    new NameValueSectionConfigurationProvider(ConfigSectionsResource.DefaultStringEncryptorSectionName));

            MockRepository mocks = new MockRepository();

            ITokenVolatileDataVerifier volatileDataVerifier = 
                mocks.StrictMock<ITokenVolatileDataVerifier>();
            Expect.Call(volatileDataVerifier.VerifyVolatileData("testVolatileData")).Return(
                new TokenVerificationResult { 
                    ResultType = VerificationResultType.Success, Message = "Success" });

            mocks.ReplayAll();

            //ITokenVolatileDataVerifier volatileDataVerifier = new TokenDateTimeVerifier(
            //    new NameValueSectionConfigurationProvider("testTokenDateTimeVerifierSection"));

            EncryptionTokenVerifier target = new EncryptionTokenVerifier(crypto, volatileDataVerifier); // TODO: Initialize to an appropriate value
            string tokenTarget = "testSource";
            string token = @"n0EZVRe/bDyS3UGa0Y//VO7L0RiFrs+e8er2ilpHypC9r1+Yn2YLfjChc+CG16aV8kA6TJcs2CVnZfXps+pfNgu1woKqrP6WjV+GnnWiv1GyEeMMQBhLqlK+SeiqZuY2";

            TokenVerificationResult actual = target.VerifyToken(tokenTarget, token);

            mocks.VerifyAll();

            Assert.IsTrue(actual.ResultType == VerificationResultType.Success);

        }

        [TestMethod()]
        public void VerifyTokenForTemperedDataTest()
        {
            IStringCryptoTransformer crypto = new StringCryptoTransformer(
                    new NameValueSectionConfigurationProvider(ConfigSectionsResource.DefaultStringEncryptorSectionName));

            MockRepository mocks = new MockRepository();

            ITokenVolatileDataVerifier volatileDataVerifier =
                mocks.StrictMock<ITokenVolatileDataVerifier>();
            Expect.Call(volatileDataVerifier.VerifyVolatileData("testVolatileData")).Return(
                new TokenVerificationResult
                {
                    ResultType = VerificationResultType.Success,
                    Message = "Success"
                });

            mocks.ReplayAll();

            //ITokenVolatileDataVerifier volatileDataVerifier = new TokenDateTimeVerifier(
            //    new NameValueSectionConfigurationProvider("testTokenDateTimeVerifierSection"));

            EncryptionTokenVerifier target = new EncryptionTokenVerifier(crypto, volatileDataVerifier); // TODO: Initialize to an appropriate value
            string tokenTarget = "testSource";
            string token = @"h0EZVRe/bDyS3UGa0Y//VO7L0RiFrs+e8er2ilpHypC9r1+Yn2YLfjChc+CG16aV8kA6TJcs2CVnZfXps+pfNgu1woKqrP6WjV+GnnWiv1GyEeMMQBhLqlK+SeiqZuY2";

            TokenVerificationResult actual = target.VerifyToken(tokenTarget, token);

            //mocks.VerifyAll();

            Trace.WriteLine("VerificationResultType: " + actual.ResultType);

            Assert.IsTrue(BinaryOperatorUtility.CheckIfContains(VerificationResultType.Failure, actual.ResultType));

        }

        [TestMethod()]
        public void VerifyTokenForSourceAndTargetMismatchTest()
        {
            IStringCryptoTransformer crypto = new StringCryptoTransformer(
                    new NameValueSectionConfigurationProvider(ConfigSectionsResource.DefaultStringEncryptorSectionName));

            MockRepository mocks = new MockRepository();

            ITokenVolatileDataVerifier volatileDataVerifier =
                mocks.StrictMock<ITokenVolatileDataVerifier>();
            Expect.Call(volatileDataVerifier.VerifyVolatileData("testVolatileData")).Return(
                new TokenVerificationResult
                {
                    ResultType = VerificationResultType.Success,
                    Message = "Success"
                });

            mocks.ReplayAll();

            //ITokenVolatileDataVerifier volatileDataVerifier = new TokenDateTimeVerifier(
            //    new NameValueSectionConfigurationProvider("testTokenDateTimeVerifierSection"));

            EncryptionTokenVerifier target = new EncryptionTokenVerifier(crypto, volatileDataVerifier); // TODO: Initialize to an appropriate value
            string tokenTarget = "testSource1";
            string token = @"n0EZVRe/bDyS3UGa0Y//VO7L0RiFrs+e8er2ilpHypC9r1+Yn2YLfjChc+CG16aV8kA6TJcs2CVnZfXps+pfNgu1woKqrP6WjV+GnnWiv1GyEeMMQBhLqlK+SeiqZuY2";

            TokenVerificationResult actual = target.VerifyToken(tokenTarget, token);

            //mocks.VerifyAll();

            Trace.WriteLine("VerificationResultType: " + actual.ResultType);

            Assert.IsTrue(BinaryOperatorUtility.CheckIfContains(
                VerificationResultType.FailureForSourceAndTargetMismatch, actual.ResultType));

        }
        /// <summary>
        ///A test for EncryptionTokenVerifier Constructor
        ///</summary>
        [TestMethod()]
        public void EncryptionTokenVerifierConstructorTest()
        {
            IStringCryptoTransformer crypto = null; // TODO: Initialize to an appropriate value
            ITokenVolatileDataVerifier volatileDataVerifier = null; // TODO: Initialize to an appropriate value
            
            EncryptionTokenVerifier target = new EncryptionTokenVerifier(crypto, volatileDataVerifier);
        }
    }
}
