using AppLanchesMac.Models;

namespace AppLanchesMac.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }

    }
}
