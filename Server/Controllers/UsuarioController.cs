using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorLogin.Shared;

namespace BlazorLogin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (login.Correo == "admin@gmail.com" && login.Clave == "admin")
            {
                var respuesta = new SesionDTO()
                {
                    Nombre = "Administrador",
                    Correo = login.Correo,
                    Rol = "Administrador"
                };
                return Ok(respuesta); // Devuelve 200 OK con SesionDTO
            }
            if (login.Correo == "usuario" && login.Clave == "usuario")
            {
                var respuesta = new SesionDTO()
                {
                    Nombre = "Usuario",
                    Correo = login.Correo,
                    Rol = "Usuario"
                };
                return Ok(respuesta); // Devuelve 200 OK con SesionDTO
            }

            return Unauthorized(); // Devuelve 401 sin cuerpo
        }
    }
}
