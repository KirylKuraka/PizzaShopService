using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasData
            (
                new PaymentMethod
                {
                    PaymentMethodID = new Guid("3a45abc3-0a27-4d40-b618-43b800a6d8aa"),
                    PaymentMethodName = "Наличные"
                },
                new PaymentMethod
                {
                    PaymentMethodID = new Guid("d1f0556e-0691-43fa-a7ff-f2e3bd2fee17"),
                    PaymentMethodName = "Карта"
                }
            );
        }
    }
}
