using System.Threading.Tasks;
using UnityEngine;

namespace GraphQL
{
    public static partial class QuizQL
    {
        public static async Task<QuizData[]> HttpQuizGames(string url, string token)
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
            
            return JsonUtility.FromJson<OrganizationData<QuizData>>(orgGames.ToString()).Games;
        }
    }
}
