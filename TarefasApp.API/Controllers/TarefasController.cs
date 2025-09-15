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
            if (dto == null)
                return BadRequest("Não foi possível criar a tarefa");
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
        /// <summary>
        /// Serviço para atualiação de tarefas.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(TarefaDto), 200)]
        public async Task<IActionResult> Put(TarefaUpdateCommand command)
        {
            var dto = await _tarefaAppService.Update(command);
            if (dto == null)
                return NotFound("Tarefa não encontrada");

            return Ok(dto);
        }
        /// <summary>
        /// Serviço para exclusão / inativação de tarefas.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TarefaDto), 200)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var command = new TarefaDeleteCommand { Id = id };
            var dto = await _tarefaAppService.Delete(command);
            if (dto == null)
                return NotFound("Tarefa não encontrada");

            return Ok(dto);
        }
        /// <summary>
        /// Serviço para consulta de tarefas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<TarefaDto>), 200)]
        public IActionResult GetAll()
        {
            var dtos = _tarefaAppService.GetAll();
            return Ok(dtos);
        }/// <summary>
         /// Serviço para consulta de tarefa por id.
         /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TarefaDto), 200)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var dto = _tarefaAppService.GetById(id);
            if (dto == null)
                return NotFound("Tarefa não encontrada");
            return Ok(dto);
        }
    }
}