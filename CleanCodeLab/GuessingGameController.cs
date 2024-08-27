using GamesUI;

/* 
  TODOS:
  - Bryt ut till mindre metoder som bara gör en sak
  - Vill fixa till ShowTopList så den ser finare ut (Extra om det finns tid)
 */

namespace Games;

class GuessingGameController
{
    IUI ui;
    IGuessingGame game;
    int numberOfGuesses = 0;

    public GuessingGameController(IUI ui, IGuessingGame game)
    {
        this.ui = ui;
        this.game = game;
    }

    public void RunGame()
    {
        bool playerWantsToQuit = false;

        string userName = GetUserName();
        ITopListHandler topListHandler = new TopListHandlerForTxtFiles(game.TopListFileName);

        do
        {
            StartNewRound();

            // Comment out or remove next line to play real games!
            ui.Write("For practice, number is: " + game.Goal + "\n");

            LoopUntilCorrectGuess();
            topListHandler.MakeTopList(userName, numberOfGuesses);
            ShowTopList(topListHandler.GetTopList()); // Är detta fult gjort?
            ui.Write(GetCorrectGuessMessage() + "\nContinue?");
            playerWantsToQuit = CheckIfPlayerWantsToQuit();        
        }
        while (playerWantsToQuit == false);
    }

    void StartNewRound()
    {
        ui.Clear();
        ui.Write(game.GetRules());
        game.MakeGoal();
        numberOfGuesses = 0;
        ui.Write("New game:\n");       
    }

    string GetUserName()
    {
        ui.Write("Enter your user name:\n");
        return ui.Read().Trim();

        // Vill vi att det finns något som stoppar användaren från att ha ett tomt namn?
    }

    void GetGuess()
    {
        while (game.Guess == "")
        {
            game.HandleGuess(ui.Read().Trim());
            if (game.Guess == "")
            {
                ui.Write(game.GetNotProperGuessMessage());
            }
        }
    }

    void LoopUntilCorrectGuess()
    {
        while (game.Guess != game.Goal)
        {
            game.Guess = "";
            GetGuess();
            numberOfGuesses++;
            ui.Write(game.Guess + "\n");
            ui.Write(game.GetHint() + "\n");
        }
    }

    void ShowTopList(List<Player> topList)
    {
        ui.Write("Player   games average");
        foreach (Player player in topList)
        {
            ui.Write(string.Format("{0,-9}{1,5:D}{2,9:F2}", player.Name, player.NumberOfGames, player.Average()));
        }
    }

    string GetCorrectGuessMessage()
    {
        if (numberOfGuesses == 1)
        {
            return "\nAmazing, you got it right on your first try!";
        }
        else
        {
            return "\nCorrect, it took " + numberOfGuesses + " guesses";
        }
    }

    bool CheckIfPlayerWantsToQuit()
    {
        string answer = ui.Read().ToLower();

        if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
