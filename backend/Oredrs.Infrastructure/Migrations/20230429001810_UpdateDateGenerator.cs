using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Oredrs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateGenerator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Oreders",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "timezone('utc', now())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 4, 26, 12, 35, 28, 939, DateTimeKind.Utc).AddTicks(8008));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("864187cd-7b34-4be3-97b2-a559d9758dd7"), "Вилки" },
                    { new Guid("b90d4802-daed-470e-af58-d2489b54b6a3"), "Ложки" },
                    { new Guid("cda1fe8d-3eee-477e-85d8-1695b77b187d"), "Ножи" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("864187cd-7b34-4be3-97b2-a559d9758dd7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b90d4802-daed-470e-af58-d2489b54b6a3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cda1fe8d-3eee-477e-85d8-1695b77b187d"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Oreders",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 4, 26, 12, 35, 28, 939, DateTimeKind.Utc).AddTicks(8008),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "timezone('utc', now())");

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
    }
}
