namespace Games;

class PlayerData
{
    public string Name { get; private set; }
    public int numberOfGames { get; private set; }
    int totalGuesses;


    public PlayerData(string name, int guesses)
    {
        this.Name = name;
        numberOfGames = 1;
        totalGuesses = guesses;
    }

    public void Update(int guesses)
    {
        totalGuesses += guesses;
        numberOfGames++;
    }

    public double Average()
    {
        return (double)totalGuesses / numberOfGames;
    }

    // Hittar inte när dessa används
    public override bool Equals(Object p)
    {
        return Name.Equals(((PlayerData)p).Name);
    }


    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
