using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;

namespace GraphQL
{
    public static class GraphQLUtils
    {
        public static async Task<P> HttpQuery<Q, P>(string url, string query, Q variables)
        {
            Query<Q> queryObj = new Query<Q>(query, variables);

            string json = JsonUtility.ToJson(queryObj, true);
            Debug.Log(nameof(HttpQuery) + ":\n" + json);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage result = await httpClient.PostAsync(url, httpContent);

            string responseContent = await result.Content.ReadAsStringAsync();

            Response<P> response = JsonUtility.FromJson<Response<P>>(responseContent);

            return response.data;
        }

        public static async Task<P> HttpAuthQuery<Q, P>(string url, string query, Q variables, string authtoken)
        {
            Query<Q> queryObj = new Query<Q>(query, variables);

            string json = JsonUtility.ToJson(queryObj, true);
            Debug.Log(nameof(HttpAuthQuery) + ":\n" + json);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authtoken);

            HttpResponseMessage result = await httpClient.PostAsync(url, httpContent);
            string responseContent = await result.Content.ReadAsStringAsync();
            
            Debug.Log(nameof(responseContent) + ":\n" + responseContent);
            AuthorizedResponse<P> response = JsonUtility.FromJson<AuthorizedResponse<P>>(responseContent);

            return response.data.me;
        }

        public static string ReadFromFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                return File.ReadAllText(filepath);
            }
            return string.Empty;
        }

        public static string JsonRead(params string[] filepaths)
        {
            string filepath = Path.Combine(filepaths);
            if (File.Exists(filepath))
            {
                return File.ReadAllText(filepath);
            }
            return string.Empty;
        }
    }
}