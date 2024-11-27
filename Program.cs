using FogoCertApi.Configuration;
using FogoCertApi.Repository.Interfaces;
using Insight.Database;
using Insight.Database.Reliable;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using NETCore.MailKit.Core;
using NETCore.MailKit.Extensions;
using Swashbuckle.AspNetCore.Filters;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FogoCertApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            var configuration = builder.Configuration;
            var environment = builder.Environment;

            builder.Services.AddScoped<IDbConnection>(_ => new ReliableConnection<SqlConnection>(configuration.GetConnectionString("fogocertification")));

            builder.Services.Configure<JwtOptions>(options => builder.Configuration.GetSection(JwtOptions.ConfigKey).Bind(options));

            builder.Services.AddScoped(x =>
            {
                var connection = x.GetRequiredService<IDbConnection>();
                return connection.AsParallel<IFogoCertApi>();
            });


            builder.Services.AddScoped<IEmailService, EmailService>();


            builder.Services.AddMailKit(optionBuilder =>
            {
                optionBuilder.UseMailKit(new NETCore.MailKit.Infrastructure.Internal.MailKitOptions()
                {
                    Server = configuration["ExternalProviders:MailKit:SMTP:Address"],
                    Port = Convert.ToInt32(configuration["ExternalProviders:MailKit:SMTP:Port"]),
                    Account = configuration["ExternalProviders:MailKit:SMTP:Account"],
                    Password = configuration["ExternalProviders:MailKit:SMTP:Password"],
                    SenderEmail = configuration["ExternalProviders:MailKit:SMTP:SenderEmail"],
                    SenderName = configuration["ExternalProviders:MailKit:SMTP:SenderName"],
                    // Set it to TRUE to enable ssl or tls, FALSE otherwise
                    Security = true
                });
            });




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = HeaderNames.Authorization,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT"
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            }
           );

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {

                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = configuration["Jwt:Issuer"],
                         ValidAudience = configuration["Jwt:Issuer"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                         ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                     };
                 });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}