using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Endpoints
{
#if DEBUG
    public static string PlayerGames = "http://localhost:8000/project/playergames/?user_id=";
    public static string GameSession = "http://localhost:8000/project/gamesession/";
    public static string Login = "http://localhost:8000/user/login/";
#else
    public static string PlayerGames = "http://lifes.dc.ufscar.br/hcgd-backend-teste/project/playergames/?user_id=";
    public static string GameSession = "http://lifes.dc.ufscar.br/hcgd-backend-teste/project/gamesession";
    public static string Login = "http://lifes.dc.ufscar.br/hcgd-backend-teste/user/login/";
#endif


}
