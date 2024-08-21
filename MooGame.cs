namespace Games;

// Todo:
// GÖR TESTER!!!!!!!!!!
// Kolla om FindBullsAndCows kan förbättras
// Kolla om HandleGuess kan förbättras
// Vill ändra result.txt till resultMooGame så att det finns en annan result för det andra spelet (VG)


class MooGame : IGuessingGame
{
    public string Goal { get; set; } = "";
    public string Guess { get; set; } = "";
    public string TopListFileName { get; init; } = "result.txt";

    public string GetRules()
    {
        return "Rules for Moo:\nTo win you need to guess the right combination of 4 unique numbers. After each guess you get a hint.\n" +
            "For every right number on the right spot you get a B and for every right number on the wrong spot you get a C.\n";
    }

    public void MakeGoal()
    {
        Random randomGenerator = new Random();
        string goal = "";
        for (int i = 0; i < 4; i++)
        {
            int random = randomGenerator.Next(10);
            string randomDigit = "" + random;
            while (goal.Contains(randomDigit))
            {
                random = randomGenerator.Next(10);
                randomDigit = "" + random;
            }
            goal = goal + randomDigit;
        }
        Goal = goal;
    }

    public void HandleGuess(string guess)
    {
        // Vill kolla om gissningen bara består av siffror, men vill inte använda guessInt. Finns det ett bättre sätt?
        bool guessIsInt = Int32.TryParse(guess, out int guessInt);
        if (guess.Length != 4 || guessIsInt == false)
        {
            Guess = "";
        }
        else
        {
            Guess = guess;
        }

    }

    public string GetNotProperGuessMessage() // Finns det ett bättre namn?
    {
        return "\nYour guess needs to be 4 numbers, please try again.\n";
    }

    public string GetHint()
    {
        (int bulls, int cows) = FindBullsAndCows();
        return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
    }


    (int, int) FindBullsAndCows()
    {
        // Kan detta göras på ett bättre sätt?
        int bulls = 0, cows = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (Goal[i] == Guess[j])
                {
                    if (i == j)
                    {
                        bulls++;
                    }
                    else
                    {
                        cows++;
                    }
                }
            }
        }
        return (bulls, cows);
    }
}