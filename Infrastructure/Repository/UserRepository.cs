/*
    @file UserRepository.cs
    @brief ユーザーリポジトリ
*/

using HellGateServer.Infrastructure.Entity;
using HellGateServer.Domain;
using HellGateServer.Infrastructure.Persistant;
using Microsoft.EntityFrameworkCore;

namespace HellGateServer.Infrastructure.Repository;

public class UserRepository
{
    private readonly GameDb _db;
    private readonly ILogger<UserRepository> _logger;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="db"></param>
    public UserRepository(GameDb db, ILogger<UserRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    /// <summary>
    /// ユーザーを取得する
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<User> GetUserAsync(string userId)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.UserId == userId);

        // ユーザーが存在しない場合はログを出力して処理を終了する
        if(user == null)
        {
            _logger.LogWarning("User not found: {UserId}", userId);
            return default!;
        }

        return ConvertToDomain(user);
    }

    /// <summary>
    /// ユーザーを作成する
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<User> CreateUserAsync(User user)
    {
        var userEntity = new UserEntity
        {
            UserId = user.UserId,
            CustomerId = BitConverter.ToInt64(Guid.Parse(user.UserId).ToByteArray()), // ユーザーIDを元にCustomerIdを生成
            Name = user.Name,
        };

        await _db.Users.AddAsync(userEntity);
        await _db.SaveChangesAsync();

        return ConvertToDomain(userEntity);
    }

    /// <summary>
    /// EntityをDomainに変換する
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private User ConvertToDomain(UserEntity entity)
    {
        return new User
        {
            UserId = entity.UserId,
            CustomerId = entity.CustomerId,
            Name = entity.Name,
        };
    }
}