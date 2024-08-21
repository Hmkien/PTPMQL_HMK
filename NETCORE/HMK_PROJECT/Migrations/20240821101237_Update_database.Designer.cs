﻿// <auto-generated />
using HMK_PROJECT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HMK_PROJECT.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240821101237_Update_database")]
    partial class Update_database
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("HMK_PROJECT.Models.DaiLy", b =>
                {
                    b.Property<string>("MaDaiLy")
                        .HasColumnType("TEXT");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DienThoai")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MaHTPP")
                        .HasColumnType("TEXT");

                    b.Property<string>("NguoiDaiDien")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TenDaiLy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MaDaiLy");

                    b.HasIndex("MaHTPP");

                    b.ToTable("DaiLy");
                });

            modelBuilder.Entity("HMK_PROJECT.Models.HeThongPhanPhoi", b =>
                {
                    b.Property<string>("MaHTPP")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenHTPP")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MaHTPP");

                    b.ToTable("HTPP");
                });

            modelBuilder.Entity("HMK_PROJECT.Models.Person", b =>
                {
                    b.Property<string>("PersonId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PersonId");

                    b.ToTable("Person", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("HMK_PROJECT.Models.Employee", b =>
                {
                    b.HasBaseType("HMK_PROJECT.Models.Person");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("HMK_PROJECT.Models.DaiLy", b =>
                {
                    b.HasOne("HMK_PROJECT.Models.HeThongPhanPhoi", "HTPP")
                        .WithMany("DaiLy")
                        .HasForeignKey("MaHTPP")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("HTPP");
                });

            modelBuilder.Entity("HMK_PROJECT.Models.Employee", b =>
                {
                    b.HasOne("HMK_PROJECT.Models.Person", null)
                        .WithOne()
                        .HasForeignKey("HMK_PROJECT.Models.Employee", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HMK_PROJECT.Models.HeThongPhanPhoi", b =>
                {
                    b.Navigation("DaiLy");
                });
#pragma warning restore 612, 618
        }
    }
}
