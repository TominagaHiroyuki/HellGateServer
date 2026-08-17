/*
    @file UserController.cs
    @brief ユーザーコントローラー
*/

using Microsoft.AspNetCore.Mvc;

namespace HellGateServer.Api.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    public UserController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// ユーザー情報取得
    /// </summary>
    /// <param name="id">ユーザーID</param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get(string id)
    {
        return Ok();
    }

    /// <summary>
    /// ユーザー情報更新
    /// </summary>
    /// <param name="id">ユーザーID</param>
    /// <returns></returns>
    [HttpPatch("update")]
    public IActionResult Update(string id)
    {
        return Ok();
    }
}
