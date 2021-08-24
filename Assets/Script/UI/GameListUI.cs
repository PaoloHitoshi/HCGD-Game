using UnityEngine;

public class GameListUI : MonoBehaviour
{
    [SerializeField] private GameObject btnPrefab;
    [SerializeField] private VariableScrollRect scrollRectObj = null;

    public void PopulateGamesList(string sessions, string genre)
    {
        JSONObject sessionsObj = new JSONObject(sessions);

        foreach(var session in sessionsObj.list)
        {
            SelectableGame instance = scrollRectObj.SpawnItem(btnPrefab).GetComponent<SelectableGame>();

            string json = session.ToString();
            GameDataContainer gameData = JsonUtility.FromJson<Session<GameDataContainer>>(json).Game;
            
            instance.SetText(gameData.Name);
            instance.SetGame(genre, json);
            instance.SetDestination(genre);
        }
    }
}
