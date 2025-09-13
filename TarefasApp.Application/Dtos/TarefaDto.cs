using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TarefasApp.Application.Dtos
{
    /// <summary>
    /// Modelo de dados DTO para Tarefa
    /// </summary>
    public class TarefaDto
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataHora { get; set; }
        public string? Descricao { get; set; }
        public Prioridade? Prioridade { get; set; }
    }
    /// <summary>
    /// Enumerador para prioridade da tarefa
    /// </summary>
    public enum Prioridade
    {
        Baixa = 1,
        Media = 2,
        Alta = 3
    }
}
