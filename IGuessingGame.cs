namespace Games;

interface IGuessingGame
{
    string UserName { get; set; }
    string Goal { get; set; }
    string Guess { get; set; }
    int NumberOfGuesses { get; set; }
    string GetRules();
    void MakeGoal();
    void HandleGuess(string guess);
    string GetNotProperGuessMessage();
    string GetHint();
    void MakeTopList();

    List<Player> GetTopList();
}