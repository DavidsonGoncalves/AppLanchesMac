using AppLanchesMac.Models;
using AppLanchesMac.Repositories.Interfaces;
using AppLanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppLanchesMac.Controllers
{
    public class CarrinhoCompraController : Controller
    {

        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItems = itens;
            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };
            return View(carrinhoCompraVM);
        }

        public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheid)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p=> p.LancheId == lancheid);

            if(lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarAocarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemmoverItemDoCarrinhoCompra(int lancheid)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.LancheId == lancheid);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }
    }
}
