using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Infra.Messages.Models;
using TarefasApp.Infra.Messages.Settings;
namespace TarefasApp.Infra.Messages.Producers
{
    public class MessageProducer
    {
        private readonly RabbitMQSettings _rabbitMQSettings;
        public MessageProducer(RabbitMQSettings rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings;
        }
        /// <summary>
        /// Método para escrever uma mensagem na fila
        /// </summary>
        public void SendMessage(EmailMessageModel emailMessageModel)
        {
            //capturando a URL do servidor da mensageria
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(_rabbitMQSettings.Url)
            };
            //abrindo conexão com o servidor
            using (var connection = connectionFactory.CreateConnection())
            {
                //conectando na fila
                using (var model = connection.CreateModel())
                {
                    //criando / conectando na fila do rabbitmq
                    model.QueueDeclare(
                     queue: _rabbitMQSettings.Queue, //nome da fila
                    durable: true,
                     //manter a fila mesmo que a instância seja parado
                     autoDelete: false,
                     //não excluir mensagens da fila automaticamente
                     exclusive: false,
                     //permitir a conexão de outras aplicações
                     arguments: null
                    );
                    //escrever a mensagem na fila
                    model.BasicPublish(
                     exchange: string.Empty,
                    routingKey: _rabbitMQSettings.Queue,
                    basicProperties: null,
                    body: Encoding.UTF8.GetBytes
                    (JsonConvert.SerializeObject(emailMessageModel))
                     );
                }
            }
        }
    }
}