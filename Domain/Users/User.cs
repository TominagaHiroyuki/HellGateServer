/*
    @file User.cs
    @brief ユーザードメインモデル
*/

namespace HellGateServer.Domain;

public class User
{
    public string UserId { get; set; } = string.Empty; // UUID
    public long CustomerId { get; set; } = 0L; // 顧客ID
    public string Name { get; set; } = string.Empty; // ユーザー名
}