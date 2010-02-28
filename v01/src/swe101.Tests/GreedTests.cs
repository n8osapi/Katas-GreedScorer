using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBehave.Spec.MSTest;

namespace se101.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class GreedTests
    {
        private GreedScorer _scorer = new GreedScorer();

        public GreedTests()
        {
            _scorer = new GreedScorer();
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
        public void When_Roll_Is_1_1_1_0_0_Then_Score_Should_Be_1000()
        {
            var Rolls = BuildRolls(1, 1, 1, 0, 0);
            var Result = _scorer.CalculateScore(Rolls);
            Result.ShouldEqual(1000);
        }

        [TestMethod]
        public void When_Roll_Is_1_1_1_1_1_Then_Score_Should_Be_1200()
        {
            var Rolls = BuildRolls(1, 1, 1, 1, 1);
            var Result = _scorer.CalculateScore(Rolls);
            Result.ShouldEqual(1200);
        }

        [TestMethod]
        public void When_Roll_Is_1_1_1_1_1_1_Then_Score_Should_Be_2000()
        {
            var Rolls = BuildRolls(1, 1, 1, 1, 1, 1);
            var Result = _scorer.CalculateScore(Rolls);
            Result.ShouldEqual(2000);
        }

        [TestMethod]
        public void When_Roll_Is_1_1_0_0_0_Then_Score_Should_Be_200()
        {
            var Rolls = BuildRolls(1, 1, 0, 0, 0);
            var Result = _scorer.CalculateScore(Rolls);
            Result.ShouldEqual(200);
        }

        [TestMethod]
        public void When_Roll_Is_1_0_0_0_0_Then_Score_Should_Be_100()
        {
            var Rolls = BuildRolls(1, 0, 0, 0, 0);
            var Result = _scorer.CalculateScore(Rolls);
            Result.ShouldEqual(100);
        }

        [TestMethod]
        public void Acceptance_Test_When_Roll_Is_1_1_1_5_1_Then_Should_Score_Be_1150()
        {
            var Rolls = BuildRolls(1, 1, 1, 5, 1);
            var Result = _scorer.CalculateScore(Rolls);
            Result.ShouldEqual(1150);
        }

        [TestMethod]
        public void Acceptance_Test_When_Roll_Is_2_3_4_6_2_Then_Should_Score_Be_0()
        {
            var Rolls = BuildRolls(2, 3, 4, 6, 2);
            var Result = _scorer.CalculateScore(Rolls);
            Result.ShouldEqual(0);
        }

        [TestMethod]
        public void Acceptance_Test_When_Roll_Is_3_4_5_3_3_Then_Score_Should_Be_350()
        {
            var Rolls = BuildRolls(3, 4, 5, 3, 3);
            var Result = _scorer.CalculateScore(Rolls);
            Result.ShouldEqual(350);
        }

        [TestMethod]
        public void Acceptance_Test_When_Roll_Is_1_5_1_2_4_Then_Score_Should_Be_250()
        {
            var Rolls = BuildRolls(1, 5, 1, 2, 4);
            var Result = _scorer.CalculateScore(Rolls);
            Result.ShouldEqual(250);
        }

        private List<Die> BuildRolls(params int[] Rolls)
        {
            return Rolls.Select(x => new Die(x)).ToList();
        }
    }
}

public class GreedScorer
{
    List<ScoreRule> Rules = new List<ScoreRule>();

    public GreedScorer()
    {
        // Order Matters... Largest rules are checked first
        Rules.Add(new ScoreRule() { Number = 1, Score = 1000, SequenceCount=3 });
        Rules.Add(new ScoreRule() { Number = 2, Score = 200 , SequenceCount=3 });
        Rules.Add(new ScoreRule() { Number = 3, Score = 300 , SequenceCount=3 });
        Rules.Add(new ScoreRule() { Number = 4, Score = 400 , SequenceCount=3 });
        Rules.Add(new ScoreRule() { Number = 5, Score = 500 , SequenceCount=3 });
        Rules.Add(new ScoreRule() { Number = 6, Score = 600 , SequenceCount=3 });
        Rules.Add(new ScoreRule() { Number = 1, Score = 100 , SequenceCount=1 });
        Rules.Add(new ScoreRule() { Number = 2, Score = 0   , SequenceCount=1 });
        Rules.Add(new ScoreRule() { Number = 3, Score = 0   , SequenceCount=1 });
        Rules.Add(new ScoreRule() { Number = 4, Score = 0   , SequenceCount=1 });
        Rules.Add(new ScoreRule() { Number = 5, Score = 50  , SequenceCount=1 });
        Rules.Add(new ScoreRule() { Number = 6, Score = 0   , SequenceCount=1 });
    }

    public int CalculateScore(List<Die> Rolls)
    {
        var score = 0;
        Rules.ForEach(rule =>
            {
                int result = -1;

                //Loop until this particular rule does not find any more results
                while (result != 0)
                {
                    /* ProcessRolls modifies Rolls and marks whatever die 
                     * are used to calculate the score as Counted=True */
                    result = rule.ProcessRolls(Rolls);
                    score += result;
                }
            });
        return score;
    }
}

public class ScoreRule
{
    public int Number { get; set; }
    public int Score { get; set; }
    public int SequenceCount { get; set; }

    public ScoreRule()
    {
        this.Number = 0;
        this.Score = 0;
        this.SequenceCount = 0;
    }

    public int ProcessRolls(List<Die> Rolls)
    {
        var Remaining = Rolls.Where(x => (x.Value == Number) && (!x.Counted));
        if (Remaining.Count() < this.SequenceCount) return 0;
        Remaining.Take(this.SequenceCount).ToList().ForEach(x => x.Counted = true);
        return this.Score;
    }
}

public class Die
{
    public int Value { get; set; }
    public bool Counted { get; set; }

    public Die(int Value)
    {
        this.Value = Value;
        this.Counted = false;
    }

    public Die()
    {
        this.Value = 0;
        this.Counted = false;
    }
}