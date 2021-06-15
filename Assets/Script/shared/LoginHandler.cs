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
    [Space(5)]
    [SerializeField] private string connectionErrorMsg = "Ocorreu um erro, por favor tente novamente";
    [SerializeField] private string invalidCredentialsMsg = "Usuario ou senha incorretos";
    [SerializeField] private string invalidEmailFormatMsg = "Formato de email invalido para usuario";
    [SerializeField] private string incompleteInput = "Por favor insira seu usuario e senha";
    

    private void Awake()
    {
        log.text = string.Empty;
        waitingScreen.SetActive(false);
#if LOCALTEST
        //StartCoroutine(Login("admin@rufus.com", "123456"));
#endif
    }

    public void Submit()
    {
        log.text = string.Empty;

        if (string.IsNullOrEmpty(username.text) || string.IsNullOrEmpty(password.text))
            log.text = incompleteInput;
        else
            StartCoroutine(Login(username.text.ToString(), password.text.ToString()));   
    }

    private IEnumerator Login(string username, string password)
    {
        Task<LoginQL.LoginPayload> loginQuery = LoginQL.AsyncLogin(username, password, url);
        waitingScreen.SetActive(true);
        
        yield return new WaitWhile(()=>!loginQuery.IsCompleted);

        if(loginQuery.IsFaulted || loginQuery.IsCanceled)
        {
            log.text = connectionErrorMsg;
        }
        else
        {
            LoginQL.LoginPayload payload = loginQuery.Result;
            if (string.IsNullOrEmpty(payload.login.token))
            {
                if (payload.login.error != null && payload.login.error[0].Contains("invalid")) 
                {
                    log.text = invalidCredentialsMsg;
                }
                else
                {
                    if (!WebUtils.IsValidEmail(username))
                    {
                        log.text = invalidEmailFormatMsg;
                    }
                }
            }
            else
            {
                LoginQL.Token = payload.login.token;
                SceneManager.LoadScene("menuPrincipal", LoadSceneMode.Single);
            }
        }

        waitingScreen.SetActive(false);
    }
}
