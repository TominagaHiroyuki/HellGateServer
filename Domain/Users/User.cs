/*
    @file User.cs
    @brief ユーザードメインモデル
*/

namespace HellGateServer.Domain;

public class User
{
    public string UserId { get; set; } = string.Empty; // UUID
    public string CustomerId { get; set; } = string.Empty; // 顧客ID
    public string Name { get; set; } = string.Empty; // ユーザー名
}