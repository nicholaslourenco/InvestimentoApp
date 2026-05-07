namespace InvestimentoApp.Application.DTOs;

public record InvestimentoResponse(
    decimal ValorOriginal,
    DateTime DataInicio,
    DateTime DataFim,
    decimal FatorAcumulado,
    decimal ValorAtualizado
);

// DTO de Entrada (Request)
public record InvestimentoRequest(
    decimal ValorInvestido,
    DateTime DataInicio,
    DateTime DataFim
);