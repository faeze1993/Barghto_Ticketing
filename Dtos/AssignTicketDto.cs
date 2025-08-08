using Barghto_Ticketing.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Barghto_Ticketing.Dtos;

public class AssignTicketDto
{
    [Required]
    public Guid TicketId { get; set; }

    [Required]
    public Guid AdminUserId { get; set; }
}
