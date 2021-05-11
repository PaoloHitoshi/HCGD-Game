[System.Serializable]
public abstract class GameMechanicData
{
    public string CustomName;

    public string IntroductionMessage;
    public string FeedbackText;

    public string BackgroundImagePath;
    public string FeedbackAudioPath;

    public bool HasFeedback;

    public long idGame;
}