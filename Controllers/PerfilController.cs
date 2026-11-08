using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PracticaAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilController : ControllerBase
    {
        private readonly AppDb _context;

        public PerfilController(AppDb context)
        {
            _context = context;
        }

        // GET: api/perfil
        [HttpGet]
        public async Task<IActionResult> ObtenerPerfil()
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return Unauthorized();

            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null) return NotFound(new { mensaje = "Usuario no encontrado." });

            var perfilDto = new Model.PerfilDto(
                usuario.Nombre,
                usuario.Disponible,
                usuario.AniosExperiencia
            );

            return Ok(perfilDto);
        }

        // PUT: api/perfil
        [HttpPut]
        public async Task<IActionResult> ActualizarPerfil([FromBody] Model.PerfilDto dto)
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return Unauthorized();

            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null) return NotFound(new { mensaje = "Usuario no encontrado." });

            usuario.Nombre = dto.Nombre;
            usuario.Disponible = dto.Disponible;
            usuario.AniosExperiencia = dto.AniosExperiencia;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Perfil actualizado correctamente." });
        }

        private int? ObtenerUsuarioId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out int id))
            {
                return id;
            }
            return null;
        }
    }
}