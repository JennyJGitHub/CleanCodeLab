namespace GameUI;

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
}
