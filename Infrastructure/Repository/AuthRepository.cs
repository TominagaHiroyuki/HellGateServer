/*
    @file AuthRepository.cs
    @brief ユーザー認証周りのリポジトリ操作
*/


using HellGateServer.Application.Interfaces;
using HellGateServer.Domain;
using HellGateServer.Infrastructure.Persistant;
using HellGateServer.Infrastructure.Entity;
using System;

namespace HellGateServer.Infrastructure.Repository;

public class AuthRepository : IAuthRepository
{
    private GameDb _db;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="db"></param>
    public AuthRepository(GameDb db)
    {
        _db = db;
    }

    /// <summary>
    /// ユーザーと端末情報を保存する
    /// </summary>
    /// <param name="user"></param>
    /// <param name="deviceGuid"></param>
    /// <returns></returns>
    public async Task<User> SaveUserAndDeviceAsync(User user, string deviceGuid)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            var userEntity = new UserEntity
            {
                UserId = user.UserId,
                CustomerId = user.CustomerId,
                Name = user.Name,
                CreatedAt = DateTime.UtcNow.Ticks,
            };
            _db.Users.Add(userEntity);
            await _db.SaveChangesAsync();

            var userDeviceEntity = new UserDeviceEntity
            {
                DeviceGuid = deviceGuid,
                UserId = userEntity.Id,
            };
            _db.UserDevices.Add(userDeviceEntity);

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
            return userEntity.ToDomain();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
