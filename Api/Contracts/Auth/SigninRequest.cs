/*
    @file SigninRequest.cs
    @brief サインインリクエスト
*/
namespace HellGateServer.Api.Contracts.Auth;

public class SigninRequest
{
    public string DeviceGuid { get; set; } = string.Empty;
    public long CustomerId { get; set; } = 0L;
}