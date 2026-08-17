# README
HellGateアプリのサーバーサイドです。

## 前提
- .NET10
- PostgreSQL
- DB名：hellgate


## Install Package
- Scalar.AspNetCore
- Npgsql.EntityFrameworkCore.PostgreSQL
- Microsoft.EntityFrameworkCore.Design

## 実行手順（初回 or スキーマ変更時）
1. User SecretsにDB情報を格納（開発用）
    ```
    dotnet user-secrets set "ConnectionStrings:hellgate" "Host=localhost;Port={port};Database=hellgate;Username={Username};Password={password}"
    ```
2. マイグレーション（スキーマ変更時）
    ```
    dotnet ef migrations add "{name}"
    dotnet ef database update
    ```
3. ```dotnet run / watch```
4. ScalarのURL ```/scalar```

## 実行手順（通常起動）
1. ```dotnet run / watch```
2. ScalarのURL ```/scalar```