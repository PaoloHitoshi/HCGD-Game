using System.Collections.Generic;
using UnityEngine;

public static class CurrentGameSession
{
    private static readonly Dictionary<string, string> _currentGameSessions = new Dictionary<string, string>();

    public static void Set(string genre, string gameSessionJson)
    {
        _currentGameSessions[genre] = gameSessionJson;
    }

    public static bool TryGet(string genre, out string gameSessionJson)
    {
        return _currentGameSessions.TryGetValue(genre, out gameSessionJson);
    }

    public static bool TryGetGameOf<Genre>(string genre, out Genre game) where Genre : GameDataContainer
    {
        if(_currentGameSessions.TryGetValue(genre, out string sessionJson))
        {
            Session<Genre> gameSession = JsonUtility.FromJson<Session<Genre>>(sessionJson);
            gameSession.SetGameParameters();
            
            game = gameSession.Game;
            return true;
        }

        game = null;
        return false;
    }
}

