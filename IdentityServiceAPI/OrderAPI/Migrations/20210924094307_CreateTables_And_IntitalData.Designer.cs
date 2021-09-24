﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace OrderAPI.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20210924094307_CreateTables_And_IntitalData")]
    partial class CreateTables_And_IntitalData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Models.Account", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<float>("PromotionalPoins")
                        .HasColumnType("real");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("UserID");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            UserID = new Guid("bca0ad85-fea9-4251-9b0b-419b3c839423"),
                            Email = "whitefox@yandex.ru",
                            FirstName = "Кирилл",
                            LastName = "Курако",
                            PhoneNumber = "+375447045348",
                            PromotionalPoins = 5f,
                            UserName = "WhiteFox"
                        });
                });

            modelBuilder.Entity("Entities.Models.DeliveryMethod", b =>
                {
                    b.Property<Guid>("DeliveryMethodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DeliveryMethodID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeliveryMethodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("DeliveryMethodID");

                    b.ToTable("DeliveryMethod");

                    b.HasData(
                        new
                        {
                            DeliveryMethodID = new Guid("f5ec8ff6-1e86-4a95-b40e-25b592743501"),
                            DeliveryMethodName = "Самовывоз"
                        },
                        new
                        {
                            DeliveryMethodID = new Guid("36f18c03-e238-4df0-bbe7-d71ba7daf1e8"),
                            DeliveryMethodName = "Доставка"
                        });
                });

            modelBuilder.Entity("Entities.Models.OrderStatus", b =>
                {
                    b.Property<Guid>("OrderStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OrderStatusID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrderStatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("OrderStatusID");

                    b.ToTable("OrderStatus");

                    b.HasData(
                        new
                        {
                            OrderStatusID = new Guid("89e487c2-6ec2-4995-8623-c6e8349abe86"),
                            OrderStatusName = "Обрабатывается"
                        },
                        new
                        {
                            OrderStatusID = new Guid("44a36979-c0c7-4c54-a8c4-76c69ea816a0"),
                            OrderStatusName = "Подтвержден"
                        },
                        new
                        {
                            OrderStatusID = new Guid("cba8e6c5-6bc4-4d0e-ab21-56ad99b2eae7"),
                            OrderStatusName = "Отменен"
                        },
                        new
                        {
                            OrderStatusID = new Guid("c14bd298-d7e3-4876-9666-81b8de932f7c"),
                            OrderStatusName = "Отправлен"
                        },
                        new
                        {
                            OrderStatusID = new Guid("d1717863-4dce-4fc2-9524-648f8e7c4c3f"),
                            OrderStatusName = "Доставлен"
                        });
                });

            modelBuilder.Entity("Entities.Models.PaymentMethod", b =>
                {
                    b.Property<Guid>("PaymentMethodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PaymentMethodID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PaymentMethodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("PaymentMethodID");

                    b.ToTable("PaymentMethod");

                    b.HasData(
                        new
                        {
                            PaymentMethodID = new Guid("3a45abc3-0a27-4d40-b618-43b800a6d8aa"),
                            PaymentMethodName = "Наличные"
                        },
                        new
                        {
                            PaymentMethodID = new Guid("d1f0556e-0691-43fa-a7ff-f2e3bd2fee17"),
                            PaymentMethodName = "Карта"
                        });
                });

            modelBuilder.Entity("Entities.Models.Product", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<Guid>("ProductTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("PromotionalPointsCost")
                        .HasColumnType("real");

                    b.HasKey("ProductID");

                    b.HasIndex("ProductTypeID");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            ProductID = new Guid("b7dccbda-1963-4b4f-b083-5a17c5e3e6bd"),
                            Cost = 19.4f,
                            Description = "Бекон, цыпленок, ветчина, сыр блю чиз, сыры чеддер и пармезан, соус песто, кубики брынзы, томаты черри, красный лук, моцарелла, соус альфредо, чеснок, итальянские травы",
                            ProductName = "Додо Микс",
                            ProductTypeID = new Guid("05e1586f-f9a2-4aa3-8d90-513655bf4a53"),
                            PromotionalPointsCost = 10f
                        },
                        new
                        {
                            ProductID = new Guid("a76313d7-f8ec-40b9-b1f7-d88cb028caed"),
                            Cost = 14.4f,
                            Description = "Ветчина, сыр чеддер, сладкий перец, моцарелла, томатный соус, чеснок, итальянские травы",
                            ProductName = "Чиззи чеддер",
                            ProductTypeID = new Guid("05e1586f-f9a2-4aa3-8d90-513655bf4a53"),
                            PromotionalPointsCost = 7f
                        },
                        new
                        {
                            ProductID = new Guid("d4d7e487-9d42-4653-bbe5-a419e9890639"),
                            Cost = 5.4f,
                            Description = "Напиток из молока и мороженого с шоколадным сиропом",
                            ProductName = "Шоколадный молочный коктейль",
                            ProductTypeID = new Guid("304d8855-f87e-4b2b-a4ad-c35e23d2b4ca"),
                            PromotionalPointsCost = 2.5f
                        },
                        new
                        {
                            ProductID = new Guid("8c72c183-ecee-4d21-a90c-618185e957e8"),
                            Cost = 5.8f,
                            Description = "Напиток из молока и мороженого с добавлением дробленого печенья «Орео»",
                            ProductName = "Молочный коктейль с печеньем Орео",
                            ProductTypeID = new Guid("304d8855-f87e-4b2b-a4ad-c35e23d2b4ca"),
                            PromotionalPointsCost = 2.8f
                        },
                        new
                        {
                            ProductID = new Guid("4400c4c1-ac58-438a-9b23-5209c7621e4b"),
                            Cost = 0.6f,
                            Description = "Барбекю соус",
                            ProductName = "Барбекю соус",
                            ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"),
                            PromotionalPointsCost = 0.6f
                        },
                        new
                        {
                            ProductID = new Guid("52de27a6-5519-4387-b7cf-887365dd0345"),
                            Cost = 0.6f,
                            Description = "Сырный соус",
                            ProductName = "Сырный соус",
                            ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"),
                            PromotionalPointsCost = 0.6f
                        },
                        new
                        {
                            ProductID = new Guid("3b710690-d1f7-459b-b854-8e85ca14750d"),
                            Cost = 0.6f,
                            Description = "Горчичный  соус",
                            ProductName = "Горчичный  соус",
                            ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"),
                            PromotionalPointsCost = 0.6f
                        },
                        new
                        {
                            ProductID = new Guid("f1739067-0e46-4fb7-939d-b4caf93aea80"),
                            Cost = 0.6f,
                            Description = "Кисло-сладкий соус",
                            ProductName = "Кисло-сладкий соус",
                            ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"),
                            PromotionalPointsCost = 0.6f
                        },
                        new
                        {
                            ProductID = new Guid("91128504-95af-45c0-b778-5652f360f7a2"),
                            Cost = 0.6f,
                            Description = "Терияки соус",
                            ProductName = "Терияки соус",
                            ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"),
                            PromotionalPointsCost = 0.6f
                        },
                        new
                        {
                            ProductID = new Guid("26c364cc-83ff-4cf1-8c8d-269f26f9318b"),
                            Cost = 0.6f,
                            Description = "Чесночный  соус",
                            ProductName = "Чесночный  соус",
                            ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"),
                            PromotionalPointsCost = 0.6f
                        });
                });

            modelBuilder.Entity("Entities.Models.ProductType", b =>
                {
                    b.Property<Guid>("ProductTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProductTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("ProductTypeID");

                    b.ToTable("ProductType");

                    b.HasData(
                        new
                        {
                            ProductTypeID = new Guid("05e1586f-f9a2-4aa3-8d90-513655bf4a53"),
                            ProductTypeName = "Пицца"
                        },
                        new
                        {
                            ProductTypeID = new Guid("304d8855-f87e-4b2b-a4ad-c35e23d2b4ca"),
                            ProductTypeName = "Напиток"
                        },
                        new
                        {
                            ProductTypeID = new Guid("cdc542f4-78c0-45fd-8b4f-cc6b97404555"),
                            ProductTypeName = "Соус"
                        });
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "b2eaba9b-0a1d-42a2-9782-a1f4661ad9e5",
                            ConcurrencyStamp = "1a1d2ef8-1ab9-486a-b712-63b00b9bbfb7",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "fbd363b6-3227-4122-9630-3d3ba4983365",
                            ConcurrencyStamp = "7b510ecf-dcf4-4fa2-825a-a58b7fc47cdf",
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Entities.Models.Product", b =>
                {
                    b.HasOne("Entities.Models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}