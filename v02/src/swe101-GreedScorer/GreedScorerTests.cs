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
        GreedScorer gs = new GreedScorer();

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
        public void Should_Return_100_When_Roll_Is_A_Single_1()
        {
            gs.CalculateScore(1).ShouldEqual(100);
        }

        [TestMethod]
        public void Should_Return_200_When_Roll_Is_Two_1s()
        {
            gs.CalculateScore(1, 1).ShouldEqual(200);
        }

        [TestMethod]
        public void Should_Return_1000_When_Roll_Is_Three_1s()
        {
            gs.CalculateScore(1, 1, 1).ShouldEqual(1000);
            gs.CalculateScore(1, 1, 1, 1, 1, 1).ShouldEqual(2000);
        }

        [TestMethod]
        public void Should_Return_1100_When_Roll_Is_Four_1s()
        {
            gs.CalculateScore(1, 1, 1, 1).ShouldEqual(1100);
        }

        [TestMethod]
        public void Should_Return_1200_When_Roll_Is_Five_1s()
        {
            gs.CalculateScore(1, 1, 1, 1, 1).ShouldEqual(1200);
        }

        [TestMethod]
        public void Should_Return_50_When_Roll_Is_One_5()
        {
            gs.CalculateScore(5).ShouldEqual(50);
        }

        [TestMethod]
        public void Should_Return_100_When_Roll_Is_Two_5s()
        {
            gs.CalculateScore(5, 5).ShouldEqual(100);
        }

        [TestMethod]
        public void Should_Return_500_When_Roll_Is_Three_5s()
        {
            gs.CalculateScore(5, 5, 5).ShouldEqual(500);
            gs.CalculateScore(5, 5, 5, 5, 5, 5).ShouldEqual(1000);
        }

        [TestMethod]
        public void Should_Return_550_When_Roll_Is_Four_5s()
        {
            gs.CalculateScore(5, 5, 5, 5).ShouldEqual(550);
        }

        [TestMethod]
        public void Should_Return_600_When_Roll_Is_Five_5s()
        {
            gs.CalculateScore(5, 5, 5, 5, 5).ShouldEqual(600);
        }

        [TestMethod]
        public void Should_Return_0_When_Roll_Is_Any_Single_Number_Other_Than_1_Or_5()
        {
            gs.CalculateScore(2).ShouldEqual(0);
            gs.CalculateScore(3).ShouldEqual(0);
            gs.CalculateScore(4).ShouldEqual(0);
            gs.CalculateScore(6).ShouldEqual(0);
        }

        [TestMethod]
        public void Should_Return_0_When_Roll_Is_Any_Double_Number_Other_Than_1_Or_5()
        {
            gs.CalculateScore(2, 2).ShouldEqual(0);
            gs.CalculateScore(3, 3).ShouldEqual(0);
            gs.CalculateScore(4, 4).ShouldEqual(0);
            gs.CalculateScore(6, 6).ShouldEqual(0);
        }

        [TestMethod]
        public void Should_Return_200_When_Roll_Is_Three_2s()
        {
            gs.CalculateScore(2, 2, 2).ShouldEqual(200);
            gs.CalculateScore(2, 2, 2, 2, 2, 2).ShouldEqual(400);
        }

        [TestMethod]
        public void Should_Return_300_When_Roll_Is_Three_3s()
        {
            gs.CalculateScore(3, 3, 3).ShouldEqual(300);
            gs.CalculateScore(3, 3, 3, 3, 3, 3).ShouldEqual(600);
        }

        [TestMethod]
        public void Should_Return_400_When_Roll_Is_Three_4s()
        {
            gs.CalculateScore(4, 4, 4).ShouldEqual(400);
            gs.CalculateScore(4, 4, 4, 4, 4, 4).ShouldEqual(800);
        }

        [TestMethod]
        public void Should_Return_600_When_Roll_Is_Three_6s()
        {
            gs.CalculateScore(6, 6, 6).ShouldEqual(600);
            gs.CalculateScore(6, 6, 6, 6, 6, 6).ShouldEqual(1200);
        }

        [TestMethod]
        public void Should_Return_Appropriate_Result_For_Acceptance_Tests()
        {
            gs.CalculateScore(1, 1, 1, 5, 1).ShouldEqual(1150);
            gs.CalculateScore(2, 3, 4, 6, 2).ShouldEqual(0);
            gs.CalculateScore(3, 4, 5, 3, 3).ShouldEqual(350);
            gs.CalculateScore(1, 5, 1, 2, 4).ShouldEqual(250);
        }
    }
}

public class GreedScorer
{
    public int CalculateScore(params int[] Rolls)
    {
        int score = 0;
        score += Rolls.CountTriples(1) * 1000;
        score += Rolls.CountSingles(1) * 100;
        score += Rolls.CountTriples(2) * 200;
        score += Rolls.CountTriples(3) * 300;
        score += Rolls.CountTriples(4) * 400;
        score += Rolls.CountTriples(5) * 500;
        score += Rolls.CountSingles(5) * 50;
        score += Rolls.CountTriples(6) * 600;
        return score;
    }
}

public static class GreedScorerExtensions
{
    public static int CountTriples(this int[] Rolls, int NumberToLookFor)
    {
        return Rolls.Where(x => x == NumberToLookFor).Count() / 3;
    }

    public static int CountSingles(this int[] Rolls, int NumberToLookFor)
    {
        return Rolls.Where(x => x == NumberToLookFor).Count() % 3;
    }
}
