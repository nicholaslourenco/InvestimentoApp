using System;
using System.Collections.Generic;
using System.Text;
using InvestimentoApp.Domain.Entities;

namespace InvestimentoApp.Domain.Interfaces;

public interface IInvestimentoRepository
{
    Task<List<Cotacao>> ObterCotacoesPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
}
