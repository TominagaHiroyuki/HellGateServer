/*
    @file AuthController.cs
    @brief 認証コントローラー
*/

using HellGateServer.Api.Contracts.Auth;
using HellGateServer.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HellGateServer.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly AuthService _authService;


    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="authService">認証サービス</param>
    public AuthController(ILogger<AuthController> logger, AuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    /// <summary>
    /// サインアップ
    /// </summary>
    /// <param name="request">サインアップリクエスト</param>
    /// <returns>認証レスポンス</returns>
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest request)
    {
        // TODO: サインアップ処理
        _logger.LogDebug("Signup Request:{DeviceGuid}", request.DeviceGuid);

        var result = await _authService.Signup(request);
        if(result is null)
        {
            return Conflict("User already exists.");
        }

        return Ok(ToResponse(result));
    }

    /// <summary>
    /// サインイン
    /// </summary>
    /// <param name="request">サインインリクエスト</param>
    /// <returns>認証レスポンス</returns>
    [HttpPost("signin")]
    public async Task<IActionResult> Signin([FromBody] SigninRequest request)
    {
        _logger.LogDebug("Signin Request:{DeviceGuid} : {CustomerId}", request.DeviceGuid, request.CustomerId);

        var result = await _authService.Signin(request);
        if(result is null)
        {
            return Unauthorized("Invalid credentials.");
        }

        return Ok(ToResponse(result));
    }

    /// <summary>
    /// 認証レスポンスを作成する
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private AuthResponse ToResponse(AuthResult result)
    {
        return new AuthResponse
        {
            UserId = result.User.UserId,
            CustomerId = result.User.CustomerId,
            Token = result.Token,
        };
    }
}
