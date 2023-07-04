﻿// <auto-generated />
using System;
using AidKit.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AidKit.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230704183314_FixPathImageType")]
    partial class FixPathImageType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AidKit.DAL.Entities.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<bool>("Available")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("Expired")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PainKindId")
                        .HasColumnType("integer");

                    b.Property<string>("PathImage")
                        .HasColumnType("text");

                    b.Property<int>("TypeMedicineId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PainKindId");

                    b.HasIndex("TypeMedicineId");

                    b.HasIndex("UserId");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("AidKit.DAL.Entities.PainKind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("PainKinds");
                });

            modelBuilder.Entity("AidKit.DAL.Entities.TypeMedicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("TypeMedicines");
                });

            modelBuilder.Entity("AidKit.DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfileImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("UserRoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTimeOffset(new DateTime(2023, 7, 4, 18, 33, 13, 967, DateTimeKind.Unspecified).AddTicks(974), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "Admin@mail.ru",
                            FullName = "Администратор",
                            Login = "Admin",
                            Password = "$2a$11$mKbagrewLnsJi8SIVwKjje3Rzi/C8.VRkdlytUnSnFap4RRbNdMOq",
                            ProfileImage = "",
                            Status = 1,
                            UserRoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTimeOffset(new DateTime(2023, 7, 4, 18, 33, 14, 200, DateTimeKind.Unspecified).AddTicks(7170), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "User@mail.ru",
                            FullName = "Пользователь",
                            Login = "User",
                            Password = "$2a$11$LUr0EG1hluUsp5IuC9bA9uv8ZHM1Vu5OcPYOhg1S3uMsY0Gg/WEsS",
                            ProfileImage = "",
                            Status = 1,
                            UserRoleId = 2
                        });
                });

            modelBuilder.Entity("AidKit.DAL.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "User"
                        });
                });

            modelBuilder.Entity("AidKit.DAL.Entities.Medicine", b =>
                {
                    b.HasOne("AidKit.DAL.Entities.PainKind", "PainKind")
                        .WithMany("Medicines")
                        .HasForeignKey("PainKindId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AidKit.DAL.Entities.TypeMedicine", "TypeMedicine")
                        .WithMany("Medicines")
                        .HasForeignKey("TypeMedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AidKit.DAL.Entities.User", "User")
                        .WithMany("Medicines")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PainKind");

                    b.Navigation("TypeMedicine");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AidKit.DAL.Entities.User", b =>
                {
                    b.HasOne("AidKit.DAL.Entities.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("AidKit.DAL.Entities.PainKind", b =>
                {
                    b.Navigation("Medicines");
                });

            modelBuilder.Entity("AidKit.DAL.Entities.TypeMedicine", b =>
                {
                    b.Navigation("Medicines");
                });

            modelBuilder.Entity("AidKit.DAL.Entities.User", b =>
                {
                    b.Navigation("Medicines");
                });

            modelBuilder.Entity("AidKit.DAL.Entities.UserRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}