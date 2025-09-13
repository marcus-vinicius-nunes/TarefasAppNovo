using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarefasApp.Application.Commands;
using TarefasApp.Application.Commands.TarefasApp.Application.Commands;
using TarefasApp.Application.Dtos;
using TarefasApp.Application.Interfaces;
namespace TarefasApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        //atributo
        private readonly ITarefaAppService? _tarefaAppService;
        //construtor para injeção de dependência
        public TarefasController(ITarefaAppService? tarefaAppService)
        {
            _tarefaAppService = tarefaAppService;
        }
        /// <summary>
        /// Serviço para cadastro de tarefas.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TarefaDto), 201)]
        public async Task<IActionResult> Post(TarefaCreateCommand command)
        {
            var dto = await _tarefaAppService.Create(command);
            return StatusCode(201, dto);
        }
        /// <summary>
        /// Serviço para atualiação de tarefas.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(TarefaDto), 200)]
        public async Task<IActionResult> Put(TarefaUpdateCommand command)
        {
            var dto = await _tarefaAppService.Update(command);
            return StatusCode(200, dto);
        }
        /// <summary>
        /// Serviço para exclusão / inativação de tarefas.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TarefaDto), 200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new TarefaDeleteCommand { Id = id };
            var dto = await _tarefaAppService.Delete(command);

            return StatusCode(200, dto);
        }
        /// <summary>
        /// Serviço para consulta de tarefas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<TarefaDto>), 200)]
        public IActionResult GetAll()
        {
            var dtos = _tarefaAppService.GetAll();
            return StatusCode(200, dtos);
        }/// <summary>
         /// Serviço para consulta de tarefa por id.
         /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TarefaDto), 200)]
        public IActionResult GetById(Guid id)
        {
            var dto = _tarefaAppService.GetById(id);
            return StatusCode(200, dto);
        }
    }
}