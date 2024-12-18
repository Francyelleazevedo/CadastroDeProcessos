using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CadastroDeProcessos.Application.Interfaces;
using CadastroDeProcessos.Application.Services;
using CadastroDeProcessos.Domain.Interfaces;
using CadastroDeProcessos.Infra.Context;
using CadastroDeProcessos.Infra.Repositories;

namespace CadastroDeProcessos.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProcessoRepository, ProcessoRepository>();
        services.AddScoped<IProcessoService, ProcessoService>();

        return services;
    }
}
