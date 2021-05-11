using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private Text questionText;
    [SerializeField] private Text[] optionsText;

    [Header("Feedback Object")]
    [SerializeField] private GameObject feedback;

    private QuizGame quizGame;

    private void Awake()
    {
        quizGame = new QuizGame();
        quizGame.Read(Settings.selectedQuizJSON);
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
        questionText.text = question.QuestionText;
        for (int i = 0; i <= optionsText.Length; i++)
        {
            optionsText[i].text = string.IsNullOrEmpty(question.OptionsText[i]) ? "Nenhuma" : question.OptionsText[i];
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
