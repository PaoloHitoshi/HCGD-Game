using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class WebUtils
{
    public static Texture2D fetchedTexture;
    public static AudioClip fetchedAudio;

    public static IEnumerator GetTexture(string url)
    {
        fetchedTexture = null;
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                fetchedTexture = DownloadHandlerTexture.GetContent(uwr);
            }
        }
    }

    private static IEnumerator GetAudio(string url, AudioType type = AudioType.UNKNOWN)
    {
        fetchedAudio = null;
        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(url, type))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                fetchedAudio = DownloadHandlerAudioClip.GetContent(uwr);
            }
        }
    }
}
