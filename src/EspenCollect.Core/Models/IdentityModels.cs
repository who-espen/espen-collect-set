namespace EspenCollect.Core
{

    public class Session
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
