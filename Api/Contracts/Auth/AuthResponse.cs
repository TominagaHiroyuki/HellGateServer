/*
    @file AuthResponse.cs
    @brief 認証レスポンス
*/

namespace HellGateServer.Api.Contracts.Auth;

public class AuthResponse
{
    public string UserId { get; set; } = string.Empty;
    public long CustomerId { get; set; } = 0L;
    public string Token { get; set; } = string.Empty;
}
