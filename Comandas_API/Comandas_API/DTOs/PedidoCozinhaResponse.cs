namespace Comandas_API.DTOs
{
    public class PedidoCozinhaResponse
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public List<PedidoCozinhaItemResponse> Itens { get; set; } = [];
    }
    public class PedidoCozinhaItemResponse
    {
        public int ComandaItemId { get; set; }
    }
}
