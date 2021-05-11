using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;

public class GraphQLTest : MonoBehaviour
{
    private void Start()
    {
        Login();
    }

    [ContextMenu("Login")]
    public void Login()
    {
        Task.Run(HttpLogin);
    }

    public async void HttpLogin()
    {
        var query = new Query<LoginInput>(@"mutation Login($email: EmailAddress!, $password: String!) {
  login(input: { email: $email, password: $password}) {
            token
            error
  }
    }", new LoginInput("admin@rufus.com", "123456"));

        string json = JsonUtility.ToJson(query, true);
        Debug.Log("meu Json:\n" + json);

        var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var httpClient = new HttpClient();


        var result = await httpClient.PostAsync("http://localhost:8000/graphql", httpContent);
        
        string response = await result.Content.ReadAsStringAsync();

        Response<LoginPayload> payload = JsonUtility.FromJson<Response<LoginPayload>>(response);

        Debug.Log(payload.data.login.token);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", payload.data.login.token);
    }



}

[System.Serializable]
public struct LoginInput
{    
    public string email;
    public string password;

    public LoginInput(string _email, string _password)
    {
        email = _email;
        password = _password;
    }
}

[System.Serializable]
public struct LoginPayload
{
    public LoginData login;
    
    [System.Serializable]
    public struct LoginData
    {
        public string token;
        public string[] error;
    }

    public LoginPayload(string token, string[] error) => login = new LoginData { token = token, error = error };
}