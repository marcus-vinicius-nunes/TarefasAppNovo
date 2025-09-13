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
namespace TarefasApp.Application.Handlers.Requests
{
    /// <summary>
    /// Classe para receber as requisições    COMMANDS(CREATE, UPDATE e DELETE)
    /// </summary>
    public class TarefaRequestHandler :
    IRequestHandler<TarefaCreateCommand, TarefaDto>,
    IRequestHandler<TarefaUpdateCommand, TarefaDto>,
    IRequestHandler<TarefaDeleteCommand, TarefaDto>
    {
        //atributo
        private readonly IMediator _mediator;
        //construtor para injeção de dependência
        public TarefaRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<TarefaDto> Handle(TarefaCreateCommand request,
       CancellationToken cancellationToken)
        {
            //capturar os dados para gravar a tarefa
            var tarefa = new TarefaDto
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                DataHora = DateTime.Parse($"{request.Data} {request.Hora}"),
                Descricao = request.Descricao,
                Prioridade = (Prioridade)Enum.Parse
           (typeof(Prioridade), request.Prioridade.ToString())
            };
            //TODO: GRAVAR OS DADOS NO BANCO SQL
            //Gerar uma notificação para que os dados
            //sejam replicados em um banco de consulta
            var tarefaNotification = new TarefaNotification
            {
                Tarefa = tarefa,
                Action = TarefaNotificationAction.TarefaCriada
            };
            await _mediator.Publish(tarefaNotification);
            return tarefa;
        }
        public async Task<TarefaDto> Handle
       (TarefaUpdateCommand request, CancellationToken
       cancellationToken)
        {
            //capturar os dados para gravar a tarefa
            var tarefa = new TarefaDto
            {
                Id = request.Id,
                Nome = request.Nome,
                DataHora = DateTime.Parse($"{request.Data} {request.Hora}"),
                Descricao = request.Descricao,
                Prioridade = (Prioridade)Enum.Parse
           (typeof(Prioridade), request.Prioridade.ToString())
            };
            //TODO: ATUALIZAR OS DADOS NO BANCO SQL
            //Gerar uma notificação para que os dados
            //sejam replicados em um banco de consulta
            var tarefaNotification = new TarefaNotification
            {
                Tarefa = tarefa,
                Action = TarefaNotificationAction.TarefaAlterada
            };
            await _mediator.Publish(tarefaNotification);
            return tarefa;
        }
        public async Task<TarefaDto> Handle(TarefaDeleteCommand request,
       CancellationToken cancellationToken)
        {
            //capturar os dados para excluir a tarefa
            var tarefa = new TarefaDto
            {
                Id = Guid.NewGuid()
            };
            //TODO: EXCLUIR OS DADOS NO BANCO SQL
            //Gerar uma notificação para que os dados
            //sejam replicados em um banco de consulta
            var tarefaNotification = new TarefaNotification
            {
                Tarefa = tarefa,
                Action = TarefaNotificationAction.TarefaExcluida
            };
            await _mediator.Publish(tarefaNotification);
            return tarefa;
        }
    }
}
