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
        public async Task<IActionResult> Login([FromBody]LoginDTO login)
        {
            SesionDTO sestionDTO = new SesionDTO();
            if(login.Correo=="admin@gmail.com"&& login.Clave=="admin")
            {
                sestionDTO.Nombre = "admin";
                sestionDTO.Correo = login.Correo;
                sestionDTO.Rol = "Administrador";
            }
            else
            {
                sestionDTO.Nombre = "usuario";
                sestionDTO.Correo = login.Correo;
                sestionDTO.Rol = "Usuario";   
            }
            return StatusCode(StatusCodes.Status200OK,sestionDTO);
        }
    }
}
