using Microsoft.EntityFrameworkCore;

namespace training.api.Model
{
    public class TrainingContext : DbContext
    {
        public TrainingContext(DbContextOptions<TrainingContext> options) : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<ContaBancaria> ContaBancarias { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Loja> Lojas { get; set; }

        public DbSet<Banco> Bancos { get; set; }

        public DbSet<Estado> Estados { get; set; }

        public DbSet<Cidade> Cidades { get; set; }

    }
}
