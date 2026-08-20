/*
    @file UserController.cs
    @brief ユーザーコントローラー
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HellGateServer.Infrastructure.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HellGateServer.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly UserRepository _userRepo;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    public UserController(ILogger<AuthController> logger, UserRepository userRepo)
    {
        _logger = logger;
        _userRepo = userRepo;
    }

    /// <summary>
    /// ユーザー情報取得
    /// </summary>
    /// <param name="id">ユーザーID</param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        // sub は NameIdentifier にマップされることがある
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (userId is null)
        {
            return Unauthorized();
        }

        var user = await _userRepo.GetUserAsync(userId);
        if(user is null)
        {
            return NotFound();
        }
        return Ok(user);
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
