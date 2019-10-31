namespace JwtCore.Models
{
    public class UsuarioRefreshDTO
    {
        public int UsuarioId { get; set; }

        public string Login { get; set; }

        public string AuthenticationToken { get; set; }

        public string RefreshToken { get; set; }
    }
}