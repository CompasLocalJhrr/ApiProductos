using ApiProductos.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProductos.Data;

public partial class CompasProductosContext : DbContext
{
    public CompasProductosContext()
    {
    }

    public CompasProductosContext(DbContextOptions<CompasProductosContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Producto> Productos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC07C85B5C21");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
