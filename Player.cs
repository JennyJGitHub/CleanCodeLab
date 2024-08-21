/* 
  TODOS: 
  - Försök förstå Equals och GetHashCode metoderna
 */

namespace Games;

public class Player
{
    public string Name { get; private set; }
    public int NumberOfGames { get; private set; }
    int totalGuesses;


    public Player(string name, int guesses)
    {
        this.Name = name;
        NumberOfGames = 1;
        totalGuesses = guesses;
    }

    public void Update(int guesses)
    {
        totalGuesses += guesses;
        NumberOfGames++;
    }

    public double Average()
    {
        return (double)totalGuesses / NumberOfGames;
    }

    // Används när result sorteras
    public override bool Equals(Object p)
    {
        return Name.Equals(((Player)p).Name);
    }


    // Hittar inte när denna används
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
