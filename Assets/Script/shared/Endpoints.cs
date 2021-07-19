public static class Endpoints
{
#if LOCALTEST
        public static readonly string url = "http://localhost:8000/graphql";
#else
        public static readonly string url = "https://rufus-api.herokuapp.com/graphql";
#endif


}
