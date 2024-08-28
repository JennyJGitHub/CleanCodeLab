namespace Games;

interface IGuessingGame
{
    string Goal { get; set; }
    string Guess { get; set; }
    string TopListFileName { get; init; }
    string GetRules();
    void MakeGoal();
    void HandleGuess(string guess);
    string GetInvalidGuessMessage();
    string GetHint();
}