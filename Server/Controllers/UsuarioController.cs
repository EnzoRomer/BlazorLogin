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
            // Solo permite login si es el usuario admin
            if (login.Correo == "admin@gmail.com" && login.Clave == "admin")
            {
                var respuesta = new ResponseDTO<SesionDTO>()
                {
                    StatusCode = 200,
                    Value = new SesionDTO()
                    {
                        Nombre = "Administrador",
                        Correo = login.Correo,
                        Rol = "Administrador"
                    }
                };
                return Ok(respuesta);
            }

            return Unauthorized(new ResponseDTO<SesionDTO>()
            {
                StatusCode = 401,
                Message = "Credenciales inválidas"
            });
        }
    }
}
