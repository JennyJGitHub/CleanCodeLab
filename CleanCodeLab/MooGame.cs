namespace Games;

// Todo:
// Vill ändra result.txt till resultMooGame så att det finns en annan result för det andra spelet (VG)


public class MooGame : IGuessingGame
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

    public void HandleGuess(string guess)
    {
        bool guessIsOnlyNumbers = CheckIfGuessOnlyContainsNumbers(guess);

        if (guess.Length != 4 || guessIsOnlyNumbers == false)
        {
            Guess = "";
        }
        else
        {
            Guess = guess;
        }

    }

    public string GetInvalidGuessMessage()
    {
        return "\nYour guess needs to be 4 numbers, please try again.\n";
    }

    public string GetHint()
    {
        (int bulls, int cows) = GetBullsAndCows();
        return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
    }


    (int, int) GetBullsAndCows()
    {
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

    bool CheckIfGuessOnlyContainsNumbers(string guess)
    {
        foreach (char character in guess)
        {
            if (char.IsDigit(character) == false)
                return false;
        }

        return true;
    }
}