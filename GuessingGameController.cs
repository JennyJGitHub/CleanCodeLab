using GamesUI;
using System.Diagnostics;
using System.Xml.Linq;

/* 
  TODOS: 
  - GÖR TESTER!!!!!!!!!!
  - Bryt ut till mindre metoder som bara gör en sak
  - Gör en klass som heter MooGame som har logiken för det spelet
  - Försök förstå Equals och GetHashCode metoderna
  - Vill fixa till ShowTopList så den ser finare ut
  - Se om du kan göra en interface som heter IGame för de 2 olika spelen som ska göras (VG)
  - Vill ändra namet på filen result.txt till resultMooGame så att det finns en annan result för det andra spelet (VG)
  - Var ska menyn vara? I Program, GameController eller i en ny som heter GameMenu? (VG)
 */

namespace Games;

class GuessingGameController
{
    IUI ui;
    IGuessingGame game;

    public GuessingGameController(IUI ui, IGuessingGame game)
    {
        this.ui = ui;
        this.game = game;
    }

    public void Run()
    {
        string answer; // ändra till bool playerWantsToQuit = false;
        game.UserName = GetUserName();


        do
        {
            ui.Clear();
            ui.Write(game.GetRules());
            game.MakeGoal();
            game.NumberOfGuesses = 0;
            ui.Write("New game:\n");

            //comment out or remove next line to play real games!
            ui.Write("For practice, number is: " + game.Goal + "\n");

            LoopUntilCorrectGuess();
            game.MakeTopList();
            ShowTopList(game.GetTopList()); // Är detta fult?
            ui.Write("\nCorrect, it took " + game.NumberOfGuesses + " guesses\nContinue?"); // Vill lägga till "Y/N?" för det är mer tydligt för användaren vad hen ska skriva
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
            game.NumberOfGuesses++;
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

    /* Som jag kan leka med tills jag tycker det ser snyggt ut
    
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
