using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Application.Commands;
using TarefasApp.Application.Commands.TarefasApp.Application.Commands;
using TarefasApp.Application.Data;
using TarefasApp.Application.Dtos;
using TarefasApp.Application.Interfaces;
namespace TarefasApp.Application.Services
{
    /// <summary>
    /// Implementação dos serviços de tarefa da aplicação
    /// </summary>
    public class TarefaAppService : ITarefaAppService
    {
        //atributo
        private readonly IMediator _mediator;
        private readonly FakeDataStore _fakeDataStore;
        //construtor para injeção de dependência
        public TarefaAppService
       (IMediator mediator, FakeDataStore fakeDataStore)
        {
            _mediator = mediator;
            _fakeDataStore = fakeDataStore;
        }
        public async Task<TarefaDto> Create(TarefaCreateCommand command)
        {
            return await _mediator.Send(command);
        }
        public async Task<TarefaDto> Update(TarefaUpdateCommand command)
        {
            return await _mediator.Send(command);
        }
        public async Task<TarefaDto> Delete(TarefaDeleteCommand command)
        {
            return await _mediator.Send(command);
        }
        public List<TarefaDto>? GetAll()
        {
            return _fakeDataStore.GetAll();
        }
        public TarefaDto? GetById(Guid id)
        {
            return _fakeDataStore.GetById(id);
        }
    }
}