/*
    @file TokenService.cs
    @description: トークンサービス
*/

using HellGateServer.Domain;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HellGateServer.Application.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="configuration"></param>
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// トークンを生成する
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.PrimarySid, user.UserId),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}