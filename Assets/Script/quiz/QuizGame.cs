public class QuizGame
{
    public QuizData.Question CurrentQuestion => _quizData.Questions[_idQuestion];
    public GameResult Performance;

    private QuizData _quizData;
    private int _idQuestion;

    public void Read(QuizData data)
    {
        _quizData = data;
    }

    /// <summary>
    /// Setup for a new Game
    /// </summary>
    public void NewGame()
    {
        Performance = new GameResult();
        //performance.game = quizData.idGame;
        Performance.hits = 0;
        Performance.fails = 0;
        _idQuestion = 0;
    }

    /// <summary>
    /// Measures performance and evaluates given option for current question
    /// </summary>
    /// <param name="option"></param>
    /// <returns>True if option is correct</returns>
    public bool TryAnswer(char option)
    {
        if (_idQuestion >= _quizData.Questions.Length)
            return false;

        if (CurrentQuestion.Options[(int)option].isAnswer)
        {
            Performance.hits++;
            return true;
        }
        else
        {
            Performance.fails++;
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
        _idQuestion++;

        if (_idQuestion >= _quizData.Questions.Length)
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
        Performance.feeling_rate = stars;
        Performance.score = Performance.hits;
    }
}