using AppLanchesMac.Models;
using Microsoft.EntityFrameworkCore; //to inherit dbcontext

namespace AppLanchesMac.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        { 
        }
        
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set; }



    }
}
