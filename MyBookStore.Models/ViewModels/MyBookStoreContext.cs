﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MyBookStore.Models.ViewModels
{
    public partial class MyBookStoreContext : DbContext
    {
        public MyBookStoreContext()
        {
        }

        public MyBookStoreContext(DbContextOptions<MyBookStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=MyBookStore;User Id=postgres;Password=admin;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_India.1252");

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Base64image).HasColumnName("base64image");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasPrecision(10, 2)
                    .HasColumnName("price");

                entity.Property(e => e.Publisherid).HasColumnName("publisherid");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.Categoryid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_category");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.Publisherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_publisher");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bookid).HasColumnName("bookid");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.Bookid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_book");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("publisher");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .HasColumnName("address");

                entity.Property(e => e.Contact)
                    .HasMaxLength(20)
                    .HasColumnName("contact");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastname");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.Roleid).HasColumnName("roleid");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
