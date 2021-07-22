using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasData
            (
                new Account
                {
                    UserID = new Guid("bca0ad85-fea9-4251-9b0b-419b3c839423"),
                    FirstName = "Кирилл",
                    LastName = "Курако",
                    UserName = "WhiteFox",
                    Email = "whitefox@yandex.ru",
                    PhoneNumber = "+375447045348",
                    PromotionalPoins = 5f
                }
            );
        }
    }
}
