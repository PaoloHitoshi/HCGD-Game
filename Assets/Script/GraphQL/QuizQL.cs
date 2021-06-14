using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace GraphQL
{
    public static partial class QuizQL
    {
        public static async Task<QuizData[]> HttpQuizGames(string url, string token)
        {
            string query = @"{
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
            JSONObject response = await GraphQLUtils.HttpAuthQuery(url, query, new EmptyVariables(), token);
            
            JSONObject sessions = response["Organization"]["Sessions"];

            List<QuizData> Games = new List<QuizData>();
            sessions.list.ForEach((p) =>
            {
                var session = JsonUtility.FromJson<Session<QuizData>>(p.ToString());
                var game = session.Game;

                game.Feedback = session.Parameters.First((_) => _.Type == nameof(game.Feedback)).content;
                game.FontStyle = session.Parameters.First((_) => _.Type == nameof(game.FontStyle)).active;

                Games.Add(game);
            });

            return Games.ToArray();
        }
    }
}