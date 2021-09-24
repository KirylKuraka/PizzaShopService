using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderAPI.Migrations
{
    public partial class CreateTables_And_IntitalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<Guid>(nullable: false),
                    CustomerName = table.Column<string>(maxLength: 60, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 18, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryMethods",
                columns: table => new
                {
                    DeliveryMethodID = table.Column<Guid>(nullable: false),
                    DeliveryMethodName = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethods", x => x.DeliveryMethodID);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    OrderStatusID = table.Column<Guid>(nullable: false),
                    OrderStatusName = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.OrderStatusID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodID = table.Column<Guid>(nullable: false),
                    PaymentMethodName = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ProductTypeID = table.Column<Guid>(nullable: false),
                    ProductTypeName = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ProductTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(nullable: false),
                    CustomerID = table.Column<Guid>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DeliveryMethodID = table.Column<Guid>(nullable: false),
                    PaymentMethodID = table.Column<Guid>(nullable: false),
                    OrderStatusID = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(maxLength: 250, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryMethods_DeliveryMethodID",
                        column: x => x.DeliveryMethodID,
                        principalTable: "DeliveryMethods",
                        principalColumn: "DeliveryMethodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusID",
                        column: x => x.OrderStatusID,
                        principalTable: "OrderStatuses",
                        principalColumn: "OrderStatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderedProducts",
                columns: table => new
                {
                    OrderedProductID = table.Column<Guid>(nullable: false),
                    ProductTypeID = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    OrderID = table.Column<Guid>(nullable: false),
                    TotalCost = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedProducts", x => x.OrderedProductID);
                    table.ForeignKey(
                        name: "FK_OrderedProducts_ProductTypes_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductTypes",
                        principalColumn: "ProductTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderedProducts_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "DeliveryMethodID", "DeliveryMethodName" },
                values: new object[,]
                {
                    { new Guid("f5ec8ff6-1e86-4a95-b40e-25b592743501"), "Самовывоз" },
                    { new Guid("36f18c03-e238-4df0-bbe7-d71ba7daf1e8"), "Доставка" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "OrderStatusID", "OrderStatusName" },
                values: new object[,]
                {
                    { new Guid("89e487c2-6ec2-4995-8623-c6e8349abe86"), "Обрабатывается" },
                    { new Guid("44a36979-c0c7-4c54-a8c4-76c69ea816a0"), "Подтвержден" },
                    { new Guid("cba8e6c5-6bc4-4d0e-ab21-56ad99b2eae7"), "Отменен" },
                    { new Guid("c14bd298-d7e3-4876-9666-81b8de932f7c"), "Отправлен" },
                    { new Guid("d1717863-4dce-4fc2-9524-648f8e7c4c3f"), "Доставлен" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "PaymentMethodName" },
                values: new object[,]
                {
                    { new Guid("3a45abc3-0a27-4d40-b618-43b800a6d8aa"), "Наличные" },
                    { new Guid("d1f0556e-0691-43fa-a7ff-f2e3bd2fee17"), "Карта" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ProductTypeID", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("05e1586f-f9a2-4aa3-8d90-513655bf4a53"), "Пицца" },
                    { new Guid("304d8855-f87e-4b2b-a4ad-c35e23d2b4ca"), "Напиток" },
                    { new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"), "Соус" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProducts_ProductTypeID",
                table: "OrderedProducts",
                column: "ProductTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProducts_OrderID",
                table: "OrderedProducts",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryMethodID",
                table: "Orders",
                column: "DeliveryMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodID",
                table: "Orders",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusID",
                table: "Orders",
                column: "OrderStatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "DeliveryMethods");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "OrderedProducts");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
