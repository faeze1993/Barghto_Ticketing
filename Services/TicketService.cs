using Barghto_Ticketing.Interfaces;
using Barghto_Ticketing.Dtos;
using Barghto_Ticketing.Models.Entities;
using System;
using Barghto_Ticketing.Data;
using Microsoft.EntityFrameworkCore;

namespace Barghto_Ticketing.Services;

public class TicketService : ITicketService
{
    private readonly SqlLiteDBContext _dbContext;
    public TicketService(SqlLiteDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> Add(AddTicketDto model, Guid currentUserId)
    {

        var ticket = Ticket.Create(title: model.Title,description: model.Description,priority: model.Priority,createdByUserId: currentUserId);
        await _dbContext.Ticket.AddAsync(ticket);
        await _dbContext.SaveChangesAsync();

       return true;
    }
    public async Task<bool> AssignTicket(AssignTicketDto model)
    {
        var ticket = _dbContext.Ticket.Where(el => el.Id == model.TicketId).SingleOrDefault()
            ?? throw new Exception("Ticket not found");

        ticket.AssignTicket(model.AdminUserId);

        await _dbContext.SaveChangesAsync();

        return true;
    }
    public async Task<bool> Delete(Guid Id)
    {
        var ticket = _dbContext.Ticket.Where(el => el.Id == Id).SingleOrDefault()
           ?? throw new Exception("Ticket not found");
        _dbContext.Ticket.Remove(ticket);
        await _dbContext.SaveChangesAsync();

        return true;
    }


    public Task<List<TicketDto>> MyTickets(Guid currentUserId)
    {
        return _dbContext.Ticket
            .Where(el => el.CreatedByUserId == currentUserId)
            .Select(el => new TicketDto
        {
            Id = el.Id,
            Title = el.Title,
            Description = el.Description,
            Priority = el.Priority,
            Status = el.Status,
            CreateAt = el.CreatedAt,
            UpdateAt = el.UpdatedAt,
            CreatedByUserId = el.CreatedByUserId,
            AssignedToUserId = el.AssignedToUserId,
            }).ToListAsync();
    }
    public Task<List<TicketDto>> AllTickets()
    {
        return _dbContext.Ticket
          .Select(el => new TicketDto
          {
              Id = el.Id,
              Title = el.Title,
              Description = el.Description,
              Priority = el.Priority,
              Status = el.Status,
              CreateAt = el.CreatedAt,
              UpdateAt = el.UpdatedAt,
              CreatedByUserId = el.CreatedByUserId,
              AssignedToUserId = el.AssignedToUserId,
          }).ToListAsync();
    }
    public async Task<TicketDto> GetTicket(Guid Id)
    {
        return await _dbContext.Ticket.Where(el => el.Id == Id)
        .Select(el => new TicketDto
        {
            Id = el.Id,
            Title = el.Title,
            Description = el.Description,
            Priority = el.Priority,
            Status = el.Status,
            CreateAt = el.CreatedAt,
            UpdateAt = el.UpdatedAt,
            CreatedByUserId = el.CreatedByUserId,
            AssignedToUserId = el.AssignedToUserId,
        }).SingleOrDefaultAsync() ??
        throw new Exception("Ticket not found");
    }
    public async Task<List<TicketReportDto>> TicketsReport()
    {
        return await _dbContext.Ticket
               .GroupBy(t => t.Status)
               .Select(g => new TicketReportDto { Status = g.Key.ToString(), Count = g.Count()})
               .ToListAsync();
    }
}
