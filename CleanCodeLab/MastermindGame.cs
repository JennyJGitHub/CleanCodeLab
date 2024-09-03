namespace Games;

public class MastermindGame : GuessingGame, IGuessingGame
{
    public override string GetName()
    {
        return "Mastermind";
    }

    public override string GetRules()
    {
        return "Rules for Mastermind:\nTo win you need to guess the right combination of 4 numbers (0 - 5). After each guess you get a hint.\n" +
            "For every right number on the right spot you get a B and for every right number on the wrong spot you get a W.\n";
    }

    public override void MakeGoal()
    {
        Random randomGenerator = new Random();
        string goal = "";
        int goalLength = 4;

        for (int i = 0; i < goalLength; i++)
        {
            int randomNumber = randomGenerator.Next(6);
            string randomNumberAsString = randomNumber.ToString();
            goal = goal + randomNumberAsString;
        }

        Goal = goal;
    }

    public override string GetHint()
    {
        (int black, int white) = CompareGuessWithGoal();

        return "BBBB".Substring(0, black) + "," + "WWWW".Substring(0, white);
    }
}
