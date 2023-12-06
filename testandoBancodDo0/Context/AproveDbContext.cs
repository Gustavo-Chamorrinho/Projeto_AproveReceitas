using Microsoft.EntityFrameworkCore;
using testandoBancodDo0.Models;

namespace testandoBancodDo0.Context
{
    public class AproveDbContext:DbContext
    {
        public AproveDbContext(DbContextOptions<AproveDbContext> options) : base(options) 
        {
            
        }

        public DbSet<UsuarioModel> usuarios { get; set; }
        public DbSet<ReceitaModel> receitas { get; set; } 

    }
}
