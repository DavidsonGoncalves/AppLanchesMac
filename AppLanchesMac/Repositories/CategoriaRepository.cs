using AppLanchesMac.Context;
using AppLanchesMac.Models;
using AppLanchesMac.Repositories.Interfaces;

namespace AppLanchesMac.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;


    }
}
