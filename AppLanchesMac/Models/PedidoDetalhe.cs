﻿namespace AppLanchesMac.Models
{
    public class PedidoDetalhe
    {
        public int PedidoDetalheId { get; set; }
        public int PedidoID { get; set; }
        public int lancheId { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public virtual Lanche Lanche { get; set; }
        public virtual Pedido Pedido { get; set; }

    }
}
