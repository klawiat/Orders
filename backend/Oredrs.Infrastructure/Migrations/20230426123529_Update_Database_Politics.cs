using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Oredrs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Database_Politics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("205bf706-f81f-414b-864a-fa2de132a3a8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cfe7b970-91ad-4ed7-896d-bedf6f70f42c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0dc7ca6-78d4-423a-bd55-457f566f8b5c"));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Oreders",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Oreders",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 4, 26, 12, 35, 28, 939, DateTimeKind.Utc).AddTicks(8008),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 4, 25, 17, 58, 20, 426, DateTimeKind.Utc).AddTicks(5880));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0f5d184c-85b7-427e-acf4-31b62e2c86a3"), "RandomName" },
                    { new Guid("8e947fa4-3e94-41c8-9f7a-4276517948d2"), "RandomName1" },
                    { new Guid("f1e812a1-9d45-44f9-a186-2d64625e7089"), "RandomName2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0f5d184c-85b7-427e-acf4-31b62e2c86a3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8e947fa4-3e94-41c8-9f7a-4276517948d2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f1e812a1-9d45-44f9-a186-2d64625e7089"));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Oreders",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Oreders",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 4, 25, 17, 58, 20, 426, DateTimeKind.Utc).AddTicks(5880),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 4, 26, 12, 35, 28, 939, DateTimeKind.Utc).AddTicks(8008));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("205bf706-f81f-414b-864a-fa2de132a3a8"), "RandomName2" },
                    { new Guid("cfe7b970-91ad-4ed7-896d-bedf6f70f42c"), "RandomName1" },
                    { new Guid("d0dc7ca6-78d4-423a-bd55-457f566f8b5c"), "RandomName" }
                });
        }
    }
}
