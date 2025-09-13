using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Infra.Data.Contexts;

namespace TarefasApp.Infra.Data.Repositories
{
    /// <summary>
    /// Implementação do repositório de tarefas
    /// </summary>
    public class TarefaRepository : BaseRepository<Tarefa, Guid>, ITarefaRepository
    {
        private readonly DataContext? _dataContext;

        public TarefaRepository(DataContext? dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}

