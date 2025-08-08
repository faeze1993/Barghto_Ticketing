using Barghto_Ticketing.Models.Entities;
using Barghto_Ticketing.Models.Enums;
using Barghto_Ticketing.Utilities;
using System;

namespace Barghto_Ticketing.Data;

public static class DbSeeder
{
    public static void Seed(SqlLiteDBContext context)
    {
        if (!context.User.Any())
        {
            context.User.Add(
                Models.Entities.User.Create
                (
                    Id: Guid.Parse("BC70D3B0-A9AB-4423-AE94-9C6E028A6753"),
                    fullName: "Ahmad Ahmadi",
                    email: "Ahmadi@Gmail.com",
                    password: "Employee".ToHashPass(),
                    role: Models.Enums.UserRole.Employee
                ));
            context.User.Add(
                Models.Entities.User.Create
                (
                    Id: Guid.Parse("824DD69F-36E8-4B70-BC56-2AE15AF4D5DA"),
                    fullName: "Farhad Farhadi",
                    email: "Farhad@Gmail.com", 
                    password: "Admin".ToHashPass(),
                    role: Models.Enums.UserRole.Admin
                    )
                );
            context.SaveChanges();
        }
    }
}