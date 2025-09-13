using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Application.Data;
namespace TarefasApp.Application.Handlers.Notifications
{
    /// <summary>
    /// Classe para escutar as notificações de tarefas
    /// </summary>
    public class TarefaNotificationHandler : INotificationHandler<TarefaNotification>
    {
        //atributo
        private readonly FakeDataStore _fakeDataStore;
        public TarefaNotificationHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }
        public async Task Handle(TarefaNotification notification,
       CancellationToken cancellationToken)
        {
            switch (notification.Action)
            {
                case TarefaNotificationAction.TarefaCriada:
                    _fakeDataStore.Add(notification.Tarefa);
                    break;
                case TarefaNotificationAction.TarefaAlterada:
                    _fakeDataStore.Update(notification.Tarefa);
                    break;
                case TarefaNotificationAction.TarefaExcluida:
                    _fakeDataStore.Delete(notification.Tarefa.Id);
                    break;

            }
            await Task.CompletedTask;
        }
    }
}
