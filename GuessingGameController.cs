using GamesUI;
using System.Diagnostics;
using System.Xml.Linq;

/* 
  TODOS: 
  - GÖR TESTER!!!!!!!!!!
  - Bryt ut till mindre metoder som bara gör en sak
  - Gör en klass som heter MooGame som har logiken för det spelet
  - Försök förstå Equals och GetHashCode metoderna
  - Mastermind som andra spel? Hur ska det gå till? (VG)
  - Vill ändra namet på filen result.txt till resultMooGame så att det finns en annan result för det andra spelet (VG)
  - Var ska menyn vara? I Program eller i en ny som heter GameMenu? (VG)
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

    public void Run()
    {
        string answer; // kanske ändra till bool playerWantsToQuit = false;

        string userName = GetUserName();
        TopList topList = new(game.TopListFileName);

        do
        {
            ui.Clear();
            ui.Write(game.GetRules());
            game.MakeGoal();
            numberOfGuesses = 0;

            ui.Write("New game:\n");

            //comment out or remove next line to play real games!
            ui.Write("For practice, number is: " + game.Goal + "\n");

            LoopUntilCorrectGuess();
            topList.MakeTopList(userName, numberOfGuesses);
            ShowTopList(topList.GetTopList()); // Är detta fult gjort?
            ui.Write("\nCorrect, it took " + numberOfGuesses + " guesses\nContinue?"); // Vill lägga till "Y/N?" för det är mer tydligt för användaren vad hen ska skriva
            answer = ui.Read().ToLower(); // Lade till ToLower så att man användaren blir förstådd om hen använder stora bokstäver            
        }
        while (answer == null || answer == "" || answer.Substring(0, 1) != "n"); // Kommer answer någonsin vara null?
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
                ui.Write(game.GetNotProperGuessMessage());
        }
    }

    void LoopUntilCorrectGuess() // Är namnet och innehållet OK?
    {
        while (game.Guess != game.Goal)
        {
            game.Guess = "";
            GetGuess();
            numberOfGuesses++;
            ui.Write(game.Guess + "\n"); // Vill man se denna? Gissningen syns ju ändå
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

    /* Som jag kan leka med tills jag tycker det ser snyggt ut (om det finns tid)
    
    void ShowTopList(List<PlayerData> topList)
    {
        ui.Write(string.Format("{0,-9}{1,5:D}{2,9:F2}", "Player", "games", "average"));
        foreach (PlayerData player in topList)
        {
            ui.Write(string.Format("{0,-9}{1,5:D}{2,9:F2}", player.Name, player.numberOfGames, player.Average()));
        }
    }

    */
}
