using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Games Json Parser", menuName = "ScriptableObjects/Game Parser")]
public class GamesParser : ScriptableObject
{
    public string folderPath;
    public string filename;
    public Game[] games;

    [ContextMenu("Generate JSON")]
    public void CreateJSON()
    {
        Response response = new Response(games);
        string json = JsonUtility.ToJson(response, true);
        string path = Path.Combine(folderPath, filename);
        File.WriteAllText(path + ".json", json);
    }
}
