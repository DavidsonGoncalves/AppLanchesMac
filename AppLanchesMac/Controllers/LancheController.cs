using AppLanchesMac.Models;
using AppLanchesMac.Repositories;
using AppLanchesMac.Repositories.Interfaces;
using AppLanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

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

        public ViewResult Search(string searchString)
        {

            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancherepository.Lanches.OrderBy(p => p.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                lanches = _lancherepository.Lanches.Where(p=> p.Nome.ToLower().Contains(searchString.ToLower()));
                if(lanches.Any())
                {
                    categoriaAtual = "Lanches";
                }
                else
                {
                    categoriaAtual= "Nenhum Lanche foi encontrado";
                }

            }
            return View("~/Views/Lanche/List.cshtml", new LanchesListViewModel { Lanches = lanches, CategoriaAtual = categoriaAtual });

        }
    }
}
