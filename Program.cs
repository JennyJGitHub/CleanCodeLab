using System;
using System.IO;
using System.Collections.Generic;
using GamesUI;
using System.Diagnostics.Metrics;

namespace Games; // Finns det ett bättre namn?

class Program
{
    public static void Main(string[] args)
    {
        IUI ui = new ConsoleUI();

        // VG: Skapa meny så användaren kan välja vilket spel som skapas.

        IGuessingGame game = new MooGame();
        GuessingGameController controller = new(ui, game);
        controller.Run();
    }
}