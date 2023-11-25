using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UrlShortener_GT.Proyecto.Data;
using UrlShortener_GT.Proyecto.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UrlShortener_GT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(setupAction =>
            {
                setupAction.AddSecurityDefinition("UrlShortenerBearerAuth", new OpenApiSecurityScheme()
                
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = "Paste Token here."
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "UrlShortenerBearerAuth" } 
                }, new List<string>() }
    });
            });
            builder.Services.AddDbContext<UrlShortenerContext>(dbContextOptions => dbContextOptions.UseSqlite(
            builder.Configuration["ConnectionStrings:UrlShortenerDBConnectionString"]));
            builder.Services.AddAuthentication("Bearer") 

    .AddJwtBearer(options => 
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
);
            builder.Services.AddScoped<UrlShortenerService>();
            builder.Services.AddScoped<UserService>();
            var app = builder.Build();

           
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
        }
    }
}