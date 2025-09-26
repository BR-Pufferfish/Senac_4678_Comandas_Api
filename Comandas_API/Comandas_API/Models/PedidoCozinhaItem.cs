namespace Comandas_API.Models
{
    public class PedidoCozinhaItem
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public List<PedidoCozinhaItem> Itens { get; set; } = [];
    }
}
