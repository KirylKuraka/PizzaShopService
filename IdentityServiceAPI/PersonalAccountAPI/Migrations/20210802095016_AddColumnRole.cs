using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalAccountAPI.Migrations
{
    public partial class AddColumnRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Accounts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bca0ad85-fea9-4251-9b0b-419b3c839423"),
                column: "Role",
                value: "Admin - Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Accounts");
        }
    }
}
