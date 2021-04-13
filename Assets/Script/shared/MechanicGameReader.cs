using System.Collections.Generic;

public static class MechanicGameReader
{
    /// <summary>
    /// Contains available mechanics for this project
    /// </summary>
    private static List<string> _availableMechanics = new List<string> { "Quizz", "Collect", "Puzzle"};
    public static bool IsMechanicAvailable(Game game)
    {
        return _availableMechanics.Contains(game.mechanic_name);
    }
}
