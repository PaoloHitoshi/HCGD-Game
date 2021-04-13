using System.Collections.Generic;

public static class MechanicGameReader
{
    private static List<string> _availableMechanics = new List<string> { "Quizz", "Collect", "Puzzle"};
    public static bool IsMechanicAvailable(Game game)
    {
        return _availableMechanics.Contains(game.mechanic_name);
    }
}
