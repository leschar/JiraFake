using JiraFake.Application.Adapters;
using JiraFake.Application.ViewModels;
using JiraFake.Domain.Mediator;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JiraFake.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTarefaController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly ILogger<SubTarefaController> _logger;

        public SubTarefaController(IMediatorHandler mediatorHandler, ILogger<SubTarefaController> logger)
        {
            _mediator = mediatorHandler;
            _logger = logger;
        }

        // POST api/<SubTarefa>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] AdicionarSubTarefaViewModel model)
        {
            try
            {
                return CustomResponse(await _mediator.EnviarComando(SubTarefaModelAdapter.ConvertToDomain(model)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"A aplicação gerou um erro: {ex.Message}");
                return StatusCode(500, new { mensagem = "Erro interno ao criar Sub Tarefa" });

            }

        }
    }
}
