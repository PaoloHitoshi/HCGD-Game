using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectableGame : MonoBehaviour
{
    private QuizDataSO _data;
    private QuizData _gameData;

    public void SetText(string text)
    {
        GetComponentInChildren<Text>().text = text;
    }

    public void SetGame(QuizData gameData, QuizDataSO dataSO)
    {
        _data = dataSO;
        _gameData = gameData;
    }

    public void SelectGame()
    {
        _data.Data = _gameData;
    }

    public void SetDestination(string sceneName)
    {
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(sceneName));
    }
}
