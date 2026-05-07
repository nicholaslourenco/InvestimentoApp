using InvestimentoApp.Application.DTOs;
using InvestimentoApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestimentoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestimentoController(InvestimentoService service, ILogger<InvestimentoController> logger) : ControllerBase
{
    [HttpPost("calcular")]
    public async Task<ActionResult<InvestimentoResponse>> Calcular([FromBody] InvestimentoRequest request)
    {
        try
        {
            logger.LogInformation("Recebida requisição de cálculo para o valor: {Valor}", request.ValorInvestido);

            // Validação básica de datas
            if (request.DataFim <= request.DataInicio)
            {
                return BadRequest("A data de fim deve ser maior que a data de início.");
            }

            var resultado = await service.CalcularInvestimentoAsync(
                request.ValorInvestido,
                request.DataInicio,
                request.DataFim
            );

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao processar o cálculo de investimento.");
            return StatusCode(500, "Erro interno ao processar o cálculo.");
        }
    }
}