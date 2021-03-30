using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {
    private readonly string url = Endpoints.Login;
    public InputField username;
	public InputField password;
	public Text results;

	public void Submit() {
        LoginRequest loginRequest = new LoginRequest
        {
            username = username.text.ToString(), //"monica";
            password = password.text.ToString() //"senha123";
        };

        var json = JsonUtility.ToJson(loginRequest, prettyPrint: true);
		StartCoroutine(Post(url, json));
    }

    private IEnumerator Post(string url, string bodyJsonString)
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyRaw)))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.responseCode == 200)
            {
                JSONObject json = new JSONObject(request.downloadHandler.text.ToString());

                if (json["user"]["player_datas"][0]["id"] != null)
                {
                    float id = json["user"]["player_datas"][0]["id"].n;
                    Settings.userId = Convert.ToInt64(id);
                    
                    SceneManager.LoadScene("menuPrincipal", LoadSceneMode.Single);
                }
                else
                {
                    results.text = "Configuração inválida ou usuário não é jogador";
                }
            }
            if (request.isNetworkError)
            {
                results.text = "Não foi possível fazer login, verifique sua conexão";
            }
            else
            {
                results.text = "Usuário ou senha incorretos";
            }
        }
    }
}
