/*
    @file AuthService.cs
    @brief 認証サービス
*/

using HellGateServer.Domain;
using HellGateServer.Infrastructure.Repository;
using HellGateServer.Api.Contracts.Auth;
using HellGateServer.Application.Interfaces;

namespace HellGateServer.Application.Services;

public class AuthResult
{
    public User User { get; set; } = null!;
    public string Token { get; set; } = string.Empty;
}

public class SignupResult
{
    public User User { get; set; } = null!;
}

public class AuthService
{
    private readonly UserRepository _userRepo;
    private readonly UserDeviceRepository _userDeviceRepo;
    private readonly TokenService _tokenService;
    private readonly IAuthRepository _authRepo;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="userRepo"></param>
    /// <param name="userDeviceRepo"></param>
    /// <param name="tokenService"></param>
    public AuthService(UserRepository userRepo,
                       UserDeviceRepository userDeviceRepo,
                       TokenService tokenService,
                       IAuthRepository authRepo)
    {
        _userRepo = userRepo;
        _userDeviceRepo = userDeviceRepo;
        _tokenService = tokenService;
        _authRepo = authRepo;
    }

    /// <summary>
    /// サインアップ処理
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<SignupResult?> Signup(SignupRequest request)
    {
        // TODO: サインアップ処理
        if(await IsExistsUser(request.DeviceGuid))
        {
            // 既に存在するユーザーとして扱う
            return null;
        }

        var user = await CreateUserAsync(request.DeviceGuid);
        if(user is null)
        {
            return null;
        }

        return new SignupResult
        {
            User = user,
        };
    }

    /// <summary>
    /// サインイン処理
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<AuthResult?> Signin(SigninRequest request)
    {
        // 該当のデバイスが登録されているか
        var userDevice = await _userDeviceRepo.GetUserDeviceAsync(request.DeviceGuid);
        if(userDevice is null)
        {
            return null!;
        }

        // 該当のユーザーが存在するか
        var user = await _userRepo.GetUserAsync(userDevice.UserId, request.CustomerId);
        if(user is null)
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
    private async Task<User> CreateUserAsync(string deviceGuid)
    {
        var user = new User
        {
            UserId = Guid.NewGuid().ToString(),
            Name = "New User",
        };
        user.CustomerId = BitConverter.ToInt64(Guid.Parse(user.UserId).ToByteArray());

        return await _authRepo.SaveUserAndDeviceAsync(user, deviceGuid);
    }
}