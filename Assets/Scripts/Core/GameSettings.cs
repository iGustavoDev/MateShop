public static class GameSettings
{
    public static GameDifficulty SelectedDifficulty { get; private set; } = GameDifficulty.Facil;

    public static void SetDifficulty(GameDifficulty difficulty)
    {
        SelectedDifficulty = difficulty;
    }
}
