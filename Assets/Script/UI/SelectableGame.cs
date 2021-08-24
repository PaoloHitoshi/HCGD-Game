using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectableGame : MonoBehaviour
{
    private string _genre;
    private string _gameJson;

    public void SetText(string text)
    {
        GetComponentInChildren<Text>().text = text;
    }

    public void SetGame(string genre, string gameJson)
    {
        _genre = genre;
        _gameJson = gameJson;
    }

    public void SelectGame()
    {
        CurrentGameSession.Set(_genre, _gameJson);
    }

    public void SetDestination(string sceneName)
    {
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(sceneName));
    }
}
