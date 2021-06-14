using UnityEngine;

//TODO: Transform into custom editor window
public class QuizCreator : MonoBehaviour
{
    [SerializeField] private QuizData quizz;

    [ContextMenu("Make Quizz JSON")]
    public void MakeQuizzJSON()
    {
        string json = JsonUtility.ToJson(quizz, true);
        System.IO.File.WriteAllText("Assets/Debug/Quizz.json", json);
    }

    [ContextMenu("Read Quizz JSON")]
    public void ReadQuizzJSON()
    {
        string json = System.IO.File.ReadAllText("Assets/Debug/Quizz.json");
        Settings.selectedQuizJSON = json;
        //quizz = new QuizData(json);
    }
}
