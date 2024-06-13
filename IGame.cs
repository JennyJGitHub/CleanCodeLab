namespace Games;

interface IGame
{
    string MakeGoal();
    string CreateHint(string goal, string guess);
    List<PlayerData> GetTopList();
}