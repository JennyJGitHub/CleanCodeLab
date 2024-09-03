using GamesUI;

namespace Games;

class Program
{
    public static void Main(string[] args)
    {
        IUI ui = new ConsoleUI();
        GameMenu gameMenu = new(ui);
        gameMenu.Run();
    }
}