/*
    @file Program.cs
    @brief エントリポイント
*/

using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using HellGateServer.Infrastructure.Persistant;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HellGateServer.Extentions;

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
    ConfigureJwt(builder);

    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    var connectionString = builder.Configuration.GetConnectionString("hellgate")
                    ?? throw new InvalidOperationException("Connection string 'hellgate' not found.");

    builder.Services.AddDbContext<GameDb>(options => options.UseNpgsql(connectionString));
    builder.Services.RegisterServices();
}

/// <summary>
/// JWT認証の設定
/// </summary>
/// <param name="builder">WebApplicationBuilder</param>
static void ConfigureJwt(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });
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