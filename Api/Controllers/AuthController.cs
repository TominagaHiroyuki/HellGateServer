/*
    @file AuthController.cs
    @brief 認証コントローラー
*/

using HellGateServer.Api.Contracts.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HellGateServer.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// サインアップ
    /// </summary>
    /// <param name="request">サインアップリクエスト</param>
    /// <returns>認証レスポンス</returns>
    [HttpPost("signup")]
    public IActionResult Signup([FromBody] SignupRequest request)
    {
        return Ok();
    }

    /// <summary>
    /// サインイン
    /// </summary>
    /// <param name="request">サインインリクエスト</param>
    /// <returns>認証レスポンス</returns>
    [HttpPost("signin")]
    public IActionResult Signin([FromBody] SigninRequest request)
    {
        return Ok();
    }
}
