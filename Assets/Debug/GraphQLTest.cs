using GraphQL;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class GraphQLTest : MonoBehaviour
{
    public string token = string.Empty;

    private void Start()
    {
        StartCoroutine(GetGames());
    }

    public IEnumerator GetGames()
    {
        while (string.IsNullOrEmpty(token))
            yield return null;
        Task.Run(HttpGames);
    }

    public async void HttpGames()
    {
        string query = @"{
                            me{
                            Organization{
                                Games {
                                ... on Quiz {
                                    name,
        	                        Questions :Questions{
                                    QuestionText : content,
                                    Options: Options{
                                        isAnswer,
                                        text:content
                                    }
                                    }
                                }
                                }
                            }
                            }
                        }";
        var user = await GraphQLUtils.HttpAuthQuery<EmptyVariables, UserMe<QuizGameData>>("http://localhost:8000/graphql", query,
            new EmptyVariables(), token);
        
        Debug.Log(user.Organization.Games[0].Questions[0].QuestionText);
        Debug.Log(JsonUtility.ToJson(user.Organization.Games, true));
    }



}

[System.Serializable]
public struct EmptyVariables { }



[System.Serializable]
public struct OrganizationData<Mechanic>
{
    public Mechanic[] Games;
}

[System.Serializable]
public abstract class GameData
{
    public string name;
}

[System.Serializable]
public class QuizGameData
{
    public string name;
    public Question[] Questions;

    [System.Serializable]
    public struct Question
    {
        public string QuestionText;
        public Option[] Options;

        [System.Serializable]
        public struct Option
        {
            public bool isAnswer;
            public string text;
        }
    }
}

[System.Serializable]
public struct UserMe<Mechanic>
{
    public OrganizationData<Mechanic> Organization;
}