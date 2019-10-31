namespace JwtCore.Models
{
    public class UsuarioEntity
    {
        public int UsuarioId { get; set; }
        public string Login { get; set; }
        public string RefreshToken { get; set; }
    }
}