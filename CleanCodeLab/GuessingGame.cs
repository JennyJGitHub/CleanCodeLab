namespace Games;

public abstract class GuessingGame : IGuessingGame
{
    public string Goal { get; set; } = "";
    public string Guess { get; set; } = "";

    public abstract string GetName();

    public abstract string GetRules();

    public abstract void MakeGoal();

    public void HandleGuess(string guess)
    {
        bool guessIsOnlyNumbers = AreOnlyNumbers(guess);
        int validGuessLength = Goal.Length;

        if (guess.Length != Goal.Length || guessIsOnlyNumbers == false)
        {
            Guess = "";
        }
        else
        {
            Guess = guess;
        }
    }

    protected bool AreOnlyNumbers(string guess)
    {
        foreach (char character in guess)
        {
            if (char.IsDigit(character) == false)
            {
                return false;
            }
        }

        return true;
    }

    public string GetInvalidGuessMessage()
    {
        return "\nYour guess needs to be 4 numbers, please try again.\n";
    }

    public abstract string GetHint();


    protected (int, int) CompareGuessWithGoal()
    {
        int correctPosition = 0, wrongPosition = 0;

        for (int i = 0; i < Guess.Length; i++)
        {
            for (int j = 0; j < Guess.Length; j++)
            {
                if (Goal[i] == Guess[j])
                {
                    if (i == j)
                    {
                        correctPosition++;
                    }
                    else
                    {
                        wrongPosition++;
                    }
                }
            }
        }

        return (correctPosition, wrongPosition);
    }
}
