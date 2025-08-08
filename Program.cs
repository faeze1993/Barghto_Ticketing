    using Barghto_Ticketing.Auth;
using Barghto_Ticketing.Data;
using Barghto_Ticketing.Interfaces;
using Barghto_Ticketing.Interfaces.Auth;
using Barghto_Ticketing.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SqlLiteDBContext>(opt =>
    opt.UseSqlite("Data Source=TicketingLocalDb.sqllite"));

// Add services to the container.
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddTransient<IJWTTokenService, JWTTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var security = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "bearer [Token]",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(security.Reference.Id, security);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {security, new string[]{} }
            });

});




builder.Services.AddAuthentication(Options =>
{
    Options.DefaultSignInScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
})
                   .AddJwtBearer(configureOptions =>
                   {
                       configureOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                       {
                           ValidIssuer = builder.Configuration.GetSection("JWtConfig")["issuer"],
                           ValidAudience = builder.Configuration.GetSection("JWtConfig")["audience"],
                           IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWtConfig")["Key"])),
                           ValidateIssuerSigningKey = true,
                           ValidateLifetime = true,
                           ClockSkew = TimeSpan.Zero
                       };
                   });
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SqlLiteDBContext>();
    db.Database.Migrate();
    DbSeeder.Seed(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
