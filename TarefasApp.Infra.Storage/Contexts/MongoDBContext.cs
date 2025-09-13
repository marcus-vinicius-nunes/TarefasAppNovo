using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Infra.Storage.Collections;
using TarefasApp.Infra.Storage.Settings;
namespace TarefasApp.Infra.Storage.Contexts
{
    /// <summary>
    /// Classe de contexto para acesso ao MongoDB
    /// </summary>
    public class MongoDBContext
    {
        private readonly MongoDBSettings _mongoDBSettings;
        public MongoDBContext(MongoDBSettings mongoDBSettings)
        {
            _mongoDBSettings = mongoDBSettings;
            Configure();
        }
        private IMongoDatabase? _mongoDatabase;
        private void Configure()
        {
            //configurando o endereço do servidor do BD (connectionstring)
            var mongoClientSettings = MongoClientSettings.FromUrl
           (new MongoUrl(_mongoDBSettings?.Host));
            //verificando se a conexão é do tipo SSL
            if (_mongoDBSettings.IsSSL)
                mongoClientSettings.SslSettings = new SslSettings
                {
                    EnabledSslProtocols = System.Security
               .Authentication.SslProtocols.Tls12
                };
            //conectando com o banco de dados
            var mongoClient = new MongoClient(mongoClientSettings);
            _mongoDatabase = mongoClient
           .GetDatabase(_mongoDBSettings.Database);
        }
        //Mapeamento das collections do banco
        public IMongoCollection<TarefaCollection> Tarefa
        => _mongoDatabase.GetCollection<TarefaCollection>("Tarefa");
    }
}
