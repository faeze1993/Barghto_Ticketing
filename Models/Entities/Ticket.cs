using Barghto_Ticketing.Models.Enums;

namespace Barghto_Ticketing.Models.Entities;

public class Ticket
{
    public Guid Id { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public TicketStatus Status { get; private set; } = default!;
    public TicketPriority Priority { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; } = default!;
    public DateTime UpdatedAt { get; private set; } = default!;
    public Guid CreatedByUserId { get; private set; } = default!;
    public Guid? AssignedToUserId { get; private set; } = default!;

    public static Ticket Create(string title, string description, TicketPriority priority, Guid createdByUserId) => new()
    {
        Id = Guid.NewGuid(),
        Title = title,
        Description = description,
        Status = TicketStatus.Open,
        Priority = priority,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
        CreatedByUserId = createdByUserId,
        AssignedToUserId = null
    };

    public void AssignTicket(Guid assignedToUserId)
    {
        AssignedToUserId = assignedToUserId;
        Status = TicketStatus.InProgress;
        UpdatedAt = DateTime.Now;
    }

    public void CloseTicket(Guid assignedToUserId)
    {
        Status = TicketStatus.Closed;
    }
}


