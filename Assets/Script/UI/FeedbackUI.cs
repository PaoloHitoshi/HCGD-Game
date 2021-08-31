using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeedbackUI : MonoBehaviour
{
    [SerializeField] private string genre;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private GameObject starRating;

    private void Awake()
    {
        int i = 0;
        foreach(var star in starRating.transform.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            int rate = i + 1;
            star.onClick.AddListener(() => SetFeelingRate(rate));
            i++;
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);

        if (CurrentGameSession.TryGetGameOf(genre, out GameDataContainer gameData))
        {
            contentText.SetText(gameData.Feedback);
        }
    }

    private void SetFeelingRate(int rate)
    {
        Debug.Log("Feeling rate: " + rate);
        //TODO: Custom scene managment script
        SceneManager.LoadScene("menuPrincipal");
    }
}