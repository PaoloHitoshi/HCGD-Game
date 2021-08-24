using UnityEngine;
using GraphQL;
using System.Collections;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class GamesQueryController : MonoBehaviour
{
    public UnityEvent OnQueryBegin;
    public UnityEvent OnQueryEnd;

    [SerializeField] private GameListUI gameListUI;

    public void GetQuizGames()
    {
        StartCoroutine(GetSessionsAsync
        (
            GetSessionOfQL.quizQuery, 
            (sessions) => gameListUI.PopulateGamesList(sessions, "quiz")
        ));
    }

    public void GetEncaixeGames()
    {
        StartCoroutine(GetSessionsAsync
        (
            GetSessionOfQL.encaixeQuery, 
            (sessions) => gameListUI.PopulateGamesList(sessions, "encaixe"))
        );
    }

    private IEnumerator GetSessionsAsync(string query, Action<string> OnSuccess)
    {
        OnQueryBegin.Invoke();

        var request = GetSessionOfQL.HttpGames(Endpoints.url, LoginQL.Token, query);
        yield return new WaitUntil(()=>request.IsCompleted);

        string response = request.Result;
        OnSuccess.Invoke(response);

        OnQueryEnd.Invoke();
    }
}