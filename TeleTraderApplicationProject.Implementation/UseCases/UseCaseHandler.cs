using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleTraderApplicationProject.Application.Logging;
using TeleTraderApplicationProject.Application.UseCases;

namespace TeleTraderApplicationProject.Implementation.UseCases
{
    public class UseCaseHandler
    {
        private IExceptionLogger _logger;
        public UseCaseHandler(IExceptionLogger logger)
        {
            this._logger = logger;
        }

        public TResponse HandleQuery<TResponse>(IQuery<TResponse> query)
        {
            try
            {
                return query.Execute();
            }catch(Exception ex)
            {
                this._logger.Log(ex);
                throw;
            }
        }
        public TResponse HandleQueryWithRequestData<TResponse, TRequest>(IQueryWithRequestData<TResponse, TRequest> query, TRequest data)
        {
            try
            {
                return query.Execute(data);
            }catch(Exception ex)
            {
                this._logger.Log(ex);
                throw;
            }
        }
    }
}
