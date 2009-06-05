using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tools.Coordination.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ModWalkerTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestMethod]
        public void TestModWalker()
        {
            const int walkerIndex = 6;
            var testArray = new[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
            var outArray = new int[13];

            WalkTheArray(walkerIndex, testArray, outArray);
            WalkTheArray(0, testArray, outArray);
            WalkTheArray(12, testArray, outArray);

            WalkTheArray(11, testArray, outArray);
            WalkTheArray(13, testArray, outArray);
        }

        private static void WalkTheArray(int walkerIndex, int[] testArray, int[] outArray)
        {
            Console.WriteLine("walker=" + walkerIndex);

            for (int i = 0; i < testArray.Length; i++)
            {
                int j;
                if (i < testArray.Length - walkerIndex)
                    j = walkerIndex + i;
                else
                    j = i - (testArray.Length - walkerIndex);

                outArray[j] = testArray[j];

                Console.WriteLine("element {0} is {1}", j, testArray[j]);
            }
        }
    }
}