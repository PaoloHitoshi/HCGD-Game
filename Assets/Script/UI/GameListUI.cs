using UnityEngine;

public class GameListUI : MonoBehaviour
{
    [SerializeField] private GameObject btnPrefab;
    [SerializeField] private VariableScrollRect scrollRectObj = null;
    [SerializeField] private QuizDataSO activeQuizGame;

    public void PopulateGamesList(GameDataContainer[] games)
    {
        foreach(var game in games)
        {
            SelectableGame instance = scrollRectObj.SpawnItem(btnPrefab).GetComponent<SelectableGame>();
            instance.SetText(game.Name);
            instance.SetGame(game as QuizData, activeQuizGame);
            instance.SetDestination("quiz");
        }
    }
}
