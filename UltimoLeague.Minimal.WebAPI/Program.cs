global using FastEndpoints;
global using FluentResults;
global using Mapster;
global using UltimoLeague.Minimal.Contracts.Dtos;
global using UltimoLeague.Minimal.Contracts.Requests;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.Extensions.Options;
using System.Data;
using UltimoLeague.Minimal.DAL.Common;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.DAL.Repositories;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Models;
using UltimoLeague.Minimal.WebAPI.Services;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors();
            builder.Services.AddAuthorization();
            builder.Services.AddFastEndpoints();
            builder.Services.AddAuthenticationJWTBearer("BrandNewTokenSigningKey");
            builder.Services.AddAuthorization(o => o.AddPolicy(Policy.AdminOnly, b => b.RequireRole(Roles.Admin)));
            builder.Services.AddSwaggerDoc(settings =>
            {
                settings.Title = "Ultimo League API";
                settings.Version = "v.1.0";
                settings.DocumentName = "Ultimo League API v.1.0";
                settings.UseControllerSummaryAsTagDescription = true;
            }, addJWTBearerAuth: true);          
            builder.Services.AddMappings();
            builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
            builder.Services.AddSingleton<IMongoDbSettings>(sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            builder.Services.AddScoped<SportService>();
            builder.Services.AddScoped<ArenaService>();
            builder.Services.AddScoped<LeagueService>();
            builder.Services.AddScoped<SeasonService>();
            builder.Services.AddScoped<TeamService>();
            builder.Services.AddScoped<PlayerService>();
            builder.Services.AddScoped<RegistrationService>();
            builder.Services.AddScoped<FixtureService>();
            builder.Services.AddScoped<FixtureResultService>();
            builder.Services.AddScoped<StatisticService>();
            builder.Services.AddScoped<SessionService>();

            var app = builder.Build();
            app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseErrorExceptionHandler();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseFastEndpoints(c =>
            {
                c.Endpoints.RoutePrefix = "api";
                c.Endpoints.Configurator = ep =>
                {
                    // ep.AllowAnonymous();
                };
            });
            app.UseOpenApi();
            app.UseSwaggerUi3(s => s.ConfigureDefaults());
            app.UseHttpsRedirection();
            
            app.Run();
        }
    }
}