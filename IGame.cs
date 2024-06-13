namespace Games;

interface IGame
{
    string MakeGoal();
    string GetHint(string goal, string guess);
    List<PlayerData> GetTopList();
}