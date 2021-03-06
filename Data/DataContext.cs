using Microsoft.EntityFrameworkCore;
using appPerfinAPI.Models;
using System.Collections.Generic;

namespace appPerfinAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Categoria> Categorias { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Categoria>()
                .HasData(new List<Categoria>(){
                    new Categoria(1, "Alimentação", "ALI"),
                    new Categoria(2, "Bar / Festas", "BAR"),
                    new Categoria(3, "Refeição", "REF"),
                    new Categoria(4, "Lazer / Esporte", "LAZ"),
                });
        }
    }
}