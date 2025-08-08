using Barghto_Ticketing.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Barghto_Ticketing.Interfaces;

public interface ITicketService
{
    Task<bool> Add(AddTicketDto model, Guid currentUserId);
    Task<bool> AssignTicket(AssignTicketDto model);
    Task<bool> Delete(Guid Id);

    Task<List<TicketDto>> MyTickets(Guid currentUserId);
    Task<List<TicketDto>> AllTickets();
    Task<TicketDto> GetTicket(Guid Id);
    Task<List<TicketReportDto>> TicketsReport();
}
