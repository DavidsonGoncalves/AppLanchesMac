using AppLanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace AppLanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }

        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //define a session
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //get a service of our context type
            var context = services.GetService<AppDbContext>();

            //get or generate carrinho id
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //define the string for the session
            session.SetString("CarrinhoId", carrinhoId);

            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarAocarrinho (Lanche lanche)
        {
            var carrinhoCompraItem = _context.carrinhoCompraItens.SingleOrDefault(s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId==CarrinhoCompraId);

            if(carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1

                };
                _context.carrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public void RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.carrinhoCompraItens.SingleOrDefault(s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);


            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                }
                else
                {
                    _context.carrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();

        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItems ?? (CarrinhoCompraItems =
                _context.carrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Include(s => s.Lanche)
                .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoitens = _context.carrinhoCompraItens
                .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _context.carrinhoCompraItens.RemoveRange(carrinhoitens);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            return _context.carrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c=> c.Lanche.Preco * c.Quantidade).Sum();
        }
        
    }
}
