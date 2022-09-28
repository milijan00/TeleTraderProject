using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleTraderApplicationProject.Application.UseCases
{
    public interface IQuery<TResponse> : IUseCase
    {
        TResponse Execute();
    }
}
