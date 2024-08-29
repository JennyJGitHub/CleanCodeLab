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
        public void GetRules_GettingRules_ReturnRules()
        {
            string expected = "Rules for Moo:\nTo win you need to guess the right combination of 4 unique numbers. After each guess you get a hint.\n" +
            "For every right number on the right spot you get a B and for every right number on the wrong spot you get a C.\n";
            string actual = mooGame.GetRules();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MakeGoal_MakingNewGoal_NewGoalMade() // Finns det något annat jag kan kolla för att se att metoden fungerar som den ska? Vet inte vad den ska heta.
        {
            int expectedLength = 4;
            mooGame.MakeGoal();

            Assert.AreEqual(expectedLength, mooGame.Goal.Length);
        }

        [TestMethod()]
        public void HandleGuess_CorrectInput_GuessIsInput()
        {
            string input = "1234";
            mooGame.HandleGuess(input);

            Assert.AreEqual(input, mooGame.Guess);
        }

        [TestMethod()]
        [DataRow("123a")]
        [DataRow("12345")]
        [DataRow("1")]
        [DataRow("Shall not pass")]
        [DataRow("    ")]
        [DataRow("1.23")]
        [DataRow("-123")]
        [DataRow("+123")]
        public void HandleGuess_WrongInput_GuessIsEmptyString(string input)
        {
            mooGame.HandleGuess(input);

            Assert.AreEqual("", mooGame.Guess);
        }

        [TestMethod()]
        public void GetInvalidGuessMessage_GettingMessage_ReturnMessage()
        {
            string expected = "\nYour guess needs to be 4 numbers, please try again.\n";
            string actual = mooGame.GetInvalidGuessMessage();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetHint_CorrectGuess_ReturnBBBB()
        {
            mooGame.Goal = "1234";
            mooGame.Guess = "1234";

            string expected = "BBBB,";
            string actual = mooGame.GetHint();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetHint_CorrectNumbersWrongPositionGuess_ReturnCCCC()
        {
            mooGame.Goal = "1234";
            mooGame.Guess = "4321";

            string expected = ",CCCC";
            string actual = mooGame.GetHint();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetHint_HalfCorrectHalfWrongPositionGuess_ReturnBBCC()
        {
            mooGame.Goal = "1234";
            mooGame.Guess = "1324";

            string expected = "BB,CC";
            string actual = mooGame.GetHint();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetHint_WrongGuess_ReturnComma()
        {
            mooGame.Goal = "1234";
            mooGame.Guess = "9876";

            string expected = ",";
            string actual = mooGame.GetHint();

            Assert.AreEqual(expected, actual);
        }
    }
}
