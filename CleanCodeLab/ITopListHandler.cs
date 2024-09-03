namespace Games;

public interface ITopListHandler
{
    void MakeTopList(string userName, int numberOfGuesses);
    List<Player> GetTopList();
}
