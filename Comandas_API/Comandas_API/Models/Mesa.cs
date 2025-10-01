namespace Comandas_API.Models
{
    public class Mesa
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public int Situacao { get; set; }
    }


    public enum SituacaoMesa
    {
        Disponivel = 0,
        Ocupada = 1,
        Reservada = 2
    }
}
