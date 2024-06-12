﻿namespace GamesUI;

class ConsoleUI : IUI
{
    public string Read()
    {
        string input = Console.ReadLine();
        return input;
    }

    public void Write(string output)
    {
        Console.WriteLine(output);
    }

    public void Clear()
    {
        Console.Clear();
    }
}
