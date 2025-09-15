using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using TarefasApp.Application.Commands;
using TarefasApp.Application.Commands.TarefasApp.Application.Commands;
using TarefasApp.Application.Dtos;

namespace TarefasApp.IntegrationTests
{
    /// <summary>
    /// Testes de integração do ENDPOINT da API de tarefas
    /// </summary>
    public class TarefasTest
    {
        [Fact]
        public async Task PostTarefas_ReturnsSuccessStatusCode()
        {
            var command = new TarefaCreateCommand
            {
                Nome = "Nova Tarefa",
                Data = DateTime.UtcNow.ToString("yyyy-MM-dd"), // string válida
                Hora = DateTime.UtcNow.ToString("HH:mm"),      // string válida
                Descricao = "Descrição da nova tarefa",
                Prioridade = 2
            };

            var client = new WebApplicationFactory<Program>().CreateClient();
            var response = await client.PostAsJsonAsync("/api/tarefas", command);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PutTarefas_ReturnsSuccessStatusCode()
        {
            var client = new WebApplicationFactory<Program>().CreateClient();
            // Cria uma nova tarefa primeiro
            var createCommand = new TarefaCreateCommand
            {
                Nome = "Tarefa para atualizar",
                Data = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Hora = DateTime.UtcNow.ToString("HH:mm"),
                Descricao = "Descrição da tarefa",
                Prioridade = 2
            };

            var createResponse = await client.PostAsJsonAsync("/api/tarefas", createCommand);
            createResponse.EnsureSuccessStatusCode();
            var createdTask = await createResponse.Content.ReadFromJsonAsync<TarefaDto>();
            var id = createdTask!.Id;
            //atualiza a tarefa criada
            var updateCommand = new TarefaUpdateCommand
            {
                Id = id,
                Nome = "Tarefa Atualizada",
                Data = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Hora = DateTime.UtcNow.ToString("HH:mm"),
                Descricao = "Descrição atualizada",
                Prioridade = 3
            };

            var updateResponse = await client.PutAsJsonAsync("/api/tarefas", updateCommand);

            Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteTarefas_ReturnsSuccessStatusCode()
        {
            var client = new WebApplicationFactory<Program>().CreateClient();
            // Cria uma nova tarefa primeiro
            var createCommand = new TarefaCreateCommand
            {
                Nome = "Tarefa para deletar",
                Data = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Hora = DateTime.UtcNow.ToString("HH:mm"),
                Descricao = "Descrição da tarefa",
                Prioridade = 2
            };

            var createResponse = await client.PostAsJsonAsync("/api/tarefas", createCommand);
            createResponse.EnsureSuccessStatusCode();

            // Recupera o ID do retorno
            var createdTask = await createResponse.Content.ReadFromJsonAsync<TarefaDto>();
            var id = createdTask!.Id;

            // Agora deleta usando o ID real
            var deleteResponse = await client.DeleteAsync($"/api/tarefas/{id}");
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task GetTarefas_ReturnsSuccessStatusCode()
        {
            var client = new WebApplicationFactory<Program>().CreateClient();
            var response = await client.GetAsync("/api/tarefas");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
