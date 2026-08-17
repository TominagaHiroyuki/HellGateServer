/*
    @file AuthResponse.cs
    @brief 認証レスポンス
*/

namespace HellGateServer.Api.Contracts.Auth;

public class AuthResponse
{
    public string UserId { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
}
