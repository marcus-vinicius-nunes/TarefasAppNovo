using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Application.Commands;
using TarefasApp.Application.Commands.TarefasApp.Application.Commands;
using TarefasApp.Application.Dtos;
using TarefasApp.Domain.Entities;
using TarefasApp.Infra.Storage.Collections;
namespace TarefasApp.Application.Mappings
{
    /// <summary>
    /// Classe para mapeamento de/para do automapper
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //TarefaCreateCommand > Tarefa
            CreateMap<TarefaCreateCommand, Tarefa>()
            .AfterMap((src, dest) =>
            {
                dest.Id = Guid.NewGuid();
                dest.DataHora = DateTime.Parse($"{src.Data} {src.Hora}");
            });
            //TarefaUpdateCommand > Tarefa
            CreateMap<TarefaUpdateCommand, Tarefa>()
            .AfterMap((src, dest) =>
            {
                dest.DataHora = DateTime.Parse($"{src.Data} {src.Hora}");
            });
            //Tarefa > TarefaCollection
            CreateMap<Tarefa, TarefaCollection>()
            .AfterMap((src, dest) =>
            {
                dest.DataHoraCadastro = DateTime.Now;
            });
            //Tarefa > TarefaDto
            CreateMap<Tarefa, TarefaDto>()
            .AfterMap((src, dest) =>
            {
                dest.Prioridade = (Prioridade)src.Prioridade;
            });
            //Tarefa > TarefaCollection
            CreateMap<Tarefa, TarefaCollection>()
            .AfterMap((src, dest) =>
            {
                dest.DataHoraCadastro = DateTime.Now;
            });
            //TarefaCollection > TarefaDto
            CreateMap<TarefaCollection, TarefaDto>()
            .AfterMap((src, dest) =>
            {
                dest.Prioridade = (Prioridade)src.Prioridade;
            })
            .ReverseMap();
        }
    }
}