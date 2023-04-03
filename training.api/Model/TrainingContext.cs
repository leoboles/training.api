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
    }
}
