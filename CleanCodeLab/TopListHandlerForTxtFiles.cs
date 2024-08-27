/* 
  TODOS:
  - Bättre namn?
 */

namespace Games;

public class TopListHandlerForTxtFiles : ITopListHandler // Fundera mer på namnet
{
    string fileName = "";

    public TopListHandlerForTxtFiles(string fileName)
    {
        this.fileName = fileName;
    }
    public void MakeTopList(string userName, int numberOfGuesses) // Är MakeOrEditTopList ett bättre namn?
    {
        StreamWriter output = new StreamWriter(fileName, append: true);
        output.WriteLine(userName + "#&#" + numberOfGuesses);
        output.Close();
    }

    public List<Player> GetTopList()
    {
        List<Player> results = GetResults();   
        results.Sort((player1, player2) => player1.Average().CompareTo(player2.Average()));

        return results;
    }

    List<Player> GetResults()
    {
        StreamReader input = new StreamReader(fileName);
        List<Player> results = new List<Player>();
        string line;
        while ((line = input.ReadLine()) != null)
        {
            string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
            string name = nameAndScore[0];
            int guesses = Convert.ToInt32(nameAndScore[1]);
            Player player = new Player(name, guesses);
            int position = results.IndexOf(player);
            if (position < 0)
            {
                results.Add(player);
            }
            else
            {
                results[position].Update(guesses);
            }
        }
        input.Close();

        return results;
    }
}
