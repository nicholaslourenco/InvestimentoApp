using InvestimentoApp.Application.DTOs;
using InvestimentoApp.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace InvestimentoApp.Application.Services;

public class InvestimentoService(IInvestimentoRepository repository, ILogger<InvestimentoService> logger)
{
    public async Task<InvestimentoResponse> CalcularInvestimentoAsync(decimal valorInvestido, DateTime dataInicio, DateTime dataFim)
    {
        logger.LogInformation("Iniciando cálculo: Valor {Valor}, de {Inicio} a {Fim}", valorInvestido, dataInicio, dataFim);

        var cotacoes = await repository.ObterCotacoesPorPeriodoAsync(dataInicio, dataFim);
        logger.LogInformation("Foram encontradas {Quantidade} cotações no banco.", cotacoes.Count);

        decimal fatorAcumulado = 1.0m;

        foreach (var cotacao in cotacoes)
        {
            logger.LogInformation("Data: {Data}, Taxa: {Taxa}", cotacao.Data, cotacao.Valor);
            // Fator Diário = (1 + taxa/100) ^ (1/252)
            double taxaAnual = (double)cotacao.Valor;
            double baseCalculo = 1.0 + (taxaAnual / 100.0);
            decimal fatorDiario = (decimal)Math.Pow(baseCalculo, 1.0 / 252.0);

            // Arredonda fator diário na 8ª casa decimal
            fatorDiario = Math.Round(fatorDiario, 8, MidpointRounding.AwayFromZero);

            // Acumula multiplicando
            fatorAcumulado *= fatorDiario;
        }

        // Truncar fator acumulado na 16ª casa decimal
        fatorAcumulado = Truncar(fatorAcumulado, 16);

        // Valor Atualizado = Valor Investido * Fator Acumulado (Truncar na 8ª casa)
        decimal valorAtualizado = Truncar(valorInvestido * fatorAcumulado, 8);

        logger.LogInformation("Cálculo finalizado. Fator Acumulado: {Fator}", fatorAcumulado);

        return new InvestimentoResponse(
            valorInvestido,
            dataInicio,
            dataFim,
            fatorAcumulado,
            valorAtualizado
        );
    }

    private static decimal Truncar(decimal valor, int casas)
    {
        decimal multiplicador = (decimal)Math.Pow(10, casas);
        return Math.Truncate(valor * multiplicador) / multiplicador;
    }
}