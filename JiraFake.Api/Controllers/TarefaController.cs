using JiraFake.Application.Adapters;
using JiraFake.Application.ViewModels;
using JiraFake.Domain.Interfaces.Models;
using JiraFake.Domain.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

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
                var tarefas = TarefaModelAdapter.ConvertToView(await _repository.GetAll());
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var tarefa = TarefaModelAdapter.ConvertToView(await _repository.GetById(id));
                return CustomResponse(tarefa);
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            try
            {
                var tarefa = await _repository.ObterTarefasPorId(id);
                var tarefasViewModel = TarefaModelAdapter.ConvertListToView(tarefa);

                return CustomResponse(tarefasViewModel);
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
                return CustomResponse(await _mediator.EnviarComando(TarefaModelAdapter.ConvertToDomain(model)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"A aplicação gerou um erro: {ex.Message} para a mensagem {JsonSerializer.Serialize(model)}");
                return StatusCode(500, new { mensagem = "Erro interno ao criar tarefa" });

            }

        }
    }
}
