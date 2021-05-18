using UnityEngine;
using GraphQL;
using System.Collections;
using UnityEngine.Events;
using System;

[System.Serializable]
public class GamesArrayEvent : UnityEvent<QuizQL.GameDataContainer[]> { }

public class GamesQueryController : MonoBehaviour
{
    public UnityEvent OnQueryBegin;
    public UnityEvent OnQueryEnd;

    public GamesArrayEvent OnSuccess;

    public void GetQuizGames()
    {
        StartCoroutine(GetQuizGamesAsync());
    }

    public IEnumerator GetQuizGamesAsync()
    {
        OnQueryBegin.Invoke();

        var request = QuizQL.HttpQuizGames(Endpoints.url, LoginQL.Token);
        yield return new WaitUntil(()=>request.IsCompleted);

        QuizQL.QuizGameData[] response = request.Result;
        OnSuccess.Invoke(response);
        /*try
        {
            QuizQL.QuizGameData[] response = request.Result;
            OnSuccess.Invoke(response);
        }
        catch (Exception e)
        {
            while (e != null)
            {
                Debug.Log(e.Message);
                e = e.InnerException;
            }
        }*/

        OnQueryEnd.Invoke();
    }

    public void PrintGamesNames(QuizQL.GameDataContainer[] games)
    {
        foreach (var game in games)
            Debug.Log(game.name);
    }
}