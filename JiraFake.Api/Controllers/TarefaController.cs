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
    public class TarefaController : MainController
    {
        private readonly ITarefaRepository _repository;
        private readonly IMediatorHandler _mediator;
        private readonly ILogger _logger;

        public TarefaController(ITarefaRepository repository, IMediatorHandler mediatorHandler, ILogger<TarefaController> logger)
        {
            _repository = repository;
            _mediator = mediatorHandler;
            _logger = logger;
        }


        // GET: api/<Tarefa>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tarefas = TarefaModelAdapter.ConvertToView(await _repository.GetAll())
                    .OrderByDescending(d => d.DataCadastro)
                    .ThenBy(n => n.Nome);

                return CustomResponse(tarefas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"A aplicação gerou um erro: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Erro interno ao obter tarefas" });
            }
        }

        // GET api/<Tarefa>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return CustomResponse(TarefaModelAdapter.ConvertToView(await _repository.GetById(id)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"A aplicação gerou um erro: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Erro interno ao obter tarefas" });
            }
        }

        // GET api/<detalhes>/5
        [HttpGet("detalhes/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            try
            {
                return CustomResponse(TarefaModelAdapter.ConvertListToView(await _repository.ObterTarefasPorId(id)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"A aplicação gerou um erro: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Erro interno ao obter tarefas" });
            }
        }



        // POST api/<Tarefa>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] AdicionarTarefaViewModel model)
        {
            _logger.LogInformation($"Model recebida : {JsonSerializer.Serialize(model)}");
            try
            {
                await _mediator.EnviarComando(TarefaModelAdapter.ConvertToDomain(model));
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"A aplicação gerou um erro: {ex.Message} para a mensagem {JsonSerializer.Serialize(model)}");
                return StatusCode(500, new { mensagem = "Erro interno ao criar tarefa" });
            }

        }

        // PUT api/<Tarefa>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Valores que representam o status,  Fechado = 0,\r\n        Aberto = 1,\r\n        ParaFazer = 2,\r\n        EmProgresso = 3,\r\n        EmTestes = 4,\r\n        TestesFinalizados = 5,\r\n        Concluido = 6 ")]
        public async Task<IActionResult> Put([FromBody] EditarTarefaViewModel model)
        {
            _logger.LogInformation($"Model recebida : {JsonSerializer.Serialize(model)}");
            try
            {
                return Ok(await _mediator.EnviarComando(TarefaModelAdapter.ConvertToDomain(model)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"A aplicação gerou um erro: {ex.Message} para a mensagem {JsonSerializer.Serialize(model)}");
                return StatusCode(500, new { mensagem = "Erro interno ao criar tarefa" });

            }

        }

        // Delete api/<Tarefa>/5
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new RemoverTarefaCommand(id);
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
