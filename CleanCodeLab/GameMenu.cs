using GamesUI;

namespace Games;

public class GameMenu
{
    IUI ui;
    Dictionary<string, Action> menuOptions;
    bool quitting = false;
    string userName = "";
    string userChoice = "";

    public GameMenu(IUI ui)
    {
        this.ui = ui;
        menuOptions = new Dictionary<string, Action>()
        {
            {"q", () => {Quit(); } },
            {"1", () => {ChangeUserName(); } },
            {"2", () => {StartGame(new MooGame()); } },
            {"3", () => {StartGame(new MastermindGame()); } }
        };
    }

    void Quit()
    {
        quitting = true;
    }

    void ChangeUserName()
    {
        ui.Write("");
        GetUserName();
    }
    void GetUserName()
    {
        string name = "";

        while (name == "")
        {
            ui.Write("Enter your user name:\n");
            name = ui.Read().Trim();
            if (name == "")
            {
                ui.Write("You have to enter a name to continue.\n");
            }
        }

        userName = name;
    }

    void StartGame(IGuessingGame newGame)
    {
        GuessingGameController controller = new(ui, newGame);
        controller.RunGame(userName);
    }

    public void Run()
    {
        try
        {
            GetUserName();
            while (quitting == false)
            {
                userChoice = "";
                ui.Clear();
                ShowGreeting();
                ShowMenu();
                LoopUntilValidChoice();
            }

        }
        catch (Exception exception)
        {
            ui.Write(exception.Message);
        }
    }

    void ShowGreeting()
    {
        ui.Write($"Welcome {userName}, what would you like to do?\n");
    }

    void ShowMenu()
    {
        ui.Write("1. Change name\n2. Play Moo\n3. Play Mastermind\nQ. Quit\n\nEnter the number of your option or Q to quit:\n");
    }

    void LoopUntilValidChoice()
    {
        while (userChoice == "")
        {
            GetUserChoice();
            HandleUserChoice(userChoice);
        }
    }

    void GetUserChoice()
    {
        userChoice = ui.Read().ToLower().Trim();
    }

    void HandleUserChoice(string choice)
    {
        Action? action = menuOptions.GetValueOrDefault(choice);

        if (action != null)
        {
            action();
        }
        else
        {
            ui.Write("\nThat is not one of the options, please try again.\n");
            userChoice = "";
        }
    }
}
