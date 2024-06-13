using GamesUI;
using System.Diagnostics;

/* 
  TODOS: 
  - Kanske ändra namnet på CheckWinCondition
  - Bryt ut till mindre metoder som bara gör en sak
  - Gör en klass som heter MooGame som har logiken för det spelet
  - Försök förstå Equals och GetHashCode metoderna - De används när top listan ska visas
  - Se om du kan göra en interface som heter IGame för de 2 olika spelen som ska göras (VG)
  - Vill ändra namet på filen result.txt till resultMooGame så att det finns en annan result för det andra spelet (VG)
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
        bool playOn = true;
        ui.Write("Enter your user name:\n");
        string name = ui.Read();

        // För framtiden där det är mer än ett spel
        //ui.Write($"Welcome {name}, which game would you like to play?\n\n1. Moo\n2. Other game\n\nEnter the number:");

        while (playOn) // Ta bort playOn och gör en do while som fortsätter så länge användaren inte svarar n eller no (eller som det redan är med att första bokstaven är n, men ogillar den lite)
        {
            string goal = game.MakeGoal();


            ui.Write("New game:\n"); // Skulle vilja ha med användarens namn här t.ex $"New game for {name}\n"
            //comment out or remove next line to play real games!
            ui.Write("For practice, number is: " + goal + "\n");
            string guess = ui.Read();

            int numberOfGuesses = 1;
            string hint = game.CreateHint(goal, guess);
            ui.Write(hint + "\n");
            while (hint != "BBBB,")
            {
                numberOfGuesses++;
                guess = ui.Read();
                ui.Write(guess + "\n");
                hint = game.CreateHint(goal, guess);
                ui.Write(hint + "\n");
            }
            StreamWriter output = new StreamWriter("result.txt", append: true);
            output.WriteLine(name + "#&#" + numberOfGuesses);
            output.Close();
            ShowTopList(game.GetTopList()); // Är det fult att göra såhär?
            ui.Write("\nCorrect, it took " + numberOfGuesses + " guesses\nContinue?"); // Vill lägga till "Y/N?" för det är mer tydligt för användaren vad hen ska skriva
            string answer = ui.Read().ToLower(); // Lade till ToLower så att man användaren blir förstådd om hen använder stora bokstäver
            if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
            {
                playOn = false;
            }
            // Vill lägga till att konsolen töms om man vill fortsätta spela, så det ser snyggare ut. Eller vill vi ha den längst upp?
            ui.Clear();
        }
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

/* 

Annat alternativ på RunGame med en do while istället för en while loop:

public void RunGame()
{
    string answer;
    ui.Write("Enter your user name:\n");
    string name = ui.Read();

    // För framtiden där det är mer än ett spel
    //ui.Write($"Welcome {name}, which game would you like to play?\n\n1. Moo\n2. Other game\n\nEnter the number:");

    do
    {
        string goal = game.MakeGoal();


        ui.Write("New game:\n"); // Skulle vilja ha med användarens namn här t.ex $"New game for {name}\n"
        //comment out or remove next line to play real games!
        ui.Write("For practice, number is: " + goal + "\n");
        string guess = ui.Read();

        int numberOfGuesses = 1;
        string bbcc = game.CheckWinCondition(goal, guess); // Behöver ändra bbcc till mer passande namn
        ui.Write(bbcc + "\n");
        while (bbcc != "BBBB,")
        {
            numberOfGuesses++;
            guess = ui.Read();
            ui.Write(guess + "\n");
            bbcc = game.CheckWinCondition(goal, guess);
            ui.Write(bbcc + "\n");
        }
        StreamWriter output = new StreamWriter("result.txt", append: true);
        output.WriteLine(name + "#&#" + numberOfGuesses);
        output.Close();
        ShowTopList(game.GetTopList()); // Är det fult att göra såhär?
        ui.Write("\nCorrect, it took " + numberOfGuesses + " guesses\nContinue?"); // Vill lägga till "Y/N?" för det är mer tydligt för användaren vad hen ska skriva
        answer = ui.Read().ToLower(); // Lade till ToLower så att man användaren blir förstådd om hen använder stora bokstäver

        // Vill lägga till att konsolen töms om man vill fortsätta spela, så det ser snyggare ut. Eller vill vi ha den längst upp?
        ui.Clear();
    }
    while (answer == null || answer == "" || answer.Substring(0, 1) != "n");
} 

 */
