using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email)) return false;

        Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
         RegexOptions.CultureInvariant | RegexOptions.Singleline);
        
        return regex.IsMatch(email);
    }
}
