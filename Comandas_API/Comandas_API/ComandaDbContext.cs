using Microsoft.EntityFrameworkCore;

namespace Comandas_API
{
    public class ComandaDbContext : DbContext
    {
        public ComandaDbContext(DbContextOptions<ComandaDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Usuario> Usuario { get; set; } = default!;
        public DbSet<Models.Mesa> Mesa { get; set; } = default!;
        public DbSet<Models.Reserva> Reserva { get; set; } = default!;
        public DbSet<Models.Comanda> Comanda { get; set; } = default!;
        public DbSet<Models.ComandaItem> ComandaItem { get; set; } = default!;
        public DbSet<Models.PedidoCozinha> PedidoCozinha { get; set; } = default!;
        public DbSet<Models.CardapioItem> CardapioItem { get; set; } = default!;
        public DbSet<Models.PedidoCozinhaItem> PedidoCozinhaItem { get; set; } = default!;
    }
}
