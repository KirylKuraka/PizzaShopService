using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductAPI.Migrations
{
    public partial class CreatingProductDB_and_InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    ProductTypeID = table.Column<Guid>(nullable: false),
                    ProductTypeName = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.ProductTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Cost = table.Column<float>(nullable: false),
                    PromotionalPointsCost = table.Column<float>(nullable: false),
                    ProductTypeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_ProductType_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductType",
                        principalColumn: "ProductTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "ProductTypeID", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("05e1586f-f9a2-4aa3-8d90-513655bf4a53"), "Пицца" },
                    { new Guid("304d8855-f87e-4b2b-a4ad-c35e23d2b4ca"), "Напиток" },
                    { new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"), "Соус" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductID", "Cost", "Description", "ProductName", "ProductTypeID", "PromotionalPointsCost" },
                values: new object[,]
                {
                    { new Guid("b7dccbda-1963-4b4f-b083-5a17c5e3e6bd"), 19.4f, "Бекон, цыпленок, ветчина, сыр блю чиз, сыры чеддер и пармезан, соус песто, кубики брынзы, томаты черри, красный лук, моцарелла, соус альфредо, чеснок, итальянские травы", "Додо Микс", new Guid("05e1586f-f9a2-4aa3-8d90-513655bf4a53"), 10f },
                    { new Guid("a76313d7-f8ec-40b9-b1f7-d88cb028caed"), 14.4f, "Ветчина, сыр чеддер, сладкий перец, моцарелла, томатный соус, чеснок, итальянские травы", "Чиззи чеддер", new Guid("05e1586f-f9a2-4aa3-8d90-513655bf4a53"), 7f },
                    { new Guid("d4d7e487-9d42-4653-bbe5-a419e9890639"), 5.4f, "Напиток из молока и мороженого с шоколадным сиропом", "Шоколадный молочный коктейль", new Guid("304d8855-f87e-4b2b-a4ad-c35e23d2b4ca"), 2.5f },
                    { new Guid("8c72c183-ecee-4d21-a90c-618185e957e8"), 5.8f, "Напиток из молока и мороженого с добавлением дробленого печенья «Орео»", "Молочный коктейль с печеньем Орео", new Guid("304d8855-f87e-4b2b-a4ad-c35e23d2b4ca"), 2.8f },
                    { new Guid("4400c4c1-ac58-438a-9b23-5209c7621e4b"), 0.6f, "Барбекю соус", "Барбекю соус", new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"), 0.6f },
                    { new Guid("52de27a6-5519-4387-b7cf-887365dd0345"), 0.6f, "Сырный соус", "Сырный соус", new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"), 0.6f },
                    { new Guid("3b710690-d1f7-459b-b854-8e85ca14750d"), 0.6f, "Горчичный  соус", "Горчичный  соус", new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"), 0.6f },
                    { new Guid("f1739067-0e46-4fb7-939d-b4caf93aea80"), 0.6f, "Кисло-сладкий соус", "Кисло-сладкий соус", new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"), 0.6f },
                    { new Guid("91128504-95af-45c0-b778-5652f360f7a2"), 0.6f, "Терияки соус", "Терияки соус", new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"), 0.6f },
                    { new Guid("26c364cc-83ff-4cf1-8c8d-269f26f9318b"), 0.6f, "Чесночный  соус", "Чесночный  соус", new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"), 0.6f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeID",
                table: "Product",
                column: "ProductTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductType");
        }
    }
}
