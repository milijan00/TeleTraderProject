using Microsoft.Extensions.DependencyInjection;
using TeleTraderApplicationProject.Application.UseCases.Queries;
using TeleTraderApplicationProject.Implementation.UseCases.Queries;

namespace TeleTraderApplicationProject.API.Extensions
{
    public static class DependencyInjectionContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetAllSymbolsQuery, GetAllSymbolsQuery>();
            services.AddTransient<IGetSumbolsWithQuotesQuery, GetSymbolsWithQuotesQuery>();
        }
    }
}
