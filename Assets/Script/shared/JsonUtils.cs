using System.Text;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JsonUtils : MonoBehaviour 
{
	private string url = Endpoints.GameSession;

	public void sendResponse(GameResult performance)
	{
		performance.player = Settings.userId;
		string resp = JsonUtility.ToJson(performance, true);
		Debug.Log(resp);
		StartCoroutine(Post(url, resp));
		SceneManager.LoadScene ("Menu", LoadSceneMode.Single);
	}

	IEnumerator Post(string url, string bodyJsonString) 
	{
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
 
        yield return request.SendWebRequest();
    }
}
