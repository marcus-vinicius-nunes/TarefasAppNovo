using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface de unidade de trabalho para os repositórios
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ITarefaRepository? TarefaRepository { get; }

        Task SaveChanges();
    }

}
