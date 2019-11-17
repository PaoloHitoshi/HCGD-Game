using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using UnityEngine.Networking;

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
            //string path = "Assets/Resources/test4.txt";
            //StreamReader reader = new StreamReader(path); 
            //string json = reader.ReadToEnd();
            //reader.Close();
            //string JSONToParse = "{\"configurations\":" + json + "}";
            /*################*/

            string response = unityWebRequest.downloadHandler.text;
            Debug.Log(response);

            string JSONToParse = "{\"configurations\":" + response + "}";
            Debug.Log(JSONToParse);

            Response gameConfigurations = JsonUtility.FromJson<Response>(JSONToParse);

            if (gameConfigurations.configurations.Length != 0) {
                for (int i = 0; i < gameConfigurations.configurations.Length; i++)
                {
                    Debug.Log("init: " + gameConfigurations.configurations[i].game.name);
                    switch (gameConfigurations.configurations[i].game.name)
                    {
                        case "Perguntas":
                            Settings.quiz = gameConfigurations.configurations[i];
                            gameQuiz.SetActive(true);
                            break;
                        case "Encaixe":
                            Settings.puzzle = gameConfigurations.configurations[i];
                            gamePuzzle.SetActive(true);
                            break;
                        case "Coleta":
                            Settings.plataform = gameConfigurations.configurations[i];
                            gamePlataform.SetActive(true);
                            break;
                    }
                }
            }
            else
            {
                log.text = "Nenhum jogo disponível";
            }
        }
    }
}
