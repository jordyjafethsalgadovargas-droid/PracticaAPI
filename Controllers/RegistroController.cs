using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PracticaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroController : ControllerBase
    {
        private readonly AppDb _context;
        private readonly IPasswordHasher<Model.Usuario> _passwordHasher;

        public RegistroController(AppDb context, IPasswordHasher<Model.Usuario> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] Model.RegistroDto dto)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.Correo == dto.Correo);
            if (existe)
            {
                return BadRequest(new { mensaje = "El correo ya está registrado." });
            }

            var nuevoUsuario = new Model.Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Disponible = true,
                AniosExperiencia = 0
            };

            nuevoUsuario.ClaveHash = _passwordHasher.HashPassword(nuevoUsuario, dto.Clave);

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario registrado exitosamente." });
        }
    }
}