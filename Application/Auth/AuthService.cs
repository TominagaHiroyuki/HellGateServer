/*
    @file AuthService.cs
    @brief 認証サービス
*/

using HellGateServer.Domain;
using HellGateServer.Infrastructure.Repository;
using HellGateServer.Api.Contracts.Auth;

namespace HellGateServer.Application.Services;

public class AuthResult
{
    public User User { get; set; } = null!;
    public string Token { get; set; } = string.Empty;
}

public class AuthService
{
    private readonly UserRepository _userRepo;
    private readonly UserDeviceRepository _userDeviceRepo;
    private readonly TokenService _tokenService;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="userRepo"></param>
    /// <param name="userDeviceRepo"></param>
    /// <param name="tokenService"></param>
    public AuthService(UserRepository userRepo, UserDeviceRepository userDeviceRepo, TokenService tokenService)
    {
        _userRepo = userRepo;
        _userDeviceRepo = userDeviceRepo;
        _tokenService = tokenService;
    }

    /// <summary>
    /// サインアップ処理
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<AuthResult?> Signup(SignupRequest request)
    {
        // TODO: サインアップ処理
        if(await IsExistsUser(request.DeviceGuid))
        {
            // 既に存在するユーザーとして扱う
            return null;
        }

        var user = await CreateUserAsync();
        var token = _tokenService.GenerateToken(user);

        return new AuthResult
        {
            User = user,
            Token = token,
        };
    }

    /// <summary>
    /// サインイン処理
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<AuthResult?> Signin(SigninRequest request)
    {
        // ユーザーを取得する
        var user = await _userRepo.GetUserAsync(request.DeviceGuid);
        if(user == null)
        {
            return null!;
        }

        var token = _tokenService.GenerateToken(user);

        return new AuthResult
        {
            User = user,
            Token = token,
        };
    }

    /// <summary>
    /// ユーザーを取得する
    /// </summary>
    /// <param name="deviceGuid"></param>
    /// <returns></returns>
    public async Task<User> GetUserAsync(string deviceGuid)
    {
        var user = await _userRepo.GetUserAsync(deviceGuid);
        return user;
    }

    /// <summary>
    /// ユーザーが存在するかどうかを確認する
    /// </summary>
    /// <param name="deviceGuid"></param>
    /// <returns></returns>
    private async Task<bool> IsExistsUser(string deviceGuid)
    {
        // ある場合は既に存在するユーザーとして扱う
        // ない場合は新規ユーザーとして扱う
        var device = await _userDeviceRepo.GetUserDeviceAsync(deviceGuid);

        return device != null;
    }

    /// <summary>
    /// ユーザーを作成する
    /// </summary>
    /// <returns></returns>
    private async Task<User> CreateUserAsync()
    {
        var user = new User
        {
            UserId = Guid.NewGuid().ToString(),
            Name = "New User",
        };

        user = await _userRepo.CreateUserAsync(user);

        return user;
    }
}