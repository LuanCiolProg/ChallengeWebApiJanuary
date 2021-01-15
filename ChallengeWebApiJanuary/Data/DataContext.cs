using Microsoft.EntityFrameworkCore;
using ChallengeWebApiJanuary.Models;

namespace ChallengeWebApiJanuary.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
            {
            }

            public DbSet<Prestacao> Prestacoes { get; set; }
            public DbSet<Contrato> Contratos { get; set; }
    }
}