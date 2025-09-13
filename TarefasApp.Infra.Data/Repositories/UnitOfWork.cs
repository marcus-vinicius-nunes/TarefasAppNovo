using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Infra.Data.Contexts;
namespace TarefasApp.Infra.Data.Repositories
{
    /// <summary>
    /// Implementação da unidade de trabalho dos repositórios
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext? _dataContext;
        public UnitOfWork(DataContext? dataContext)
        => _dataContext = dataContext;
        public ITarefaRepository? TarefaRepository
       => new TarefaRepository(_dataContext);
        public async Task SaveChanges()
        {
            await _dataContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}