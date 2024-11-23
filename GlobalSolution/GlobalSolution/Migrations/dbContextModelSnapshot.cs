﻿// <auto-generated />
using System;
using GlobalSolution.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace GlobalSolution.Migrations
{
    [DbContext(typeof(dbContext))]
    partial class dbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GlobalSolution.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR2(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR2(255)");

                    b.Property<string>("PeriodoDeUso")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR2(255)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("GlobalSolution.Models.EnergyConsumption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Custo")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<float>("EnergiaTotal")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<int?>("RelatorioId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("RelatorioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("EnergyConsumptions");
                });

            modelBuilder.Entity("GlobalSolution.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("CustoTotal")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR2(255)");

                    b.Property<float>("EnergiaTotal")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<DateTime>("PeriodoFim")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime>("PeriodoInicio")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("GlobalSolution.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR2(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR2(255)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR2(255)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GlobalSolution.Models.UserSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AlertaEmail")
                        .HasColumnType("BOOLEAN");

                    b.Property<float>("LimiteEnergia")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("GlobalSolution.Models.Device", b =>
                {
                    b.HasOne("GlobalSolution.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GlobalSolution.Models.EnergyConsumption", b =>
                {
                    b.HasOne("GlobalSolution.Models.Report", "Report")
                        .WithMany()
                        .HasForeignKey("RelatorioId");

                    b.HasOne("GlobalSolution.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GlobalSolution.Models.Report", b =>
                {
                    b.HasOne("GlobalSolution.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GlobalSolution.Models.UserSetting", b =>
                {
                    b.HasOne("GlobalSolution.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
