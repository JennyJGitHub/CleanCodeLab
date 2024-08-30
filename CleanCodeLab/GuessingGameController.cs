using GamesUI;

/* 
  TODOS:
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
        string userName = GetUserName();
        ITopListHandler topListHandler = new TxtFileTopListHandler(game.GetName());

        bool quitting = false;
        while (quitting == false)
        {
            StartNewRound();

            // Comment out or remove next line to play real games!
            ui.Write("For practice, number is: " + game.Goal + "\n");

            LoopUntilCorrectGuess();
            topListHandler.MakeTopList(userName, numberOfGuesses);
            ShowTopList(topListHandler.GetTopList());
            ShowCorrectGuessMessage();
            quitting = IsQuitting();        
        }
        
    }

    string GetUserName()
    {
        string name = "";

        while (name == "")
        {
            ui.Write("Enter your user name:\n");
            name = ui.Read().Trim();
            if (name == "")
            {
                ui.Write("You have to enter a name to continue.\n");
            }
        }

        return name;
    }

    void StartNewRound()
    {
        ui.Clear();
        ui.Write(game.GetRules());
        game.MakeGoal();
        numberOfGuesses = 0;
        ui.Write("New game:\n");       
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

    void GetGuess()
    {
        while (game.Guess == "")
        {
            game.HandleGuess(ui.Read().Trim());

            if (game.Guess == "")
            {
                ui.Write(game.GetInvalidGuessMessage());
            }
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

    void ShowCorrectGuessMessage()
    {
        if (numberOfGuesses == 1)
        {
            ui.Write("\nAmazing, you got it right on your first try!");
        }
        else
        {
            ui.Write("\nCorrect, it took " + numberOfGuesses + " guesses.");
        }
    }

    bool IsQuitting()
    {
        ui.Write("\nContinue?");
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
