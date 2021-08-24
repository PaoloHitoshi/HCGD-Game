using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundLoad : MonoBehaviour
{
    [SerializeField] private string genre;

    private void Start()
    {
        if(CurrentGameSession.TryGetGameOf(genre, out GameDataContainer gameData))
        {
            if (string.IsNullOrEmpty(gameData.BackgroundUrl)) return;

            StartCoroutine(ChangeTexture(gameData.BackgroundUrl));
        }
    }

    private IEnumerator ChangeTexture(string url)
    {
        StartCoroutine(WebUtils.GetTexture(url));

        yield return new WaitUntil(()=>WebUtils.fetchedTexture != null);

        GetComponent<RawImage>().texture = WebUtils.fetchedTexture;
    }
}