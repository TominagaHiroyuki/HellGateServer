/*
    @file IAuthRepository.cs
    @brief IAuthRepository interface
*/

using HellGateServer.Domain;


namespace HellGateServer.Application.Interfaces;

public interface IAuthRepository
{
    /// <summary>
    /// ユーザーと端末情報を保存する
    /// </summary>
    /// <param name="user"></param>
    /// <param name="deviceGuid"></param>
    /// <returns></returns>
    Task<User> SaveUserAndDeviceAsync(User user, string deviceGuid);
}