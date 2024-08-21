namespace Games;

public class TopList // Fundera mer på namnet
{
    string fileName = "";

    public TopList(string fileName)
    {
        this.fileName = fileName;
    }
    public void MakeTopList(string userName, int numberOfGuesses) // Är MakeOrEditTopList ett bättre namn?
    {
        StreamWriter output = new StreamWriter(fileName, append: true);
        output.WriteLine(userName + "#&#" + numberOfGuesses);
        output.Close();
    }

    public List<Player> GetTopList() // Gå igenom metoden, förstå hur den funkar och ändra variablerna till tydligare namn
    {
        StreamReader input = new StreamReader(fileName);
        List<Player> results = new List<Player>();
        string line;
        while ((line = input.ReadLine()) != null) // Kan man bryta ut detta till en privat metod?
        {
            string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
            string name = nameAndScore[0];
            int guesses = Convert.ToInt32(nameAndScore[1]);
            Player pd = new Player(name, guesses);
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
}
