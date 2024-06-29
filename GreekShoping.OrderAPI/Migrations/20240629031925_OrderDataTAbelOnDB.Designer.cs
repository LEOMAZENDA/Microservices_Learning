﻿// <auto-generated />
using System;
using GreekShoping.OrderAPI.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GreekShoping.OrderAPI.Migrations
{
    [DbContext(typeof(MySqlContext))]
    [Migration("20240629031925_OrderDataTAbelOnDB")]
    partial class OrderDataTAbelOnDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GreekShoping.OrderAPI.Models.OrderDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<int>("Count")
                        .HasColumnType("int")
                        .HasColumnName("Count");

                    b.Property<long>("OrderHeaderId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("Price");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint")
                        .HasColumnName("ProductId");

                    b.Property<string>("ProductName")
                        .HasColumnType("longtext")
                        .HasColumnName("Product_name");

                    b.HasKey("Id");

                    b.HasIndex("OrderHeaderId");

                    b.ToTable("TbOrderDetail");
                });

            modelBuilder.Entity("GreekShoping.OrderAPI.Models.OrderHeader", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("CVV")
                        .HasColumnType("longtext")
                        .HasColumnName("CVV");

                    b.Property<string>("CardNumber")
                        .HasColumnType("longtext")
                        .HasColumnName("Card_number");

                    b.Property<int>("CartTotalItens")
                        .HasColumnType("int")
                        .HasColumnName("Total_itens");

                    b.Property<string>("CouponCode")
                        .HasColumnType("longtext")
                        .HasColumnName("coupon_code");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("Purchase_date");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("Discount_amount");

                    b.Property<string>("Email")
                        .HasColumnType("longtext")
                        .HasColumnName("Email");

                    b.Property<string>("ExpiryMonthYear")
                        .HasColumnType("longtext")
                        .HasColumnName("Expiry_month_year");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext")
                        .HasColumnName("First_name");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext")
                        .HasColumnName("Last_name");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("Order_time");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("Payment_status");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext")
                        .HasColumnName("Phone_number");

                    b.Property<decimal>("PurchaseAmount")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("Purchase_amount");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext")
                        .HasColumnName("User_id");

                    b.HasKey("Id");

                    b.ToTable("TbOrderHeader");
                });

            modelBuilder.Entity("GreekShoping.OrderAPI.Models.OrderDetail", b =>
                {
                    b.HasOne("GreekShoping.OrderAPI.Models.OrderHeader", "OrderHeader")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderHeader");
                });

            modelBuilder.Entity("GreekShoping.OrderAPI.Models.OrderHeader", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
