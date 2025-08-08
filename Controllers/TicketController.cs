using Barghto_Ticketing.Interfaces;
using Barghto_Ticketing.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Barghto_Ticketing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromForm] AddTicketDto cmd)
        {
           return Ok(await _ticketService.Add(cmd, GetUserId()));
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid ticketId)
        {
            return Ok(await _ticketService.Delete(ticketId));
        }

        [HttpGet]
        [Route("GetDetailById")]
        public async Task<IActionResult> GetTicket([FromQuery] Guid ticketId)
        {
            return Ok(await _ticketService.GetTicket(ticketId));
        }

        [HttpGet]
        [Route("MyTickets")]
        public async Task<IActionResult> MyTickets()
        {
            return Ok(await _ticketService.MyTickets(GetUserId()));
        }

        private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    }
}
