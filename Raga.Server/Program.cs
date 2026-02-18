using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Raga.Server;
using Raga.Server.Common.Interfaces;
using Raga.Server.Data;
using Raga.Server.Data.Models;
using Raga.Server.Data.Repositories;
using Raga.Server.Features.Gacha.Commands.PullGacha;
using Raga.Server.Features.Gacha.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

ConfigureConfiguration(builder);
ConfigureLogging(builder.Host);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

EnsureDatabaseCreated(app);
ConfigureRequestPipeline(app);

app.Run();
return;

void ConfigureConfiguration(WebApplicationBuilder builder)
{
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
}

void ConfigureLogging(ConfigureHostBuilder hostBuilder)
{
    hostBuilder.UseSerilog((context, loggerConfig)
        => loggerConfig.ReadFrom.Configuration(context.Configuration));
}

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddGrpc();
    services.AddGrpcReflection();

    services.AddDbContext<GachaContext>(options 
        => options.UseSqlite(configuration.GetConnectionString("GachaDatabase")));

    services.AddScoped<IPlayerRepository, PlayerRepository>();
    services.AddScoped<IGachaItemRepository, GachaItemRepository>();
    services.AddScoped<IClanRepository, ClanRepository>();
    services.AddScoped<IFakeDataGenerator<GachaItem>, GachaItemFaker>();
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PullGachaHandler).Assembly));
    services.AddValidatorsFromAssemblyContaining<PullGachaCommandValidator>();
    services.AddLogging();
}

void EnsureDatabaseCreated(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<GachaContext>();
    db.Database.EnsureCreated();
}

void ConfigureRequestPipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.MapGrpcReflectionService();
    }

    MapGrpcServices(app);

    app.MapGet("/", () =>
        """
        Communication with gRPC endpoints must be made through a gRPC client.
        To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909
        """);
}

void MapGrpcServices(WebApplication app)
{
    app.MapGrpcAuthService();
    app.MapGrpcChatService();
    app.MapGrpcClanService();
    app.MapGrpcGachaService();
    app.MapGrpcInfoService();
    app.MapGrpcLeaderboardService();
    app.MapGrpcMatchmakingService();
    app.MapGrpcNotificationService();
    app.MapGrpcPlayerService();
    app.MapGrpcStoreService();
}