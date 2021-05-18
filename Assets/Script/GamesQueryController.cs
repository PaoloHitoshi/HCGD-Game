using UnityEngine;
using GraphQL;
using System.Collections;
using UnityEngine.Events;
using System;

public class GamesQueryController : MonoBehaviour
{
    public UnityEvent OnQueryBegin;
    public UnityEvent OnQueryEnd;

    public void GetQuizGames()
    {
        StartCoroutine(GetQuizGamesAsync());
    }

    public IEnumerator GetQuizGamesAsync()
    {
        OnQueryBegin.Invoke();

        var request = QuizQL.HttpQuizGames(Endpoints.url, LoginQL.Token);
        yield return new WaitUntil(()=>request.IsCompleted);

        
        try
        {
            QuizQL.QuizGameData[] response = request.Result;
        }
        catch (Exception e)
        {
            while (e != null)
            {
                Debug.Log(e.Message);
                e = e.InnerException;
            }
        }

        OnQueryEnd.Invoke();
    }
}