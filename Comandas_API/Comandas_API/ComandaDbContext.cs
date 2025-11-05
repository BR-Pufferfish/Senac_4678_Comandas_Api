using Microsoft.EntityFrameworkCore;

namespace Comandas_API
{
    public class ComandaDbContext : DbContext
    {
        public ComandaDbContext(DbContextOptions<ComandaDbContext> options) : base(options)
        {
        }

        // Definir algumas configs adicionais do banco de dados
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Usuario>()
                .HasData(
                    new Models.Usuario
                    {
                        Id = 1,
                        Nome = "Admin",
                        Email = "admin@admin.com",
                        Senha = "admin123"
                    }
                );

            modelBuilder.Entity<Models.CardapioItem>()
                .HasData(
                    new Models.CardapioItem
                    {
                        Id = 1,
                        Titulo = "Coxinha",
                        Descricao = "Salgadinho frito em formato de gota, recheado com frango desfiado e temperado.",
                        Preco = 5.00m
                    },
                    new Models.CardapioItem
                    {
                        Id = 2,
                        Titulo = "Pastel",
                        Descricao = "Massa fina e crocante, recheada com carne moída, queijo ou outros ingredientes, frita até dourar.",
                        Preco = 6.50m
                    },
                    new Models.CardapioItem
                    {
                        Id = 3,
                        Titulo = "Brigadeiro",
                        Descricao = "Doce feito com leite condensado, chocolate em pó, manteiga e granulado de chocolate.",
                        Preco = 3.00m
                    }
                );

            modelBuilder.Entity<Models.Mesa>()
                .HasData(
                    new Models.Mesa 
                    { Id = 1,
                      NumeroMesa = 10,
                      Situacao = 1
                    },
                    new Models.Mesa
                    {
                        Id = 2,
                        NumeroMesa = 11,
                        Situacao = 2
                    },
                    new Models.Mesa
                    {
                        Id = 3,
                        NumeroMesa = 12,
                        Situacao = 3
                    }
                );


            base.OnModelCreating(modelBuilder);
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
