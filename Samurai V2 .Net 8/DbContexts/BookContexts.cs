using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Samurai_V2_.Net_8.DbContexts.Models;

namespace Samurai_V2_.Net_8.DbContexts;

public partial class BookContexts : DbContext
{
    public BookContexts()
    {
    }

    public BookContexts(DbContextOptions<BookContexts> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBook> TblBooks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source= 192.168.61.49,1433;Initial Catalog=BoookManagement;User Id=softadmin; password=w23eW@#E;Connection Timeout=30;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBook>(entity =>
        {
            entity.HasKey(e => e.BookId);

            entity.ToTable("tblBook");

            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.BookTitle).HasMaxLength(100);
            entity.Property(e => e.Genre).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnName("price");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
