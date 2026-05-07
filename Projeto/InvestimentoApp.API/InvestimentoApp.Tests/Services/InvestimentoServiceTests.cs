using Moq;
using Xunit;
using InvestimentoApp.Application.Services;
using InvestimentoApp.Domain.Interfaces;
using InvestimentoApp.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace InvestimentoApp.Tests;

public class InvestimentoServiceTests
{
    [Fact]
    public async Task Calcular_DeveBaterComExemploDoPDF()
    {
        // Arrange
        var mockRepo = new Mock<IInvestimentoRepository>();
        var mockLogger = new Mock<ILogger<InvestimentoService>>();

        var valorInicial = 10000m;
        var dataInicio = new DateTime(2025, 03, 13);
        var dataFim = new DateTime(2025, 03, 21);

        // Simulando as taxas da tabela do PDF [cite: 25]
        var cotacoesFake = new List<Cotacao>
        {
            new() { Data = new DateTime(2025, 03, 13), Valor = 12.00m }, // Taxa do dia 13 inclusa
            new() { Data = new DateTime(2025, 03, 14), Valor = 12.50m },
            new() { Data = new DateTime(2025, 03, 17), Valor = 11.00m },
            new() { Data = new DateTime(2025, 03, 18), Valor = 12.20m },
            new() { Data = new DateTime(2025, 03, 19), Valor = 13.00m },
            new() { Data = new DateTime(2025, 03, 20), Valor = 12.40m } // Termina dia 20
        };

        mockRepo.Setup(r => r.ObterCotacoesPorPeriodoAsync(dataInicio, dataFim))
                .ReturnsAsync(cotacoesFake);

        var service = new InvestimentoService(mockRepo.Object, mockLogger.Object);

        // Act
        var resultado = await service.CalcularInvestimentoAsync(valorInicial, dataInicio, dataFim);

        // Assert
        Assert.Equal(1.0027406329672245m, resultado.FatorAcumulado);
        Assert.Equal(10027.40632967m, resultado.ValorAtualizado);
    }
}