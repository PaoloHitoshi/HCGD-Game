public class QuizGame
{
    public QuizData quizData;
    public QuizData.Question CurrentQuestion => quizData.Questions[idQuestion];

    private int idQuestion;
    public GameResult performance;

    public void Read(QuizDataSO dataSO)
    {
        quizData = dataSO.Data;
    }

    /// <summary>
    /// Setup for a new Game
    /// </summary>
    public void NewGame()
    {
        performance = new GameResult();
        //performance.game = quizData.idGame;
        performance.hits = 0;
        performance.fails = 0;
        idQuestion = 0;
    }

    /// <summary>
    /// Measures performance and evaluates given option for current question
    /// </summary>
    /// <param name="option"></param>
    /// <returns>True if option is correct</returns>
    public bool TryAnswer(char option)
    {
        if (idQuestion >= quizData.Questions.Length)
            return false;

        if (CurrentQuestion.Options[(int)option].isAnswer)
        {
            performance.hits++;
            return true;
        }
        else
        {
            performance.fails++;
        }
        return false;
    }

    /// <summary>
    /// Advances to the next question
    /// </summary>
    /// <param name="question"></param>
    /// <returns>true if next question is available</returns>
    public void NextQuestion(out QuizData.Question? question)
    {
        idQuestion++;

        if (idQuestion >= quizData.Questions.Length)
            question = null;
        else
            question = CurrentQuestion;
    }

    /// <summary>
    /// Sets performance score and feeling rate
    /// </summary>
    /// <param name="stars"></param>
    public void SetFeelingRate(int stars)
    {
        performance.feeling_rate = stars;
        performance.score = performance.hits;
    }
}