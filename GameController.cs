using GamesUI;

/* 
  TODOS: 
  - Metoder ska börja med stor bokstav
  - Försök förstå Equals och GetHashCode metoderna - De används när top listan ska visas
  - Bryt ut till mindre metoder som bara gör en sak
  - Gör en controller
  - Gör en klass som heter MooGame som har logiken för det spelet
  - Se om du kan göra en interface som heter IGame för de 2 olika spelen som ska göras
  - Ska det heta IO istället för UI?
 */

namespace Games; // Hette MooGame innan. Finns det ett bättre namn?

class GameController
{
    IUI ui;

    public GameController(IUI ui)
    {
        this.ui = ui;
    }

    public void RunGame()
    {
        IUI ui = new ConsoleUI();

        bool playOn = true;
        ui.Write("Enter your user name:\n");
        string name = ui.Read();

        while (playOn)
        {
            string goal = makeGoal();


            ui.Write("New game:\n");
            //comment out or remove next line to play real games!
            ui.Write("For practice, number is: " + goal + "\n");
            string guess = ui.Read();

            int numberOfGuesses = 1;
            string bbcc = checkBC(goal, guess);
            ui.Write(bbcc + "\n");
            while (bbcc != "BBBB,")
            {
                numberOfGuesses++;
                guess = ui.Read();
                ui.Write(guess + "\n");
                bbcc = checkBC(goal, guess);
                ui.Write(bbcc + "\n");
            }
            StreamWriter output = new StreamWriter("result.txt", append: true);
            output.WriteLine(name + "#&#" + numberOfGuesses);
            output.Close();
            List<PlayerData> topList = GetTopList();
            ui.Write("Player   games average");
            foreach (PlayerData p in topList)
            {
                ui.Write(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.numberOfGames, p.Average()));
            }
            ui.Write("\nCorrect, it took " + numberOfGuesses + " guesses\nContinue?");
            string answer = ui.Read().ToLower();
            if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
            {
                playOn = false;
            }
            // Vill lägga till att konsolen töms om man vill fortsätta spela, så det ser snyggare ut.
        }
    }

    static string makeGoal()
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
        return goal;
    }

    static string checkBC(string goal, string guess) // Byt namn till CheckWinCondition? 
    {
        int bulls = 0, cows = 0;
        guess += "    ";     // if player entered less than 4 chars
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (goal[i] == guess[j])
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
        return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
    }


    static List<PlayerData> GetTopList() // Var void innan
    {
        StreamReader input = new StreamReader("result.txt");
        List<PlayerData> results = new List<PlayerData>();
        string line;
        while ((line = input.ReadLine()) != null)
        {
            string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
            string name = nameAndScore[0];
            int guesses = Convert.ToInt32(nameAndScore[1]);
            PlayerData pd = new PlayerData(name, guesses);
            int pos = results.IndexOf(pd);
            if (pos < 0)
            {
                results.Add(pd);
            }
            else
            {
                results[pos].Update(guesses);
            }


        }
        results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
        //Console.WriteLine("Player   games average");
        //foreach (PlayerData p in results)
        //{
        //    Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.numberOfGames, p.Average()));
        //}
        input.Close();

        return results; // lade till denna för att jag inte vill ha Console.WriteLine, utan vill använda mig av UIn
    }
}
