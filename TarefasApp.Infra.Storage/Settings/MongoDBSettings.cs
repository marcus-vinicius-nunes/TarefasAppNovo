using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TarefasApp.Infra.Storage.Settings
{
    /// <summary>
    /// Classe para capturar as configurações de ambiente para o MongoDB
    /// </summary>
    public class MongoDBSettings
    {
        public string? Host { get; set; }
        public string? Database { get; set; }
        public bool IsSSL { get; set; }
    }
}

