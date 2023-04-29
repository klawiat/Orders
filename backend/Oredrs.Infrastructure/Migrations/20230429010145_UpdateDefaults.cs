using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Oredrs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDefaults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20ac28f3-7bf8-44eb-aaf3-f2050bd7439c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3779d983-32d9-43a5-9ec5-6ea51a172c82"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ff6db325-5cd4-4a58-8460-4f28397a0184"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Oreders",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now() at time zone 'utc'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "timezone('utc', now())");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                defaultValueSql: "timezone('utc', now())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now() at time zone 'utc'");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("20ac28f3-7bf8-44eb-aaf3-f2050bd7439c"), "Ложки" },
                    { new Guid("3779d983-32d9-43a5-9ec5-6ea51a172c82"), "Вилки" },
                    { new Guid("ff6db325-5cd4-4a58-8460-4f28397a0184"), "Ножи" }
                });
        }
    }
}
