/*
    @file UserDevice.cs
    @brief ユーザー端末ドメインモデル
*/

namespace HellGateServer.Domain;

public class UserDevice
{
    public string DeviceGuid { get; set; } = string.Empty; // UUID
}