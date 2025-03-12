using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;

namespace Bernhoeft.GRT.Teste.Api.Controllers.v1
{
    /// <response code="401">Não Autenticado.</response>
    /// <response code="403">Não Autorizado.</response>
    /// <response code="500">Erro Interno no Servidor.</response>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
    public class AvisosController : RestApiController
    {

        ///// <summary>
        ///// Retorna um Aviso por ID.
        ///// </summary>
        ///// <param name="request"></param>
        ///// <param name="cancellationToken"></param>
        ///// <returns>Aviso.</returns>
        ///// <response code="200">Sucesso.</response>
        ///// <response code="400">Dados Inválidos.</response>
        ///// <response code="404">Aviso Não Encontrado.</response>
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAvisoResponse))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[JwtAuthorize(Roles = AuthorizationRoles.CONTRACTLAYOUT_SISTEMA_AVISO_PESQUISAR)]
        //public async Task<object> GetAviso([FromModel] GetAvisoRequest request, CancellationToken cancellationToken)
        //    => await Mediator.Send(request, cancellationToken);

        /// <summary>
        /// Retorna Todos os Avisos Cadastrados para Tela de Edição.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Lista com Todos os Avisos.</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="204">Sem Avisos.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDocumentationRestResult<IEnumerable<GetAvisosResponse>>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<object> GetAvisos(CancellationToken cancellationToken)
            => await Mediator.Send(new GetAvisosRequest(), cancellationToken);

        /// <summary>
        /// Retorna um Aviso por ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Aviso.</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="404">Aviso Não Encontrado.</response>    
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAvisosResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAviso(int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAvisoRequest { Id = id }, cancellationToken);
            if (!result.IsSuccess)
                return NotFound(result.Errors);
            return Ok(result.Data);
        }

        /// <summary>
        /// Cria um Aviso.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Aviso Criado.</returns>
        /// <response code="201">Aviso Criado.</response>
        /// <response code="400">Dados Inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAviso([FromBody] CreateAvisoRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Entering CreateAviso method in AvisosController");
            var result = await Mediator.Send(request, cancellationToken);
            if (!result.IsSuccess)
            {
                Console.WriteLine("CreateAviso method in AvisosController: BadRequest");
                return BadRequest(result.Errors);
            }
            Console.WriteLine("CreateAviso method in AvisosController: Created");
            return CreatedAtAction(nameof(GetAviso), new { id = result.Data.Id }, result.Data);
        }

        /// <summary>
        /// Atualiza um Aviso.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Aviso Atualizado.</returns>
        /// <response code="200">Aviso Atualizado.</response>
        /// <response code="400">Dados Inválidos.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAviso(int id, [FromBody] UpdateAvisoRequest request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                return BadRequest("ID não corresponde ao corpo da requisição.");

            var result = await Mediator.Send(request, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }

        /// <summary>
        /// Deleta um Aviso.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Sem Conteúdo.</returns>
        /// <response code="204">Sem Conteúdo,mas marcado como deletado</response>
        /// <response code="404">Aviso Não Encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAviso(int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteAvisoRequest { Id = id }, cancellationToken);
            if (!result.IsSuccess)
                return NotFound(result.Errors);
            return NoContent();
        }
    }
}