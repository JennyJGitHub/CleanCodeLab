using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Tests
{
    [TestClass()]
    public class MooGameTests
    {
        MooGame mooGame;

        [TestInitialize]
        public void InitializeMooGameTests()
        {
            mooGame = new MooGame();
        }

        [TestMethod()]
        public void GetRulesTest() // Är den nödvändig?
        {
            Assert.AreEqual("Rules for Moo:\nTo win you need to guess the right combination of 4 unique numbers. After each guess you get a hint.\n" +
            "For every right number on the right spot you get a B and for every right number on the wrong spot you get a C.\n", mooGame.GetRules());
        }

        [TestMethod()]
        public void MakeGoalTest() // Är den ok? Får jag ha flera asserts?
        {
            Assert.AreEqual("", mooGame.Goal);
            mooGame.MakeGoal();
            Assert.AreEqual(4, mooGame.Goal.Length);
        }

        [TestMethod()]
        public void HandleGuessRightInputTest()
        {
            string guess = "1234";
            mooGame.HandleGuess(guess);
            Assert.AreEqual(guess, mooGame.Guess);
        }

        [TestMethod()]
        [DataRow("123a")]
        [DataRow("12345")]
        [DataRow("1")]
        [DataRow("Shall not pass")]
        [DataRow("    ")]
        [DataRow("1.23")]
        public void HandleGuessWrongInputTest(string input) // Borde detta vara flera tester? Är det svårt att förstå?
        {
            mooGame.HandleGuess(input);
            Assert.AreEqual("", mooGame.Guess);
        }

        [TestMethod()]
        public void GetNotProperGuessMessageTest() // Är den nödvändig?
        {
            Assert.AreEqual("\nYour guess needs to be 4 numbers, please try again.\n", mooGame.GetNotProperGuessMessage());
        }

        [TestMethod()]
        public void GetHintTest() // Borde detta vara flera tester? Är det svårt att förstå?
        {
            mooGame.Goal = "1234";

            mooGame.Guess = "1234";
            Assert.AreEqual("BBBB,", mooGame.GetHint());

            mooGame.Guess = "1235";
            Assert.AreEqual("BBB,", mooGame.GetHint());

            mooGame.Guess = "1245";
            Assert.AreEqual("BB,C", mooGame.GetHint());

            mooGame.Guess = "4321";
            Assert.AreEqual(",CCCC", mooGame.GetHint());

            mooGame.Guess = "9876";
            Assert.AreEqual(",", mooGame.GetHint());
        }
    }
}

namespace CleanCodeLabTests // Varför skapades detta?
{
    internal class MooGameTests
    {
    }
}
