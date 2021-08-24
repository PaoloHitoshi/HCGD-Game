using TMPro;
using UnityEngine;

public class FeedbackUI : MonoBehaviour
{
    [SerializeField] private string genre;
    [SerializeField] private TextMeshProUGUI contentText;

    private void OnEnable()
    {
        if (CurrentGameSession.TryGetGameOf(genre, out GameDataContainer gameData))
        {
            contentText.SetText(gameData.Feedback);
        }
    }
}