using Tools.Common.Authorisation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.Common.Cryptography;
using Tools.Common.Config;
namespace Tools.Common.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for TokenCryptoTransformerTest and is intended
    ///to contain all TokenCryptoTransformerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StringCryptoTransformerTest
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
        ///A test for Encrypt
        ///</summary>
        [TestMethod()]
        public void EncryptTest()
        {
            StringCryptoTransformer target = new StringCryptoTransformer(
                    new NameValueSectionConfigurationProvider(
                        ConfigSectionsResource.DefaultStringEncryptorSectionName));

            string plainText = "Test";
            string expected = "EJyBNXvBlbjViO1mOEEIfA==";
            string actual;
            actual = target.Encrypt(plainText);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for Decrypt
        ///</summary>
        [TestMethod()]
        public void DecryptTest()
        {
            StringCryptoTransformer target = new StringCryptoTransformer(
                    new NameValueSectionConfigurationProvider(
                        ConfigSectionsResource.DefaultStringEncryptorSectionName));
            string cipherText = "EJyBNXvBlbjViO1mOEEIfA==";
            string expected = "Test"; 
            string actual;
            actual = target.Decrypt(cipherText);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TokenCryptoTransformer Constructor
        ///</summary>
        [TestMethod()]
        public void TokenCryptoTransformerConstructorTest()
        {
            StringCryptoTransformer target = new StringCryptoTransformer(
                    new NameValueSectionConfigurationProvider(
                        ConfigSectionsResource.DefaultStringEncryptorSectionName));
        }
    }
}
