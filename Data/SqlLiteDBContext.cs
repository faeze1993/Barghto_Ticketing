using Barghto_Ticketing.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Barghto_Ticketing.Data;

public class SqlLiteDBContext: DbContext
{
    public SqlLiteDBContext(DbContextOptions<SqlLiteDBContext> options):base(options)
    {
        
    }
    public DbSet<User> User { get; set; }
    public DbSet<Ticket> Ticket { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlLiteDBContext).Assembly);
    }
}
