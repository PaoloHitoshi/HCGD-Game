using System.Threading.Tasks;
using UnityEngine;

namespace GraphQL
{
    public static class LoginQL
    {
        public static string Token { set; get; }

        public static async Task<LoginPayload> AsyncLogin(string username, string password, string url)
        {
            string query = @"mutation Login($email: EmailAddress!, $password: String!) {
          login(input: { email: $email, password: $password}) {
                    token
                    error
          }
            }";
            return await GraphQLUtils.HttpQuery<LoginInput, LoginPayload>
            (
                url,
                query,
                new LoginInput(username, password)
            );
        }

        #region Input/Payload Structs
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
        #endregion
    }
}
