/*
    @file UserEntity.cs
    @brief ユーザーテーブルエンティティ
*/

using HellGateServer.Domain;

namespace HellGateServer.Infrastructure.Entity;

public class UserEntity
{
    public int Id { get; set; } = 0; // テーブル上のID (Primary Key)
    public string UserId { get; set; } = string.Empty; // UUID (Unique)
    public long CustomerId { get; set; } = 0L; // 顧客ID (Unique)
    public long CreatedAt { get; set; } = 0L; // UnixTime (UTC)
    public string Name { get; set; } = string.Empty; // ユーザー名

    public User ToDomain()
    {
        return new User
        {
            UserId = UserId,
            CustomerId = CustomerId,
            Name = Name,
        };
    }
}