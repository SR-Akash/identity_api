using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Identity_API.Models;

#nullable disable

namespace Identity_API.DbContexts
{
    public partial class DBContextCom : DbContext
    {
        public DBContextCom()
        {
        }

        public DBContextCom(DbContextOptions<DBContextCom> options)
            : base(options)
        {
        }

        public virtual DbSet<TblUser> TblUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=imakash.database.windows.net;Initial Catalog=CMS;User ID=imakash;Password=Akash25@122540;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.IntUserId);

                entity.ToTable("tblUser", "idn");

                entity.Property(e => e.IntUserId).HasColumnName("intUserId");

                entity.Property(e => e.DteInsertTime)
                    .HasColumnType("datetime")
                    .HasColumnName("dteInsertTime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsMasterUser).HasColumnName("isMasterUser");

                entity.Property(e => e.StrEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("strEmail");

                entity.Property(e => e.StrPassword)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("strPassword");

                entity.Property(e => e.StrPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("strPhone");

                entity.Property(e => e.StrUserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("strUserName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
