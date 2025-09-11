using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cust_middleware.Models;

public partial class LiteContext : DbContext
{
    public LiteContext()
    {
    }

    public LiteContext(DbContextOptions<LiteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RequestLog> RequestLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DOTNETSERVER;Initial Catalog=LITE;User ID=Fusion;Password=ispl334643mle;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RequestLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RequestL__3214EC0793AD42ED");

            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Method)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Path)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.QueryString)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Timestamp).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
