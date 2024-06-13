namespace Games;

interface IGame
{
    string GetRules();
    string MakeGoal();
    string CreateHint(string goal, string guess);
    List<PlayerData> GetTopList();
}