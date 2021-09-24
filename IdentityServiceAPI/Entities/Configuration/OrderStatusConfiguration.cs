using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasData
            (
                new OrderStatus
                {
                    OrderStatusID = new Guid("89e487c2-6ec2-4995-8623-c6e8349abe86"),
                    OrderStatusName = "Обрабатывается"
                },
                new OrderStatus
                {
                    OrderStatusID = new Guid("44a36979-c0c7-4c54-a8c4-76c69ea816a0"),
                    OrderStatusName = "Подтвержден"
                },
                new OrderStatus
                {
                    OrderStatusID = new Guid("cba8e6c5-6bc4-4d0e-ab21-56ad99b2eae7"),
                    OrderStatusName = "Отменен"
                },
                new OrderStatus
                {
                    OrderStatusID = new Guid("c14bd298-d7e3-4876-9666-81b8de932f7c"),
                    OrderStatusName = "Отправлен"
                },
                new OrderStatus
                {
                    OrderStatusID = new Guid("d1717863-4dce-4fc2-9524-648f8e7c4c3f"),
                    OrderStatusName = "Доставлен"
                }
            );
        }
    }
}
