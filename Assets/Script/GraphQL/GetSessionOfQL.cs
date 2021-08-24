using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace GraphQL
{
    public static partial class GetSessionOfQL
    {
        public const string quizQuery = @"{
                              me
                              {
                                Organization
                                {
                                  Sessions 
                                  {
                                    Game
                                    {
        	                            ...on Quiz
                                        {
                                            Name: name,
        		                            Questions :Questions
                                            {
            	                                QuestionText : content,
            	                                Options: Options
            	                                {
              	                                    isAnswer,
              	                                    text:content
            	                                },
                                            }
        	                            },
                                    },
                                    Parameters
                                    {
                                      ...on Feedback
                                      {
                                        Type: __typename,
                                        content
                                      },
                                      ...on FontStyle
                                      {
                                        Type: __typename,
                                        active,
                                      }
                                    }
                                  }
                                }
                              }
                            }";

        public const string encaixeQuery = "";

        public static async Task<string> HttpGames(string url, string token, string query)
        {
            JSONObject response = await GraphQLUtils.HttpAuthQuery(url, query, new EmptyVariables(), token);
            JSONObject sessions = response["Organization"]["Sessions"];

            return sessions.ToString();
        }

        /*private static List<Genre> ExtractGameFromSession<Genre>(JSONObject sessions)
        {
            List<Genre> Games = new();
            sessions.list.ForEach((p) =>
            {
                var session = JsonUtility.FromJson<Session<Genre>>(p.ToString());
                var game = session.Game;

                game.Feedback = session.Parameters.First((_) => _.Type == nameof(GameDataContainer.Feedback)).content;
                game.FontStyle = session.Parameters.First((_) => _.Type == nameof(GameDataContainer.FontStyle)).active;

                Games.Add(game);
            });
        }*/
    }
}