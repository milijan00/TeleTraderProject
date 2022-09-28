using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleTraderApplicationProject.Application.Dto;

namespace TeleTraderApplicationProject.Application.UseCases.Queries
{
    public interface IGetAllSymbolsQuery : IQueryWithRequestData<IEnumerable<SymbolDto>, string>
    {
    }
}
