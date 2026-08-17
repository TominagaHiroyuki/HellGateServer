using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HellGateServer.Migrations
{
    /// <inheritdoc />
    public partial class UserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceGuid",
                table: "Users");

            // PostgreSQL は text → bigint を自動変換しないため USING が必要
            migrationBuilder.Sql(
                """ALTER TABLE "Users" ALTER COLUMN "CustomerId" TYPE bigint USING COALESCE(NULLIF("CustomerId", ''), '0')::bigint;""");

            migrationBuilder.CreateTable(
                name: "UserDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DeviceGuid = table.Column<string>(type: "text", nullable: false),
                    AddedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDevices", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDevices");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "DeviceGuid",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
