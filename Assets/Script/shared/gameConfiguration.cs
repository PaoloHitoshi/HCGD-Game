
[System.Serializable]
public class LoginRequest{
	public string username;
	public string password;
}

[System.Serializable]
public class User{
	public long id;
	public string first_name;
	public string last_name;
	public string username;
}

[System.Serializable]
public class LoginWrapper{
	public string token;
	public User user;
}

[System.Serializable]
public class ResourceType{
	public long id;
	public string name;
}

[System.Serializable]
public class Resource{
	public long id;
	public string content;
	public string rights;
	public bool status;
	public long author;
	public ResourceType resourceType;
	public string institution;
	public string related;
    public string role;
	public int score;
}

[System.Serializable]
public class Response{
	public Configuration[] configurations;
}

[System.Serializable]
public class GameResult{
	public int hits;
    public int fails;
	public int score;
	public int feeling_rate;
	public long game;
	public long player;
}

///////////////////// NEW JSON 10/11 //////////////////////
[System.Serializable]
public class Component{
	public long id;
	public string name;
	public int score;
	public string purpose;
	public string tag;
	public ResourceType[] resourceTypes;
	public Resource[] resources;
}

[System.Serializable]
public class Game{
	public long id;
	public string name;
	public Component[] components;
}

[System.Serializable]
public class Configuration{
	public long id; //Not used
	public Game game;
}