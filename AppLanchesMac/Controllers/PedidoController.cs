using AppLanchesMac.Models;
using AppLanchesMac.Repositories.Interfaces;
//using AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace AppLanchesMac.Controllers
{
    public class PedidoController : Controller
    {

        private readonly IpedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhocompra;

        public PedidoController(IpedidoRepository pedidoRepository, CarrinhoCompra carrinhocompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhocompra = carrinhocompra;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = decimal.Zero;

            List<CarrinhoCompraItem> items = _carrinhocompra.GetCarrinhoCompraItens();
            _carrinhocompra.CarrinhoCompraItems = items;

            if (_carrinhocompra.CarrinhoCompraItems.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho está vazio");
            }

            foreach (var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }

            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            if (ModelState.IsValid)
            {
                _pedidoRepository.CriarPedido(pedido);
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido";
                ViewBag.TotalPedido = _carrinhocompra.GetCarrinhoCompraTotal();

                _carrinhocompra.LimparCarrinho();

                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            return View(pedido);
        }

    }
}
