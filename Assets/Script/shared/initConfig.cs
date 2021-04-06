﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class InitConfig : MonoBehaviour 
{
	public Text log;
    public GameObject gameQuiz;
	public GameObject gamePuzzle;
	public GameObject gamePlataform;

	void Start () 
    {
		gameQuiz.SetActive(false);
		gamePuzzle.SetActive(false);
		gamePlataform.SetActive(false);
		string url = $"{Endpoints.PlayerGames}{Settings.userId}";
#if LOCALTEST
        GetPlayerGamesLocal();
#else
        StartCoroutine(GetPlayerGames(url));
#endif
	}

	IEnumerator GetPlayerGames(string url) 
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url))
        {
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.isNetworkError)
            {
                log.text = "Não foi possível obter os jogos, verifique sua conexão";
                Debug.Log(unityWebRequest.error);
            }

            if (unityWebRequest.isHttpError)
            {
                log.text = "Alguma coisa deu errada, tente novamente";
                Debug.Log(unityWebRequest.error);
            }


            string response = unityWebRequest.downloadHandler.text;
            //Debug.Log(response);

            string JSONToParse = "{\"configurations\":" + response + "}";
            //Debug.Log(json);

            Response gameConfigurations = JsonUtility.FromJson<Response>(JSONToParse);

            if (gameConfigurations.games.Length == 0)
            {
                log.text = "Nenhum jogo disponível";
                yield break;
            }

            SetGameConfigurations(gameConfigurations);

        }
    }

#if LOCALTEST
    /// <summary>
    /// Gets player games from local files
    /// </summary>
    private void GetPlayerGamesLocal()
    {
        log.text = "Teste local";
        GamesParser parser = Resources.Load<GamesParser>("Quizz Json Parser");
        
        Response gameConfigurations = new Response(parser.games);
        SetGameConfigurations(gameConfigurations);
    }
#endif

    private void SetGameConfigurations(Response gameConfigurations)
    {
        for (int i = 0; i < gameConfigurations.games.Length; i++)
        {
            Debug.Log("init: " + gameConfigurations.games[i].name);
            switch (gameConfigurations.games[i].mechanic_name)
            {
                case "Quizz":
                    Settings.quiz = gameConfigurations.games[i];
                    gameQuiz.SetActive(true);
                    break;
                case "Puzzle":
                    Settings.puzzle = gameConfigurations.games[i];
                    gamePuzzle.SetActive(true);
                    break;
                case "Collect":
                    Settings.plataform = gameConfigurations.games[i];
                    gamePlataform.SetActive(true);
                    break;
            }
        }
    }
}
