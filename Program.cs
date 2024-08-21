using System;
using System.IO;
using System.Collections.Generic;
using GamesUI;
using System.Diagnostics.Metrics;

/* 
  TODOS:
  - Mastermind som andra spel? Hur ska det gå till? (VG)
  - Var ska menyn vara? I Program eller i en ny som heter GameMenu? (VG)
 */

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