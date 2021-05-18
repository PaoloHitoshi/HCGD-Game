using UnityEngine;

public class GameListUI : MonoBehaviour
{
    [SerializeField] private GameObject btnPrefab;
    [SerializeField] private VariableScrollRect _scrollRectObj = null;

    public void PopulateGamesList(GraphQL.QuizQL.GameDataContainer[] games)
    {
        foreach(var game in games)
        {
            GameObject instance = _scrollRectObj.SpawnItem(btnPrefab);
            instance.GetComponentInChildren<UnityEngine.UI.Text>().text = game.name;
        }
    }
}
