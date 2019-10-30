using JwtCore.Models;
using JwtCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jwtcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public object Post([FromBody]User usuario, [FromServices] JwtService jwtService)
        {
            bool credenciaisValidas = false;
            if (usuario != null && !string.IsNullOrWhiteSpace(usuario.UserID))
            {
                credenciaisValidas = usuario.UserID == "1";
            }

            if (credenciaisValidas)
            {
                var userAuth = jwtService.CriarToken(usuario);

                return new
                {
                    authenticated = true,
                    created = userAuth.Create.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = userAuth.Expiration.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = userAuth.Token,
                    refreshToken = userAuth.RefreshToken,
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
		public IActionResult RefreshToken([FromServices] JwtService jwtService, [FromBody] User usuario, string authenticationToken, string refreshToken)
		{
			var principal = jwtService.GetPrincipalFromExpiredToken(authenticationToken);
			var username = principal.Identity.Name; //this is mapped to the Name claim by default

			//if (user == null || user.RefreshToken != refreshToken) return BadRequest();

            var userAuth = jwtService.CriarToken(usuario);

			//user.RefreshToken = newRefreshToken;
			//await _context.SaveChangesAsync();

			return new ObjectResult(new
			{
				accessToken = userAuth.Token,
				refreshToken = userAuth.RefreshToken
			});
		}
    }
}