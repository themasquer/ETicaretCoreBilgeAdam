﻿// <auto-generated />
using System;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ETicaretContext))]
    [Migration("20220419155300_v1")]
    partial class v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataAccess.Entities.Kategori", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Aciklamasi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ETicaretKategoriler", (string)null);
                });

            modelBuilder.Entity("DataAccess.Entities.Kullanici", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AktifMi")
                        .HasColumnType("bit");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KullaniciAdi")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("KullaniciAdi")
                        .IsUnique();

                    b.HasIndex("RolId");

                    b.ToTable("ETicaretKullanicilar", (string)null);
                });

            modelBuilder.Entity("DataAccess.Entities.KullaniciDetayi", b =>
                {
                    b.Property<int>("KullaniciId")
                        .HasColumnType("int");

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Cinsiyet")
                        .HasColumnType("int");

                    b.Property<string>("Eposta")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("SehirId")
                        .HasColumnType("int");

                    b.Property<int>("UlkeId")
                        .HasColumnType("int");

                    b.HasKey("KullaniciId");

                    b.HasIndex("Eposta")
                        .IsUnique();

                    b.HasIndex("SehirId");

                    b.HasIndex("UlkeId");

                    b.ToTable("ETicaretKullaniciDetaylari", (string)null);
                });

            modelBuilder.Entity("DataAccess.Entities.Magaza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Imaj")
                        .HasColumnType("image");

                    b.Property<string>("ImajDosyaUzantisi")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<bool>("SanalMi")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("ETicaretMagazalar", (string)null);
                });

            modelBuilder.Entity("DataAccess.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ETicaretRoller", (string)null);
                });

            modelBuilder.Entity("DataAccess.Entities.Sehir", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UlkeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UlkeId");

                    b.ToTable("ETicaretSehirler", (string)null);
                });

            modelBuilder.Entity("DataAccess.Entities.Siparis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Durum")
                        .HasColumnType("int");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KullaniciId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("KullaniciId");

                    b.ToTable("ETicaretSiparisler", (string)null);
                });

            modelBuilder.Entity("DataAccess.Entities.Ulke", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ETicaretUlkeler", (string)null);
                });

            modelBuilder.Entity("DataAccess.Entities.Urun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Aciklamasi")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<double>("BirimFiyati")
                        .HasColumnType("float");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImajDosyaUzantisi")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SonKullanmaTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("StokMiktari")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Adi");

                    b.HasIndex("KategoriId");

                    b.ToTable("ETicaretUrunler", (string)null);
                });

            modelBuilder.Entity("DataAccess.Entities.UrunMagaza", b =>
                {
                    b.Property<int>("UrunId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("MagazaId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("UrunId", "MagazaId");

                    b.HasIndex("MagazaId");

                    b.ToTable("ETicaretUrunMagazalar", (string)null);
                });

            modelBuilder.Entity("DataAccess.Entities.UrunSiparis", b =>
                {
                    b.Property<int>("UrunId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("SiparisId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("UrunAdedi")
                        .HasColumnType("int");

                    b.HasKey("UrunId", "SiparisId");

                    b.HasIndex("SiparisId");

                    b.ToTable("ETicaretUrunSiparisler", (string)null);
                });

            modelBuilder.Entity("DataAccess.Entities.Kullanici", b =>
                {
                    b.HasOne("DataAccess.Entities.Rol", "Rol")
                        .WithMany("Kullanicilar")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("DataAccess.Entities.KullaniciDetayi", b =>
                {
                    b.HasOne("DataAccess.Entities.Kullanici", "Kullanici")
                        .WithOne("KullaniciDetayi")
                        .HasForeignKey("DataAccess.Entities.KullaniciDetayi", "KullaniciId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.Sehir", "Sehir")
                        .WithMany("KullaniciDetaylari")
                        .HasForeignKey("SehirId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.Ulke", "Ulke")
                        .WithMany("KullaniciDetaylari")
                        .HasForeignKey("UlkeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Kullanici");

                    b.Navigation("Sehir");

                    b.Navigation("Ulke");
                });

            modelBuilder.Entity("DataAccess.Entities.Sehir", b =>
                {
                    b.HasOne("DataAccess.Entities.Ulke", "Ulke")
                        .WithMany("Sehirler")
                        .HasForeignKey("UlkeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ulke");
                });

            modelBuilder.Entity("DataAccess.Entities.Siparis", b =>
                {
                    b.HasOne("DataAccess.Entities.Kullanici", "Kullanici")
                        .WithMany("Siparisler")
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("DataAccess.Entities.Urun", b =>
                {
                    b.HasOne("DataAccess.Entities.Kategori", "Kategori")
                        .WithMany("Urunler")
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Kategori");
                });

            modelBuilder.Entity("DataAccess.Entities.UrunMagaza", b =>
                {
                    b.HasOne("DataAccess.Entities.Magaza", "Magaza")
                        .WithMany("UrunMagazalar")
                        .HasForeignKey("MagazaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.Urun", "Urun")
                        .WithMany("UrunMagazalar")
                        .HasForeignKey("UrunId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Magaza");

                    b.Navigation("Urun");
                });

            modelBuilder.Entity("DataAccess.Entities.UrunSiparis", b =>
                {
                    b.HasOne("DataAccess.Entities.Siparis", "Siparis")
                        .WithMany("UrunSiparisler")
                        .HasForeignKey("SiparisId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.Urun", "Urun")
                        .WithMany("UrunSiparisler")
                        .HasForeignKey("UrunId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Siparis");

                    b.Navigation("Urun");
                });

            modelBuilder.Entity("DataAccess.Entities.Kategori", b =>
                {
                    b.Navigation("Urunler");
                });

            modelBuilder.Entity("DataAccess.Entities.Kullanici", b =>
                {
                    b.Navigation("KullaniciDetayi");

                    b.Navigation("Siparisler");
                });

            modelBuilder.Entity("DataAccess.Entities.Magaza", b =>
                {
                    b.Navigation("UrunMagazalar");
                });

            modelBuilder.Entity("DataAccess.Entities.Rol", b =>
                {
                    b.Navigation("Kullanicilar");
                });

            modelBuilder.Entity("DataAccess.Entities.Sehir", b =>
                {
                    b.Navigation("KullaniciDetaylari");
                });

            modelBuilder.Entity("DataAccess.Entities.Siparis", b =>
                {
                    b.Navigation("UrunSiparisler");
                });

            modelBuilder.Entity("DataAccess.Entities.Ulke", b =>
                {
                    b.Navigation("KullaniciDetaylari");

                    b.Navigation("Sehirler");
                });

            modelBuilder.Entity("DataAccess.Entities.Urun", b =>
                {
                    b.Navigation("UrunMagazalar");

                    b.Navigation("UrunSiparisler");
                });
#pragma warning restore 612, 618
        }
    }
}