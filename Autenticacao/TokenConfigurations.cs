namespace jwtcore.Autenticacao
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Hours { get; set; }
    }
}