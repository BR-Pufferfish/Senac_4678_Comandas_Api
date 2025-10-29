namespace Comandas_API.Models
{
    public class PedidoCozinhaItem
    {
        public int Id { get; set; }
        public int PedidoCozinhaId { get; set; }
        public virtual PedidoCozinha PedidoCozinha { get; set; }
        public int ComandaItemId { get; set; }
        public virtual ComandaItem ComandaItem { get; set; }
    }
}