using System.Xml.Linq;

namespace Games;

class MooGame : IGame
{
    public string GetRules()
    {
        return "Rules for Moo:\nTo win you need to guess the right combination of 4 unique numbers. After each guess you get a hint.\n" +
            "For every right number on the right spot you get a B and for every right number on the wrong spot you get a C.\n";
    }

    public string MakeGoal()
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

    public string CreateHint(string goal, string guess)
    {
        // Detta borde hanteras på något annat ställe
        guess += "    ";     // if player entered less than 4 chars

        (int bulls, int cows) = FindBullsAndCows(goal, guess); // Är det ok att skicka vidare samma parametrar?
        return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
    }

    public void MakeTopList(string name, int numberOfGuesses) // Är det clean code att använda samma namn på variablerna och parametrarna? Borde jag ändra?
    {
        StreamWriter output = new StreamWriter("result.txt", append: true);
        output.WriteLine(name + "#&#" + numberOfGuesses);
        output.Close();
    }

    public List<PlayerData> GetTopList()
    {
        StreamReader input = new StreamReader("result.txt"); // Vill ändra namet till resultMooGame så att det finns en annan result för det andra spelet
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
        input.Close();

        return results;
    }

    (int, int) FindBullsAndCows(string goal, string guess)
    {
        // Kan detta göras på ett bättre sätt?
        int bulls = 0, cows = 0;
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
        return (bulls, cows);
    }
}