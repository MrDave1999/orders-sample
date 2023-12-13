﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Document")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Document")
                        .IsUnique();

                    b.ToTable("Customer");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Document = "123456789",
                            Email = "steven_smith@hotmail.com",
                            FirstName = "Steven",
                            LastName = "Smith",
                            Phone = "3053581035"
                        },
                        new
                        {
                            Id = 2,
                            Document = "123456790",
                            Email = "dave_smith@hotmail.com",
                            FirstName = "Dave",
                            LastName = "Smith",
                            Phone = "3053581090"
                        });
                });

            modelBuilder.Entity("Domain.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("DeliveryAddress")
                        .HasColumnType("varchar(60)");

                    b.Property<DateOnly>("DeliveryDate")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("ShippedDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderStatusId");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 1,
                            Date = new DateOnly(2023, 12, 12),
                            DeliveryAddress = "Calle 38A #80-72, Medellin, Colombia",
                            DeliveryDate = new DateOnly(2023, 12, 20),
                            Description = "",
                            OrderStatusId = 1,
                            ShippedDate = new DateOnly(2023, 12, 15)
                        });
                });

            modelBuilder.Entity("Domain.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<string>("Product")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetail");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 2,
                            OrderId = 1,
                            Price = 187935.0,
                            Product = "Clean Code"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 2,
                            OrderId = 1,
                            Price = 2580.0,
                            Product = "A las Puertas del Abismo"
                        },
                        new
                        {
                            Id = 3,
                            Amount = 3,
                            OrderId = 1,
                            Price = 10580.0,
                            Product = "Los Besos Robados de Bridget"
                        });
                });

            modelBuilder.Entity("Domain.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("OrderStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pendiente"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Confirmado"
                        },
                        new
                        {
                            Id = 3,
                            Name = "En curso"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Entregado"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Cancelado"
                        });
                });

            modelBuilder.Entity("Domain.Order", b =>
                {
                    b.HasOne("Domain.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("Domain.OrderDetail", b =>
                {
                    b.HasOne("Domain.Order", "Order")
                        .WithMany("Details")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Order", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
