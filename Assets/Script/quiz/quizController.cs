using UnityEngine;
using TMPro;

public class QuizController : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI[] optionsText;

    [Header("Feedback")]
    [SerializeField] private FeedbackUI feedbackUI;

    private QuizGame quizGame;

    private void Awake()
    {
        SetCurrentQuiz();
    }

    private void SetCurrentQuiz()
    {
        if (CurrentGameSession.TryGetGameOf("quiz", out QuizData gameData))
        {
            quizGame = new QuizGame();
            quizGame.Read(gameData);
        }
    }

    private void Start()
    {
        quizGame.NewGame();

        SetQuestion(quizGame.CurrentQuestion);
    }

    public void AnswerQuestion(int option)
    {
        quizGame.TryAnswer((char)option);
        NextQuestion();
    }

    public void SetFeelingRate(int stars)
    {
        quizGame.SetFeelingRate(stars);

        JsonUtils jsonUtils = (new GameObject("jsonUtils")).AddComponent<JsonUtils>();
        jsonUtils.sendResponse(quizGame.Performance);
    }

    private void SetQuestion(QuizData.Question question)
    {
        questionText.SetText(question.QuestionText);
        for (int i = 0; i < optionsText.Length; i++)
        {
            optionsText[i].text = "Nenhuma";
            if (question.Options.Length > i && !string.IsNullOrEmpty(question.Options[i].text))
                optionsText[i].SetText(question.Options[i].text);
        }
    }
    private void NextQuestion()
    {
        quizGame.NextQuestion(out QuizData.Question? question);
        if (question.HasValue)
            SetQuestion(question.Value);
        else
            feedbackUI.Open();
    }
}
