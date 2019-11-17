using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JsonUtils : MonoBehaviour {
	private string url = Endpoints.GameSession;

	public void sendResponse(GameResult performance){
		performance.player = Settings.userId;
		string resp = JsonUtility.ToJson(performance, true);
		Debug.Log(resp);
		StartCoroutine(Post(url, resp));
		SceneManager.LoadScene ("Menu", LoadSceneMode.Single);
	}

	IEnumerator Post(string url, string bodyJsonString) {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
 
        yield return request.SendWebRequest();
 /* 
        Debug.Log("Status Code: " + request.responseCode);
		StringBuilder sb = new StringBuilder();
		foreach (System.Collections.Generic.KeyValuePair<string, string> dict in request.GetResponseHeaders()) {
			sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
		}

		// Print Headers
		Debug.Log(sb.ToString());
		// Print Body
		Debug.Log(request.downloadHandler.text.ToString());
		JSONObject json = new JSONObject(request.downloadHandler.text.ToString());
		*/
    }
}
