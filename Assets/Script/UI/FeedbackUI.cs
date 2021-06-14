using TMPro;
using UnityEngine;

public class FeedbackUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private QuizDataSO quizData;


    private void OnEnable()
    {
        contentText.SetText(quizData.Data.Feedback);
    }
}
