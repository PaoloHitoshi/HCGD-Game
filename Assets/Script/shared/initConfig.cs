using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class InitConfig : MonoBehaviour {
	public Text log;
    public GameObject gameQuiz;
	public GameObject gamePuzzle;
	public GameObject gamePlataform;

	void Start () {
		gameQuiz.SetActive(false);
		gamePuzzle.SetActive(false);
		gamePlataform.SetActive(false);
		string url = $"{Endpoints.PlayerGames}{Settings.userId}";

		StartCoroutine(GetPlayerGames(url));
	}

	IEnumerator GetPlayerGames(string url) {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url)) {
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.isNetworkError)  {
				log.text = "Não foi possível obter os jogos, verifique sua conexão";
                Debug.Log(unityWebRequest.error);
            }

            if (unityWebRequest.isHttpError)
            {
                log.text = "Alguma coisa deu errada, tente novamente";
                Debug.Log(unityWebRequest.error);
            }

            /*  TEST LOCAL */
            string path = "Assets/Resources/QuizzExample_.json";
            StreamReader reader = new StreamReader(path); 
            string json = reader.ReadToEnd();
            reader.Close();
            //string JSONToParse = "{\"configurations\":" + json + "}";
            /*################*/

            //string response = unityWebRequest.downloadHandler.text;
            //Debug.Log(response);

            //string JSONToParse = "{\"configurations\":" + response + "}";
            Debug.Log(json);

            Response gameConfigurations = JsonUtility.FromJson<Response>(json);

            if (gameConfigurations.games.Length == 0)
            {
                log.text = "Nenhum jogo disponível";
                yield break;
            }


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
}
