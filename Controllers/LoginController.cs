using JwtCore.Data;
using JwtCore.Models;
using JwtCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace jwtcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        [EnableCors("AllowSpecificOrigins")]
        public object Login([FromBody]UsuarioLoginDTO usuarioLogin, [FromServices]JwtService jwtService, [FromServices]UsuarioRepository repository)
        {
            var usuario = repository.Get(usuarioLogin.UsuarioId);

            if (usuario != null)
            {
                var jwtToken = jwtService.CriarToken(usuario);

                usuario.RefreshToken = jwtToken.RefreshToken;
                repository.Save(usuario);

                return new
                {
                    authenticated = true,
                    created = jwtToken.Create.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = jwtToken.Expiration.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = jwtToken.Token,
                    refreshToken = jwtToken.RefreshToken,
                    message = "OK"
                };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }

        [HttpPost]
		[AllowAnonymous]
        [Route("Refresh")]
        [EnableCors("AllowSpecificOrigins")]
        public IActionResult RefreshToken([FromServices]JwtService jwtService, [FromServices]UsuarioRepository repository, [FromBody]UsuarioRefreshDTO usuarioRefresh)
		{
			var principal = jwtService.GetPrincipalFromExpiredToken(usuarioRefresh.AuthenticationToken);
			var username = principal.Identity.Name; //this is mapped to the Name claim by default

            var usuario = repository.Get(usuarioRefresh.UsuarioId);

			if (usuario == null || usuario.Login != username || usuario.RefreshToken != usuarioRefresh.RefreshToken)
                return BadRequest();

            var jwtToken = jwtService.CriarToken(usuario);

			usuario.RefreshToken = jwtToken.RefreshToken;
			repository.Save(usuario);

			return new ObjectResult(new
			{
				accessToken = jwtToken.Token,
				refreshToken = jwtToken.RefreshToken
			});
		}
    }
}