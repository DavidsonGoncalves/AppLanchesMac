using AppLanchesMac.Context;
using AppLanchesMac.Models;
using AppLanchesMac.Repositories.Interfaces;

namespace AppLanchesMac.Repositories
{
    public class PedidoRepository : IpedidoRepository
    {
        private readonly AppDbContext _appDbcontext;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext appDbcontext, CarrinhoCompra carrinhoCompra)
        {
            _appDbcontext = appDbcontext;
            _carrinhoCompra = carrinhoCompra;
        }



        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado=DateTime.Now;
            _appDbcontext.Pedidos.Add(pedido);
            _appDbcontext.SaveChanges();

            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItems;

            foreach(var carrinhoitem in carrinhoCompraItens)
            {
                var pedidoDetail = new PedidoDetalhe()
                {
                    Quantidade = carrinhoitem.Quantidade,
                    lancheId = carrinhoitem.Lanche.LancheId,
                    PedidoID = pedido.PedidoId,
                    Preco = carrinhoitem.Lanche.Preco
                };
                _appDbcontext.PedidosDetalhe.Add(pedidoDetail);
            }
            _appDbcontext.SaveChanges ();
        }
    }
}
