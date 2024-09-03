namespace Games;

public class MooGame : GuessingGame, IGuessingGame
{
    public override string GetName()
    {
        return "Moo";
    }

    public override string GetRules()
    {
        return "Rules for Moo:\nTo win you need to guess the right combination of 4 unique numbers (0-9). After each guess you get a hint.\n" +
            "For every right number on the right spot you get a B and for every right number on the wrong spot you get a C.\n";
    }

    public override void MakeGoal()
    {
        Random randomGenerator = new Random();
        string goal = "";
        int goalLength = 4;

        for (int i = 0; i < goalLength; i++)
        {
            int randomNumber = randomGenerator.Next(10);
            string randomNumberAsString = randomNumber.ToString();

            while (goal.Contains(randomNumberAsString))
            {
                randomNumber = randomGenerator.Next(10);
                randomNumberAsString = randomNumber.ToString();
            }

            goal = goal + randomNumberAsString;
        }

        Goal = goal;
    }

    public override string GetHint()
    {
        (int bulls, int cows) = CompareGuessWithGoal();

        return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
    }
}