namespace Games;

public interface ITopListHandler // Bättre namn?
{
    void MakeTopList(string userName, int numberOfGuesses);
    List<Player> GetTopList();
}
