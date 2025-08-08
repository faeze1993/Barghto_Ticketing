namespace Barghto_Ticketing.Dtos;

public class AuthenticateResDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Token { get; set; }
}
