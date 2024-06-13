namespace Games;

interface IGame
{
    string GetRules();
    string MakeGoal();
    string CreateHint(string goal, string guess);
    void MakeTopList(string name, int numberOfGuesses);
    List<PlayerData> GetTopList();
}