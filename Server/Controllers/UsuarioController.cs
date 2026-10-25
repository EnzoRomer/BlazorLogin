using Microsoft.AspNetCore.Mvc;
using BlazorLogin.Shared;
using BlazorLogin.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorLogin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Inyectamos la base de datos
        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            // --- DIAGNÓSTICO 1: Ver qué llega desde Blazor ---
            Console.WriteLine($"--> [DIAGNÓSTICO] Llegó petición. Correo: '{login.Correo}', Clave: '{login.Clave}'");

            try
            {
                // --- DIAGNÓSTICO 2: Ver si conecta a la BD y cuántos usuarios hay ---
                var todosLosUsuarios = await _context.Usuarios.ToListAsync();
                Console.WriteLine($"--> [DIAGNÓSTICO] Conexión a BD exitosa. Usuarios encontrados: {todosLosUsuarios.Count}");

                foreach (var u in todosLosUsuarios)
                {
                    Console.WriteLine($"   -> [BD] Usuario: '{u.Correo}', Clave: '{u.Contrasena}'");
                }

                // --- LÓGICA REAL DEL LOGIN ---
                // Usamos .Trim() y .ToLower() para evitar errores por espacios o mayúsculas
                var usuarioDb = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo.Trim().ToLower() == login.Correo.Trim().ToLower()
                                           && u.Contrasena.Trim() == login.Clave.Trim());

                if (usuarioDb != null)
                {
                    Console.WriteLine("--> [DIAGNÓSTICO] ¡Usuario encontrado! Iniciando sesión.");

                    var respuesta = new SesionDTO()
                    {
                        Nombre = usuarioDb.Nombre,
                        Correo = usuarioDb.Correo,
                        Rol = "Administrador"
                    };
                    return Ok(respuesta);
                }
                else
                {
                    Console.WriteLine("--> [DIAGNÓSTICO] Usuario o clave incorrectos (No hubo coincidencia).");
                }
            }
            catch (Exception ex)
            {
                // --- DIAGNÓSTICO 3: Si hay error de base de datos, lo mostramos ---
                Console.WriteLine($"--> [ERROR FATAL BD] {ex.Message}");
                return StatusCode(500, "Error interno del servidor al conectar con la base de datos.");
            }

            return Unauthorized();
        }
    }
}