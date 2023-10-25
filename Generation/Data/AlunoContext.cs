using Generation.Models;
using Microsoft.EntityFrameworkCore;

namespace Generation.Data
{
    public class AlunoContext : DbContext 
    {
        public AlunoContext(DbContextOptions<AlunoContext> opts) 
            : base(opts) 
        { 
        
        }

        public DbSet<Aluno> Alunos { get; set; }
    }
}
