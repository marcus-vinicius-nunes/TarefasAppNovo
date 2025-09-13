using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Application.Dtos;
namespace TarefasApp.Application.Data
{
    /// <summary>
    /// Classe para armazenamento dos dados em memória
    /// </summary>
    public class FakeDataStore
    {
        //atributo
        private static List<TarefaDto>? _tarefas;
        //construtor para inicializar o atributo
        public FakeDataStore() => _tarefas = new List<TarefaDto>();
        //Adicionar tarefa
        public void Add(TarefaDto dto)
        {
            _tarefas?.Add(dto);
        }
        //Atualizar tarefa
        public void Update(TarefaDto dto)
        {
            var tarefa = _tarefas?.FirstOrDefault(t => t.Id == dto.Id);
            if (tarefa != null)
            {
                tarefa.Nome = dto.Nome;
                tarefa.DataHora = dto.DataHora;
                tarefa.Descricao = dto.Descricao;
                tarefa.Prioridade = dto.Prioridade;
            }
        }
        //Excluir tarefa
        public void Delete(Guid? id)
        {
            var tarefa = _tarefas?.FirstOrDefault(t => t.Id == id);
            if (tarefa != null)
                _tarefas?.Remove(tarefa);
        }
        //Consultar todas as tarefas
        public List<TarefaDto>? GetAll()
        => _tarefas;
        //Consultar 1 tarefa através do id
        public TarefaDto? GetById(Guid? id)
        => _tarefas?.FirstOrDefault(t => t.Id == id);
    }
}