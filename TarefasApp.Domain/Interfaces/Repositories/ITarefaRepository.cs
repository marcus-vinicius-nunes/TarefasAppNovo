using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Repositories;

namespace TarefasApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface para operações de repositório de tarefas
    /// </summary>
    public interface ITarefaRepository : IBaseRepository<Tarefa, Guid>
    {

    }
}
