using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TarefasApp.API.Controllers;
using TarefasApp.Application.Commands;
using TarefasApp.Application.Commands.TarefasApp.Application.Commands;
using TarefasApp.Application.Dtos;
using TarefasApp.Application.Interfaces;
using Xunit;

namespace TarefasApp.UnitTests
{
    public class TarefasUnitTest
    {
        private readonly Mock<ITarefaAppService> _tarefaServiceMock;
        private readonly TarefasController _controller;

        public TarefasUnitTest()
        {
            _tarefaServiceMock = new Mock<ITarefaAppService>();
            _controller = new TarefasController(_tarefaServiceMock.Object);
        }

        [Fact]
        public async Task PostTarefas_ReturnsCreatedResult()
        {
            var command = new TarefaCreateCommand
            {
                Nome = "Nova Tarefa",
                Data = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Hora = DateTime.UtcNow.ToString("HH:mm"),
                Descricao = "Descrição da nova tarefa",
                Prioridade = 2
            };

            var createdDto = new TarefaDto
            {
                Id = Guid.NewGuid(),
                Nome = command.Nome,
                Descricao = command.Descricao,
                DataHora = DateTime.Parse($"{command.Data} {command.Hora}"),    
                Prioridade = (Prioridade?)command.Prioridade
            };

            _tarefaServiceMock
                .Setup(s => s.Create(It.IsAny<TarefaCreateCommand>()))
                .ReturnsAsync(createdDto);

            var result = await _controller.Post(command);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<TarefaDto>(createdResult.Value);
            Assert.Equal(command.Nome, returnValue.Nome);
        }

        [Fact]
        public async Task PutTarefas_ReturnsOkResult()
        {
            var updateCommand = new TarefaUpdateCommand
            {
                Id = Guid.NewGuid(),
                Nome = "Tarefa Atualizada",
                Data = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Hora = DateTime.UtcNow.ToString("HH:mm"),
                Descricao = "Descrição atualizada",
                Prioridade = 3
            };

            var updatedDto = new TarefaDto
            {
                Id = updateCommand.Id,
                Nome = updateCommand.Nome,
                Descricao = updateCommand.Descricao,
                DataHora = DateTime.Parse($"{updateCommand.Data} {updateCommand.Hora}"),
                Prioridade = (Prioridade?)updateCommand.Prioridade
            };

            _tarefaServiceMock
                .Setup(s => s.Update(It.IsAny<TarefaUpdateCommand>()))
                .ReturnsAsync(updatedDto);

            var result = await _controller.Put(updateCommand);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<TarefaDto>(okResult.Value);
            Assert.Equal("Tarefa Atualizada", returnValue.Nome);
        }

        [Fact]
        public async Task DeleteTarefas_ReturnsOkResult()
        {
            var id = Guid.NewGuid();
            var deleteCommand = new TarefaDeleteCommand { Id = id };

            var deletedDto = new TarefaDto
            {
                Id = id,
                Nome = "Tarefa Deletada",
                Descricao = "Descrição da tarefa deletada"
            };

            _tarefaServiceMock
                .Setup(s => s.Delete(It.IsAny<TarefaDeleteCommand>()))
                .ReturnsAsync(deletedDto);

            var result = await _controller.Delete(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<TarefaDto>(okResult.Value);
            Assert.Equal(id, returnValue.Id);
        }

        [Fact]
        public void GetTarefas_ReturnsOkObjectResult()
        {
            var tarefas = new List<TarefaDto>
            {
                new TarefaDto { Id = Guid.NewGuid(), Nome = "Tarefa 1", DataHora = DateTime.UtcNow, Descricao = "Desc 1", Prioridade = (Prioridade?)1 },
                new TarefaDto { Id = Guid.NewGuid(), Nome = "Tarefa 2", DataHora = DateTime.UtcNow , Descricao = "Desc 2", Prioridade = (Prioridade?)2 }
            };

            _tarefaServiceMock.Setup(s => s.GetAll()).Returns(tarefas);

            var result = _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TarefaDto>>(okResult.Value);
            Assert.Equal(2, ((List<TarefaDto>)returnValue).Count);
        }
    }
}
