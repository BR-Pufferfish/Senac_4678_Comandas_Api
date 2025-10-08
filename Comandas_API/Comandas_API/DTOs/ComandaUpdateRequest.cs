namespace Comandas_API.DTOs
{
    public class ComandaUpdateRequest
    {
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public int[] CardapioItemIds { get; set; } = default!;
    }
}
