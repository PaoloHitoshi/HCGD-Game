public static class Endpoints
{
    //#if DEBUG
    //    public static string PlayerGames = "http://localhost:8000/project/playergames/?user_id=";
    //    public static string GameSession = "http://localhost:8000/project/gamesession/";
    //    public static string Login = "http://localhost:8000/user/login/";
    //#else
    //Digital ocean droplet
    public static string PlayerGames = "http://138.68.245.134:8000/project/playergames/?user_id=";
    public static string GameSession = "http://138.68.245.134:8000/project/gamesession";
    public static string Login = "http://138.68.245.134:8000/user/login/";
    //#endif


}
