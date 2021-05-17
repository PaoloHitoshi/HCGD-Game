using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GraphQL;
using System.Threading.Tasks;

public class LoginHandler : MonoBehaviour
{
    private readonly string url = Endpoints.url;
    
    [Header("Login Input")]
    [SerializeField] private InputField username;
    [SerializeField] private InputField password;
    
    [Header("User Feedback")]
    [SerializeField] private GameObject waitingScreen;
    [SerializeField] private Text log;

    private void Awake()
    {
        log.text = string.Empty;
        waitingScreen.SetActive(false);
    }

    public void Submit()
    {
        StartCoroutine(Login(username.text.ToString(), password.text.ToString()));   
    }

    private IEnumerator Login(string username, string password)
    {
        Task<LoginQL.LoginPayload> loginQuery = LoginQL.AsyncLogin(username, password, url);
        waitingScreen.SetActive(true);
        
        yield return new WaitWhile(()=>!loginQuery.IsCompleted);

        LoginQL.LoginPayload payload = loginQuery.Result;
        if (string.IsNullOrEmpty(payload.login.token))
        {
            log.text = payload.login.error?[0];
        }
        else
        {
            LoginQL.Token = payload.login.token;
            SceneManager.LoadScene("menuPrincipal", LoadSceneMode.Single);
        }

        Debug.Log(JsonUtility.ToJson(payload));
        waitingScreen.SetActive(false);
    }
}
