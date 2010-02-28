using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBehave.Spec.MSTest;

namespace swe101_GreedScorer
{
    /// <summary>
    /// Summary description for GreedScorerTests
    /// </summary>
    [TestClass]
    public class GreedScorerShould
    {
        public void GreedScorerTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        public void Should_Return_100_When_For_A_Single_1()
        {
            GreedScorer gs = new GreedScorer();
            gs.CalculateScore(1).ShouldEqual(100);
        }
    }
}

public class GreedScorer
{
    public GreedScorer()
    {
    }

    public int CalculateScore(params int[] Rolls)
    {
        return 100;
    }
}