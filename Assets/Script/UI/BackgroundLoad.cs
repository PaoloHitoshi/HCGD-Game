using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundLoad : MonoBehaviour
{
    [SerializeField] private QuizDataSO quizData;

    private void Start()
    {
        if (string.IsNullOrEmpty(quizData.Data.BackgroundUrl)) return;
        StartCoroutine(ChangeTexture());
    }

    private IEnumerator ChangeTexture()
    {
        StartCoroutine(WebUtils.GetTexture(quizData.Data.BackgroundUrl));

        yield return new WaitUntil(()=>WebUtils.fetchedTexture != null);

        GetComponent<RawImage>().texture = WebUtils.fetchedTexture;
    }
}
