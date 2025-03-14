using Microsoft.AspNetCore.Mvc;
using Sistema_Gestion_Tickets.DTOs;
using Sistema_Gestion_Tickets.Models;
using Sistema_Gestion_Tickets.Repositories.Interfaces;
using Sistema_Gestion_Tickets.Services.Implementations;
using Sistema_Gestion_Tickets.Services.Interfaces;


namespace ParqueateYA.Ticket.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VehicleController : ControllerBase
	{


		private readonly IVehicle vehicle;

		public VehicleController(IVehicle vehicleRepository) 
		{
		 this.vehicle = vehicleRepository;
		}


		// GET: api/<TikcketController>
		[HttpGet("GetTickets")]
		public async Task<IActionResult> GetTickets()
		{
			var result = await vehicle.ObtenerTicketsActivos();
			if (result == null)
				return NotFound("No se encontraron datos");

			return Ok(result);
		}

		// GET api/<TikcketController>/5
		[HttpGet("{codigo}")]
		public async Task<ActionResult<TicketDTO>> GetTicketByCodigo(string codigo)
		{
			var ticket = await vehicle.ObtenerTicketPorCodigo(codigo);
			if (ticket == null)
				return NotFound(new { message = "Ticket no encontrado." });

			return Ok(ticket);
		}


		// POST /api/vehicles/entry
		[HttpPost("entry")]
		public async Task<ActionResult<TicketDTO>> Entry([FromBody] TicketViewModel vehiculoDTO)
		{
			try
			{
				var ticket = await vehicle.GenerarTicket(vehiculoDTO);
				return Ok(ticket);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpPost("exit")]
		public async Task<ActionResult> Exit([FromBody] TicketExitDTO exitDTO)
		{
			try
			{
				var resultado = await vehicle.RegistrarSalida(exitDTO.CodigoTicket);
				return Ok(resultado);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}


		[HttpPut("update")]
		public async Task<ActionResult> UpdateTicket([FromBody] TicketDTO ticketDTO)
		{
			try
			{
				await vehicle.ActualizarTicket(ticketDTO);
				return Ok(new { message = "Ticket actualizado exitosamente." });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpDelete("{codigo}")]
		public async Task<ActionResult> DeleteTicket(string codigo)
		{
			try
			{
				bool eliminado = await vehicle.EliminarTicket(codigo);

				if (eliminado)
				{
					return Ok(new { message = "Ticket eliminado exitosamente." });
				}
				else
				{
					return NotFound(new { message = "Ticket no encontrado." });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}
