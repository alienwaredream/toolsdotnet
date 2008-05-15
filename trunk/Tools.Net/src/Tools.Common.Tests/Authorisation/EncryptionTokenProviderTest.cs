using Tools.Common.Authorisation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Tools.Common.Cryptography;
using Tools.Common.Config;
using Rhino.Mocks;
using Spring.Core.IO;
namespace Tools.Common.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for EncryptionTokenProviderTest and is intended
    ///to contain all EncryptionTokenProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EncryptionTokenProviderTest
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
        ///A test for IssueToken
        ///</summary>
        [TestMethod()]
        public void IssueTokenTest()
        {
            MockRepository mocks = new MockRepository();

            ITokenVolatileDataProvider volatileDataProvider = mocks.CreateMock<ITokenVolatileDataProvider>();
            Expect.Call(volatileDataProvider.GetVolatileData()).Return("testVolatileData");

            mocks.ReplayAll();

            EncryptionTokenProvider target = new EncryptionTokenProvider(
                new StringCryptoTransformer(
                    new NameValueSectionConfigurationProvider(ConfigSectionsResource.DefaultStringEncryptorSectionName)),
                    new NameValueSectionConfigurationProvider(ConfigSectionsResource.DefaultEncryptionTokenProviderSectionName),
                    volatileDataProvider);

            string tokenSource = "testSource";
            string expected = "n0EZVRe/bDyS3UGa0Y//VO7L0RiFrs+e8er2ilpHypC9r1+Yn2YLfjChc+CG16aV8kA6TJcs2CVnZfXps+pfNgu1woKqrP6WjV+GnnWiv1GyEeMMQBhLqlK+SeiqZuY2";
            string actual;
            actual = target.IssueToken(tokenSource);

            Trace.WriteLine("Token:" + actual);

            mocks.VerifyAll();

            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for EncryptionTokenProvider Constructor
        ///</summary>
        [TestMethod()]
        public void EncryptionTokenProviderConstructorTest()
        {
            EncryptionTokenProvider target = new EncryptionTokenProvider(
                new StringCryptoTransformer(
                    new NameValueSectionConfigurationProvider(ConfigSectionsResource.DefaultStringEncryptorSectionName)),
                    new NameValueSectionConfigurationProvider(ConfigSectionsResource.DefaultEncryptionTokenProviderSectionName));

        }
    }
}
