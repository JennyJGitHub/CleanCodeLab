using GamesUI;
using System.Diagnostics;
using System.Xml.Linq;

/* 
  TODOS: 
  - Bryt ut till mindre metoder som bara gör en sak
  - Gör en klass som heter MooGame som har logiken för det spelet
  - Försök förstå Equals och GetHashCode metoderna - De används när top listan ska visas
  - Se om du kan göra en interface som heter IGame för de 2 olika spelen som ska göras (VG)
  - Vill ändra namet på filen result.txt till resultMooGame så att det finns en annan result för det andra spelet (VG)
  - Var ska menyn vara? I Program, GameController eller i en ny som heter GameMenu? (VG)
 */

namespace Games;

class GameController
{
    IUI ui;
    IGame game;

    public GameController(IUI ui, IGame game)
    {
        this.ui = ui;
        this.game = game;
    }

    public void Run()
    {
        string answer;
        game.UserName = GetUserName();


        do
        {
            string goal = game.MakeGoal();
            ui.Clear();
            ui.Write(game.GetRules());

            ui.Write("New game:\n");
            //comment out or remove next line to play real games!
            ui.Write("For practice, number is: " + goal + "\n");
            string guess = ui.Read();

            game.NumberOfGuesses = 1;
            string hint = game.CreateHint(goal, guess);
            ui.Write(hint + "\n");
            while (hint != "BBBB,")
            {
                game.NumberOfGuesses++;
                guess = ui.Read();
                ui.Write(guess + "\n"); // Varför ska denna skrivas ut igen? Det gör den inte första gången man gissar
                hint = game.CreateHint(goal, guess);
                ui.Write(hint + "\n");
            }
            game.MakeTopList();
            ShowTopList(game.GetTopList()); // Är detta fult?
            ui.Write("\nCorrect, it took " + game.NumberOfGuesses + " guesses\nContinue?"); // Vill lägga till "Y/N?" för det är mer tydligt för användaren vad hen ska skriva
            answer = ui.Read().ToLower(); // Lade till ToLower så att man användaren blir förstådd om hen använder stora bokstäver            
        }
        while (answer == null || answer == "" || answer.Substring(0, 1) != "n");
    }

    string GetUserName()
    {
        ui.Write("Enter your user name:\n");
        return ui.Read().Trim();

        // Vil vi att det finns något som stoppar användaren från att ha ett tomt namn?
    }

    void ShowTopList(List<PlayerData> topList)
    {
        ui.Write("Player   games average");
        foreach (PlayerData player in topList)
        {
            ui.Write(string.Format("{0,-9}{1,5:D}{2,9:F2}", player.Name, player.numberOfGames, player.Average()));
        }
    }
}
