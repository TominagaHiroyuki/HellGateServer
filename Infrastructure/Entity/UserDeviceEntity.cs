/*
    @file UserDeviceEntity.cs
    @brief ユーザーデバイステーブルエンティティ
*/

namespace HellGateServer.Infrastructure.Entity;

public class UserDeviceEntity
{
    public int Id { get; set; } = 0; // テーブル上のID
    public int UserId { get; set; } = 0; // ユーザーID (UserEntity.Id)
    public string DeviceGuid { get; set;} = string.Empty; // デバイスGUID
    public long AddedAt { get; set; } = 0L; // 追加された日時UnixTime (UTC)
}