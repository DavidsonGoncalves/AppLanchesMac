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

        public IActionResult List()
        {
            //var lanche = _lancherepository.Lanches;
            //return View(lanche);

            var lancheListViewModel = new LanchesListViewModel();
            lancheListViewModel.Lanches = _lancherepository.Lanches;
            lancheListViewModel.CategoriaAtual = "Categoria";

            return View(lancheListViewModel);

        }
    }
}
