using UnityEngine;
using TMPro;

public class QuizController : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI[] optionsText;

    [Header("SO References")]
    [SerializeField] private QuizDataSO quizDataSO;

    [Header("Feedback Object")]
    [SerializeField] private GameObject feedback;
    
    private QuizGame quizGame;

    private void Awake()
    {
        quizGame = new QuizGame();
        quizGame.Read(quizDataSO);
    }

    private void Start()
    {
        feedback.SetActive(false);
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
        jsonUtils.sendResponse(quizGame.performance);
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
            feedback.SetActive(true);
    }
}
