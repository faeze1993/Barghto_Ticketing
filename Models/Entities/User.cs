using Barghto_Ticketing.Models.Enums;

namespace Barghto_Ticketing.Models.Entities;

public class User
{
    public Guid Id { get; private set; } = default!;
    public string FullName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;
    public UserRole Role { get; private set; } = default!;

    public static User Create(Guid Id, string fullName, string email, string password, UserRole role) => new() 
    { 
        Id = Id,
        FullName = fullName,
        Email = email,
        Password = password,
        Role = role
    };

}


