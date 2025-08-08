using Barghto_Ticketing.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Barghto_Ticketing.Dtos;

public class AddTicketDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public TicketPriority Priority { get; set; }
}
