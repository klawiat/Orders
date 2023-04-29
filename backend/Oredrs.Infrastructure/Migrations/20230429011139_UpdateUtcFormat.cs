using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Oredrs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUtcFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11145aac-d502-4067-af53-56da26ff9ecb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8104c982-5c6e-4e9e-a6d2-6884be72793e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("86f5fc68-bbff-4e7c-b12f-0e17b78ab1cc"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Oreders",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "timezone('UTC', now())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now() at time zone 'utc'");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("349fdab8-3e16-47aa-9a45-333430ea8436"), "Вилки" },
                    { new Guid("71182654-e033-43c7-9075-6b3732a8bfc0"), "Ножи" },
                    { new Guid("98830bb2-b024-478c-ba44-89dea64947d4"), "Ложки" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("349fdab8-3e16-47aa-9a45-333430ea8436"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("71182654-e033-43c7-9075-6b3732a8bfc0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("98830bb2-b024-478c-ba44-89dea64947d4"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Oreders",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now() at time zone 'utc'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "timezone('UTC', now())");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11145aac-d502-4067-af53-56da26ff9ecb"), "Ножи" },
                    { new Guid("8104c982-5c6e-4e9e-a6d2-6884be72793e"), "Ложки" },
                    { new Guid("86f5fc68-bbff-4e7c-b12f-0e17b78ab1cc"), "Вилки" }
                });
        }
    }
}
