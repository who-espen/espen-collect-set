namespace EspenCollect.Core
{

    public static class Session
    {
        public static string Id { get; set; }
    }

    public class SessionType
    {
        public string Id { get; set; }
    }

    public class User
    {
        public string IdToken { get; set; }

        public string CommonName { get; set; }

        public string Email { get; set; }
    }

}
