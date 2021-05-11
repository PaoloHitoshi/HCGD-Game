
[System.Serializable]
public class LoginRequest
{
    public string username;
    public string password;
}

[System.Serializable]
public class User
{
    public long id;
    public string first_name;
    public string last_name;
    public string username;
}

[System.Serializable]
public class LoginWrapper
{
    public string token;
    public User user;
}

[System.Serializable]
public class Response
{
    public Game[] games;
    public Response(Game[] gameArray)
    {
        games = gameArray;
    }
}

[System.Serializable]
public class GameResult
{
    public int hits;
    public int fails;
    public int score;
    public int feeling_rate;
    public long game;
    public long player;
}


[System.Serializable]
public class Game
{
    public long id;
    public string name;
    public string mechanic_name;
    public Component[] components;
}

[System.Serializable]
public class Component
{
    public string name;
    public string purpose;
    public string tag;
    public ComponentField[] fields;
}

[System.Serializable]
public class ComponentField
{
    public string role;
    public string type;
    public Resource resource;
}

[System.Serializable]
public class Resource
{
    public int score;
    public string content;
}

