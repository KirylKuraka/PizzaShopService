using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasData
            (
                new ProductType
                {
                    ProductTypeID = new Guid("05e1586f-f9a2-4aa3-8d90-513655bf4a53"),
                    ProductTypeName = "Пицца"
                },
                new ProductType
                {
                    ProductTypeID = new Guid("304d8855-f87e-4b2b-a4ad-c35e23d2b4ca"),
                    ProductTypeName = "Напиток"
                },
                new ProductType
                {
                    ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"),
                    ProductTypeName = "Соус"
                }
            );
        }
    }
}
