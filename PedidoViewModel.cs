namespace WebCafe.Models
{
    public class PedidoViewModel
    {
        public int NumeroPedido { get; set; }
        public DateTime DataRealizacao { get; set; }
        public DateTime PrevisaoEntrega { get; set; }
        public decimal ValorTotal { get; set; }
        public List<CartItem> Itens { get; set; }
    }

}
