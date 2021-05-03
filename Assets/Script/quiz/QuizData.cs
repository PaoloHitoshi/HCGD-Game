using UnityEngine;

[System.Serializable]
public class QuizData : GameMechanicData
{
    [System.Serializable]
    public struct Question
    {
        public string QuestionText;
        public string[] OptionsText;
        public char AnswerIndex;
        //public uint score;
    }

    public Question[] questions;

    public QuizData(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}
