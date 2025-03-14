using Microsoft.AspNetCore.Mvc;
using Sistema_Gestion_Tickets.DTOs;
using Sistema_Gestion_Tickets.Services.Implementations;
using Sistema_Gestion_Tickets.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParqueateYA.Ticket.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IAdmin _admin;

		public AdminController(IAdmin admin)
		{
			_admin = admin;
		}

		[HttpGet("spaces")]
		public async Task<ActionResult<IEnumerable<EstacionamientoDTO>>> GetSpaces()
		{
			var espacios = await _admin.ObtenerEstadoEstacionamientos();
			return Ok(espacios);
		}

		[HttpGet("spaces/{tipoVehiculo}")]
		public async Task<ActionResult<EstacionamientoDTO>> GetEstacionamientoByTipoVehiculo(string tipoVehiculo)
		{
			try
			{
				var estacionamiento = await _admin.ObtenerEstacionamientoPorTipoVehiculo(tipoVehiculo);
				return Ok(estacionamiento);
			}
			catch (Exception ex)
			{
				return NotFound(new { message = ex.Message });
			}
		}



		// POST api/<AdminController>
		[HttpPost("login")]
		public async Task<ActionResult<AdminDTO>> Login([FromBody] AdminDTO loginDTO)
		{
			try
			{
				var admin = await _admin.AutenticarAdministrador(loginDTO.NombreUsuario, loginDTO.Contrasena);
				return Ok(admin);
			}
			catch (Exception ex)
			{
				return Unauthorized(new { message = ex.Message });
			}
		}

		[HttpPut("spaces/update")]
		public async Task<ActionResult> UpdateEstacionamiento([FromBody] EstacionamientoDTO estacionamientoDTO)
		{
			try
			{
				await _admin.ActualizarEstacionamiento(estacionamientoDTO);
				return Ok(new { message = "Estacionamiento actualizado exitosamente." });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}


		[HttpPost("spaces/create")]
		public async Task<ActionResult> CreateEstacionamiento([FromBody] EstacionamientoDTO estacionamientoDTO)
		{
			try
			{
				await _admin.CrearEstacionamiento(estacionamientoDTO);
				return Ok(new { message = "Estacionamiento creado exitosamente." });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}
