using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.HasData
            (
                new DeliveryMethod
                {
                    DeliveryMethodID = new Guid("f5ec8ff6-1e86-4a95-b40e-25b592743501"),
                    DeliveryMethodName = "Самовывоз"
                },
                new DeliveryMethod
                {
                    DeliveryMethodID = new Guid("36f18c03-e238-4df0-bbe7-d71ba7daf1e8"),
                    DeliveryMethodName = "Доставка"
                }
            );
        }
    }
}
