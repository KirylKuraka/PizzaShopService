using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalAccountAPI.Migrations
{
    public partial class CreatingDB_And_InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 60, nullable: true),
                    LastName = table.Column<string>(maxLength: 60, nullable: true),
                    UserName = table.Column<string>(maxLength: 60, nullable: false),
                    Email = table.Column<string>(maxLength: 60, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 60, nullable: true),
                    PromotionalPoins = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "PromotionalPoins", "UserName" },
                values: new object[] { new Guid("bca0ad85-fea9-4251-9b0b-419b3c839423"), "whitefox@yandex.ru", "Кирилл", "Курако", "+375447045348", 5f, "WhiteFox" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
