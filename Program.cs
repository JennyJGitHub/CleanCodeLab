using System;
using System.IO;
using System.Collections.Generic;
using GamesUI;

/* 
  TODOS: 
  - Metoder ska börja med stor bokstav
  - Försök förstå Equals och GetHashCode metoderna - De används när top listan ska visas
  - Bryt ut till mindre metoder som bara gör en sak
  - Gör en controller
  - Gör en klass som heter MooGame som har logiken för det spelet
  - Se om du kan göra en interface som heter IGame för de 2 olika spelen som ska göras
  - Ska det heta IO istället för UI?
 */

namespace Games; // Hette MooGame innan. Finns det ett bättre namn?

class MainClass  // Får den heta Program?
{
    public static void Main(string[] args)
    {
        IUI ui = new ConsoleUI();
        // Kommer göra interface och klasser för de olika spelen sen som också ska in i controllern
        GameController controller = new(ui);
        controller.RunGame();
    }
}