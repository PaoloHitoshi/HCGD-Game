[System.Serializable]
public class QuizData : GameDataContainer
{
    public Question[] Questions;

    [System.Serializable]
    public struct Question
    {
        public string QuestionText;
        public Option[] Options;

        [System.Serializable]
        public struct Option
        {
            public bool isAnswer;
            public string text;
        }

    }
}