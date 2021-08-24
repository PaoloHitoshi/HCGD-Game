using System.Linq;

public class Session<GameGenre> where GameGenre : GameDataContainer
{
    public GameGenre Game;
    public Parameter[] Parameters;

    public void SetGameParameters()
    {
        Game.Feedback = Parameters.First((_) => _.Type == nameof(GameDataContainer.Feedback)).content;
        Game.FontStyle = Parameters.First((_) => _.Type == nameof(GameDataContainer.FontStyle)).active;
    }
}