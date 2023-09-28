using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehicleManagement.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//jwt
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = "JWTAuthenticationServer",
//            ValidAudience = "JWTServicePostmanClient",
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs0bn")),
//        };


//    });


builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "ReactAccess",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

//Configure the Sql Server Database ConnectionStrings
builder.Services.AddDbContext<VehicleManagementContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("mvcConnection")));


var app = builder.Build();




//jwt
builder.Services.AddAuthorization();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//jwt
app.UseAuthentication();
//jwt
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("ReactAccess");

app.MapControllers();

app.Run();
