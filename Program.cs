/*
    @file Program.cs
    @brief エントリポイント
*/

using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using HellGateServer.Infrastructure.Persistant;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder);

var app = builder.Build();
ConfigurePipeline(app);

app.Run();

/// <summary>
/// サービスを構成する
/// </summary>
/// <param name="builder">WebApplicationBuilder</param>
static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    var connectionString = builder.Configuration.GetConnectionString("hellgate")
                    ?? throw new InvalidOperationException("Connection string 'hellgate' not found.");

    builder.Services.AddDbContext<GameDbContext>(options => options.UseNpgsql(connectionString));
}

/// <summary>
/// パイプラインを構成する
/// </summary>
/// <param name="app">WebApplication</param>
static void ConfigurePipeline(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}