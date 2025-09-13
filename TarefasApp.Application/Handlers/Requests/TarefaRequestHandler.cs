using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Application.Commands;
using TarefasApp.Application.Commands.TarefasApp.Application.Commands;
using TarefasApp.Application.Dtos;
using TarefasApp.Application.Handlers.Notifications;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces;
using TarefasApp.Domain.Services;
namespace TarefasApp.Application.Handlers.Requests
{
    /// <summary>
    /// Classe para receber as requisições  COMMANDS(CREATE, UPDATE e DELETE)
    /// </summary>
    public class TarefaRequestHandler :
    IRequestHandler<TarefaCreateCommand, TarefaDto>,
    IRequestHandler<TarefaUpdateCommand, TarefaDto>,
    IRequestHandler<TarefaDeleteCommand, TarefaDto>
    {
        //atributo
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ITarefaDomainService _tarefaDomainService;
        //construtor para injeção de dependência
        public TarefaRequestHandler(IMediator mediator, IMapper mapper,
       ITarefaDomainService tarefaDomainService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _tarefaDomainService = tarefaDomainService;
        }
        public async Task<TarefaDto> Handle(TarefaCreateCommand request,
       CancellationToken cancellationToken)
        {
            //Gravar os dados no domínio
            var tarefa = _mapper.Map<Tarefa>(request);
            await _tarefaDomainService.Add(tarefa);
            //Gerar uma notificação para que os dados
            //sejam replicados em um banco de consulta
            var tarefaDto = _mapper.Map<TarefaDto>(tarefa);
            var tarefaNotification = new TarefaNotification
            {
                Tarefa = tarefaDto,
                Action = TarefaNotificationAction.TarefaCriada
            };
            await _mediator.Publish(tarefaNotification);
            return tarefaDto;
        }
        public async Task<TarefaDto> Handle(TarefaUpdateCommand request,
       CancellationToken cancellationToken)
        {
            //Atualizar os dados no domínio
            var tarefa = _mapper.Map<Tarefa>(request);
            await _tarefaDomainService.Update(tarefa);
            //Gerar uma notificação para que os dados
            //sejam replicados em um banco de consulta
            var tarefaDto = _mapper.Map<TarefaDto>(tarefa);
            var tarefaNotification = new TarefaNotification
            {
                Tarefa = tarefaDto,
                Action = TarefaNotificationAction.TarefaAlterada
            };
            await _mediator.Publish(tarefaNotification);
            return tarefaDto;
        }
        public async Task<TarefaDto> Handle(TarefaDeleteCommand request,
       CancellationToken cancellationToken)
        {
            //Excluir os dados no domínio
            var tarefa = await _tarefaDomainService
           .GetById(request.Id.Value);
            await _tarefaDomainService.Delete(tarefa);
            //Gerar uma notificação para que os dados
            //sejam replicados em um banco de consulta
            var tarefaDto = _mapper.Map<TarefaDto>(tarefa);
            var tarefaNotification = new TarefaNotification
            {
                Tarefa = tarefaDto,
                Action = TarefaNotificationAction.TarefaExcluida
            };
            await _mediator.Publish(tarefaNotification);
            return tarefaDto;
        }
    }
}
