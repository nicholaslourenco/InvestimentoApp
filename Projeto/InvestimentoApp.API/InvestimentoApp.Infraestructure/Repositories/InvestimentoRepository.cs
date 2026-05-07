using InvestimentoApp.Domain.Entities;
using InvestimentoApp.Domain.Interfaces;
using InvestimentoApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InvestimentoApp.Infrastructure.Repositories;

public class InvestimentoRepository(InvestimentoDbContext context) : IInvestimentoRepository
{
    public async Task<List<Cotacao>> ObterCotacoesPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        var todas = await context.Cotacoes
            .Where(c => c.Data >= dataInicio.AddDays(-1) && c.Data <= dataFim)
            .ToListAsync();

        return todas
            .Where(c => c.Data.Date >= dataInicio.Date && c.Data.Date < dataFim.Date)
            .OrderBy(c => c.Data)
            .ToList();
    }
}