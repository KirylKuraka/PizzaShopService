using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
            (
                new Product
                {
                    ProductID = new Guid("b7dccbda-1963-4b4f-b083-5a17c5e3e6bd"),
                    ProductName = "Додо Микс",
                    Description = "Бекон, цыпленок, ветчина, сыр блю чиз, сыры чеддер и пармезан, соус песто, кубики брынзы, томаты черри, красный лук, моцарелла, соус альфредо, чеснок, итальянские травы",
                    Cost = 19.40F,
                    PromotionalPointsCost = 10F,
                    ProductTypeID = new Guid("05e1586f-f9a2-4aa3-8d90-513655bf4a53")

                },
                new Product
                {
                    ProductID = new Guid("a76313d7-f8ec-40b9-b1f7-d88cb028caed"),
                    ProductName = "Чиззи чеддер",
                    Description = "Ветчина, сыр чеддер, сладкий перец, моцарелла, томатный соус, чеснок, итальянские травы",
                    Cost = 14.40F,
                    PromotionalPointsCost = 7F,
                    ProductTypeID = new Guid("05e1586f-f9a2-4aa3-8d90-513655bf4a53")
                },
                new Product
                {
                    ProductID = new Guid("d4d7e487-9d42-4653-bbe5-a419e9890639"),
                    ProductName = "Шоколадный молочный коктейль",
                    Description = "Напиток из молока и мороженого с шоколадным сиропом",
                    Cost = 5.40F,
                    PromotionalPointsCost = 2.5F,
                    ProductTypeID = new Guid("304d8855-f87e-4b2b-a4ad-c35e23d2b4ca")
                },
                new Product
                {
                    ProductID = new Guid("8c72c183-ecee-4d21-a90c-618185e957e8"),
                    ProductName = "Молочный коктейль с печеньем Орео",
                    Description = "Напиток из молока и мороженого с добавлением дробленого печенья «Орео»",
                    Cost = 5.80F,
                    PromotionalPointsCost = 2.8F,
                    ProductTypeID = new Guid("304d8855-f87e-4b2b-a4ad-c35e23d2b4ca")
                },
                new Product
                {
                    ProductID = new Guid("4400c4c1-ac58-438a-9b23-5209c7621e4b"),
                    ProductName = "Барбекю соус",
                    Description = "Барбекю соус",
                    Cost = 0.6f,
                    PromotionalPointsCost = 0.6F,
                    ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555")
                },
                new Product
                {
                    ProductID = new Guid("52de27a6-5519-4387-b7cf-887365dd0345"),
                    ProductName = "Сырный соус",
                    Description = "Сырный соус",
                    Cost = 0.6f,
                    PromotionalPointsCost = 0.6F,
                    ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555")
                },
                new Product
                {
                    ProductID = new Guid("3b710690-d1f7-459b-b854-8e85ca14750d"),
                    ProductName = "Горчичный  соус",
                    Description = "Горчичный  соус",
                    Cost = 0.6f,
                    PromotionalPointsCost = 0.6F,
                    ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555")
                },
                new Product
                {
                    ProductID = new Guid("f1739067-0e46-4fb7-939d-b4caf93aea80"),
                    ProductName = "Кисло-сладкий соус",
                    Description = "Кисло-сладкий соус",
                    Cost = 0.6f,
                    PromotionalPointsCost = 0.6F,
                    ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555")
                },
                new Product
                {
                    ProductID = new Guid("91128504-95af-45c0-b778-5652f360f7a2"),
                    ProductName = "Терияки соус",
                    Description = "Терияки соус",
                    Cost = 0.6f,
                    PromotionalPointsCost = 0.6F,
                    ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555")
                },
                new Product
                {
                    ProductID = new Guid("26c364cc-83ff-4cf1-8c8d-269f26f9318b"),
                    ProductName = "Чесночный  соус",
                    Description = "Чесночный  соус",
                    Cost = 0.6f,
                    PromotionalPointsCost = 0.6F,
                    ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555")
                }

            );
        }
    }
}
