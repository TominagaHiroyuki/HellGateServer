/*
    @file UserDeviceRepository.cs
    @brief ユーザー端末リポジトリ
*/

using HellGateServer.Infrastructure.Entity;
using HellGateServer.Domain;
using HellGateServer.Infrastructure.Persistant;
using Microsoft.EntityFrameworkCore;

namespace HellGateServer.Infrastructure.Repository;

public class UserDeviceRepository
{
    private readonly GameDb _db;
    private readonly ILogger<UserDeviceRepository> _logger;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="db"></param>
    public UserDeviceRepository(GameDb db, ILogger<UserDeviceRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    /// <summary>
    /// 端末情報を取得する
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<UserDevice> GetUserDeviceAsync(string deviceGuid)
    {
        var userDevice = await _db.UserDevices.FirstOrDefaultAsync(x => x.DeviceGuid == deviceGuid);

        // ユーザーが存在しない場合はログを出力して処理を終了する
        if(userDevice == null)
        {
            _logger.LogWarning($"UserDevice not found: {deviceGuid}");
            return default!;
        }

        return ConvertToDomain(userDevice);
    }

    /// <summary>
    /// EntityをDomainに変換する
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private UserDevice ConvertToDomain(UserDeviceEntity entity)
    {
        return new UserDevice
        {
            DeviceGuid = entity.DeviceGuid,
            UserId = entity.UserId,
        };
    }
}