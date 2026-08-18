/*
    @file GameDbContext.cs
    @brief ゲームDBコンテキスト
*/

using Microsoft.EntityFrameworkCore;
using HellGateServer.Infrastructure.Entity;
namespace HellGateServer.Infrastructure.Persistant;


#pragma warning disable CS8618
#pragma warning disable IDE0290

public class GameDb : DbContext
{
    public GameDb(DbContextOptions<GameDb> options) : base(options){}

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserDeviceEntity> UserDevices { get; set; }
}

#pragma warning restore CS8618
#pragma warning restore IDE0290