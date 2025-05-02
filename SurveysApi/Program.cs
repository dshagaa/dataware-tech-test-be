using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using SurveysApi.Data;
using SurveysApi.Utilities;
using System.Text;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var DbConnectionString = builder.Configuration.GetConnectionString("pgDatabase") ??
    throw new InvalidOperationException("Connection string not found.");

// Add contexts
builder.Services.AddDbContext<DefaultDbContext>(options => options.UseNpgsql(DbConnectionString));
// Add utilities
builder.Services.AddSingleton<JWT>();
builder.Services.AddSingleton<DateTimeUtils>();

// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Set to true in production
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,  // Validate the JWT Issuer (iss) claim
        ValidateAudience = true, // Validate the audience and issuer
        ValidateLifetime = true, // Validate the token expiration
        ClockSkew = TimeSpan.Zero, // Remove delay of token when expired
        ValidateIssuerSigningKey = true, // Validate the token signing key
        ValidIssuer = builder.Configuration["JWT:Issuer"], // Validate the JWT Issuer (iss) claim
        ValidAudience = builder.Configuration["JWT:Audience"], // Validate the audience and issuer
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)) // The key used to sign the token
    };
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
