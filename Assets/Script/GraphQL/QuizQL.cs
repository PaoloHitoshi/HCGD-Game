using System.Threading.Tasks;
using UnityEngine;

namespace GraphQL
{
    public static class QuizQL
    {
        public static async Task<QuizGameData[]> HttpQuizGames(string url, string token)
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
            JSONObject response = await GraphQLUtils.HttpAuthQuery(url, query, new EmptyVariables(), token);
            JSONObject orgGames = response["Organization"];
            
            return JsonUtility.FromJson<OrganizationData<QuizGameData>>(orgGames.ToString()).Games;
        }

        #region PayloadQuiz Structs
        [System.Serializable]
        public struct OrganizationData<MechanicType>
        {
            public MechanicType[] Games;
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
        #endregion
    }
}
