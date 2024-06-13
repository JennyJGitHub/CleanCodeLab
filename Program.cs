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
        // Kommer göra interface och klasser för de olika spelen sen som också ska in i controllern
        // Eller så skapas de i controllern, inte bestämt mig än
        IGame game = new MooGame();
        GameController controller = new(ui, game);
        controller.Run();
    }
}