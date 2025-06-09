using Microsoft.EntityFrameworkCore;

namespace API.Modelos
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Livro> Livros => Set<Livro>();
        public DbSet<Categoria> Categorias => Set<Categoria>();
    }
}
