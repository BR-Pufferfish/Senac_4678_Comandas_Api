namespace Comandas_API.DTOs
{
    public class PedidoCozinhaCreateRequest
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public List<PedidoCozinhaItemCreateRequest> Itens { get; set; } = [];
    }


    public class PedidoCozinhaItemCreateRequest
    {
        public int Id { get; set; }
        public int PedidoCozinhaId { get; set; }
        public int ComandaItemId { get; set; }
    }
}
