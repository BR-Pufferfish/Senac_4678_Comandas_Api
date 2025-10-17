namespace Comandas_API.DTOs
{
    public class PedidoCozinhaItemUpdateRequest
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public List<int> ItensId { get; set; } = new List<int>();
    }
}
