namespace Games;

interface IGame
{
    public string UserName { get; set; }
    public int NumberOfGuesses { get; set; }
    string GetRules();
    string MakeGoal();
    string CreateHint(string goal, string guess);
    void MakeTopList();

    List<PlayerData> GetTopList();
}