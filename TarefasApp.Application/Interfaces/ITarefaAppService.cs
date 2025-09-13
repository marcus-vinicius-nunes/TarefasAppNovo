using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Application.Commands;
using TarefasApp.Application.Commands.TarefasApp.Application.Commands;
using TarefasApp.Application.Dtos;
namespace TarefasApp.Application.Interfaces
{
    /// <summary>
    /// Contrato dos métodos de serviço da aplicação
    /// </summary>
    public interface ITarefaAppService
    {
        Task<TarefaDto> Create(TarefaCreateCommand command);
        Task<TarefaDto> Update(TarefaUpdateCommand command);
        Task<TarefaDto> Delete(TarefaDeleteCommand command);

        List<TarefaDto>? GetAll();
        TarefaDto? GetById(Guid id);
    }
}
