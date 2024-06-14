using System.Xml.Linq;

namespace Games;

// Todo:
// Ta en titt på MakeGoal och GetTopList, se om du kan bryta ut det till mindre metoder eller om det kan förbättras på andra sätt.
// Ska vi göra goal till en property? - Blir färre parametrar i våra metoder, man skulle ha så få parametrar som möjligt
// Ska vi göra guess till en property? - Behöver felhantering så att guess inte blir konstig.

// Ha en HandleGuess metod som kastar en exception/ett error om man inte fyllt i 4 siffror. NumberOfGuesses borde inte gå upp om man triggar felet.
// Kanske en GetGuess metod i GameController som använder sig av HandleGuess från spelen (för spelen kanske vill ha olika logik på gissningarna)

class MooGame : IGame
{
    public string UserName { get; set; } = "";
    public int NumberOfGuesses { get; set; } = 0;

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

    public string HandleGuess(string guess)
    {
        // Vill kolla om gissningen bara består av siffror, men vill inte använda guessInt. Finns det ett bättre sätt?
        bool guessIsInt = Int32.TryParse(guess, out int guessInt);
        if (guess.Length != 4 || guessIsInt == false)
            return "";

        return guess;
    }

    public string GetNotProperGuessMessage()
    {
        return "\nYour guess needs to be 4 numbers, please try again.\n";
    }

    public string CreateHint(string goal, string guess)
    {
        (int bulls, int cows) = FindBullsAndCows(goal, guess); // Är det ok att skicka vidare samma parametrar?
        return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
    }

    public void MakeTopList()
    {
        StreamWriter output = new StreamWriter("result.txt", append: true);
        output.WriteLine(UserName + "#&#" + NumberOfGuesses);
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