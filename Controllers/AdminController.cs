using Barghto_Ticketing.Dtos;
using Barghto_Ticketing.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barghto_Ticketing.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly ITicketService _ticketService;
    public AdminController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPut]
    [Route("AssignTicket")]
    public async Task<IActionResult> AssignTicket([FromForm] AssignTicketDto cmd)
    {
        return Ok(await _ticketService.AssignTicket(cmd));
    }

    [HttpGet]
    [Route("AllTickets")]
    public async Task<IActionResult> AllTickets()
    {
        return Ok(await _ticketService.AllTickets());
    }

    [HttpGet]
    [Route("TicketsReport")]
    public async Task<IActionResult> TicketsReport()
    {
        return Ok(await _ticketService.TicketsReport());
    }
}
