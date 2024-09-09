using Microsoft.EntityFrameworkCore;
using ProjetoEstagio.Models;

namespace ProjetoEstagio
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Fornecedor> Fornecedores { get; set; }
    }
}
