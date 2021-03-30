using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Quizz Json Parser", menuName = "ScriptableObjects/Quizz")]
public class QuizzSO : ScriptableObject
{
    public Game[] games;

    [ContextMenu("Generate JSON")]
    public void CreateJSON()
    {
        var json = JsonUtility.ToJson(this);

        File.WriteAllText("Assets/Resources/QuizzExample_.json", json);
    }
}
