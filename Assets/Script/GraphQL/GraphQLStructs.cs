namespace GraphQL
{
    public struct Query<T>
    {
        public string query;
        public T variables;

        public Query(string query, T variables)
        {
            this.query = query;
            this.variables = variables;
        }
    }

    public struct Response<T>
    {
        public T data;
    }

    [System.Serializable]
    public struct AuthorizedResponse<T>
    {
        public Me data;

        [System.Serializable]
        public struct Me
        {
            public T me;
        }

    }

    [System.Serializable]
    public struct EmptyVariables { }
}