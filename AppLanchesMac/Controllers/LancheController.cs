using AppLanchesMac.Models;
using AppLanchesMac.Repositories;
using AppLanchesMac.Repositories.Interfaces;
using AppLanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppLanchesMac.Controllers
{
    public class LancheController : Controller
    {

        private readonly ILancheRepository _lancherepository;

        public LancheController(ILancheRepository lancherepository)
        {
            _lancherepository = lancherepository;
        }

        public IActionResult List(string categoria)
        {
            //var lanche = _lancherepository.Lanches;
            //return View(lanche);
            //var lancheListViewModel = new LanchesListViewModel();
            //lancheListViewModel.Lanches = _lancherepository.Lanches;
            //lancheListViewModel.CategoriaAtual = "Categoria";

            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if(string.IsNullOrEmpty(categoria) || !_lancherepository.Lanches.Any(l => l.Categoria.CategoriaNome.Equals(categoria, StringComparison.OrdinalIgnoreCase)))
            {
                lanches = _lancherepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches = _lancherepository.Lanches.Where(l => l.Categoria.CategoriaNome.Equals(categoria, StringComparison.OrdinalIgnoreCase)).OrderBy(l => l.Nome);
            }


            var lancheListViewModel = new LanchesListViewModel()
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };
            return View(lancheListViewModel);

        }

        public IActionResult Details(int lancheId) 
        {
            var lanche= _lancherepository.Lanches.FirstOrDefault(l =>  l.LancheId == lancheId);
            return View(lanche);
        }
    }
}
