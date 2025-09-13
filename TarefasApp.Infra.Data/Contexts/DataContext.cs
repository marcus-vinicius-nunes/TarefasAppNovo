using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Infra.Data.Mappings;
namespace TarefasApp.Infra.Data.Contexts
{
    /// <summary>
    /// Classe de contexto para conexão com o banco de dados através do EF
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Construtor para injeção de dependência
        /// </summary>
        public DataContext
       (DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TarefaMap());
        }
    }
}