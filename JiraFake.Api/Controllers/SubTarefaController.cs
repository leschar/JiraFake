using JiraFake.Application.Adapters;
using JiraFake.Application.ViewModels;
using JiraFake.Domain.Commands.Tarefa;
using JiraFake.Domain.Interfaces.Models;
using JiraFake.Domain.Mediator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JiraFake.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTarefaController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly ILogger<SubTarefaController> _logger;
        private readonly ISubTarefaRepository _repository;

        public SubTarefaController(IMediatorHandler mediatorHandler, ILogger<SubTarefaController> logger, ISubTarefaRepository repository)
        {
            _mediator = mediatorHandler;
            _logger = logger;
            _repository = repository;
        }


        // GET api/<subtarefa>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return CustomResponse(SubTarefaModelAdapter.ConvertToView(await _repository.GetById(id)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"A aplicação gerou um erro: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Erro interno ao obter tarefas" });
            }
        }
        // POST api/<SubTarefa>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Post([FromBody] AdicionarSubTarefaViewModel model)
        {
            try
            {
                await _mediator.EnviarComando(SubTarefaModelAdapter.ConvertToDomain(model));
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"A aplicação gerou um erro: {ex.Message}");
                return StatusCode(500, new { mensagem = "Erro interno ao criar Sub Tarefa" });

            }

        }



        // PUT api/<SubTarefa>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Valores que representam o status,  Fechado = 0,\r\n        Aberto = 1,\r\n        ParaFazer = 2,\r\n        EmProgresso = 3,\r\n        EmTestes = 4,\r\n        TestesFinalizados = 5,\r\n        Concluido = 6 ")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Put([FromBody] EditarSubTarefaViewModel model)
        {
            _logger.LogInformation($"Model recebida : {JsonSerializer.Serialize(model)}");
            try
            {
                return CustomResponse(await _mediator.EnviarComando(SubTarefaModelAdapter.ConvertToDomain(model)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"A aplicação gerou um erro: {ex.Message} para a mensagem {JsonSerializer.Serialize(model)}");
                return StatusCode(500, new { mensagem = "Erro interno ao criar sub tarefa" });

            }

        }

        // Delete api/<Tarefa>/5
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new RemoverSubTarefaCommand(id);
                return CustomResponse(await _mediator.EnviarComando(command));
            }
            catch (Exception ex)
            {
                _logger.LogError($"A aplicação gerou um erro para remover: {ex.Message} para o id:  {id}");
                return StatusCode(500, new { mensagem = "Erro interno ao criar tarefa" });

            }
        }
    }
}
