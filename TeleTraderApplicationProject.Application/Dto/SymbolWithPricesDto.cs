using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleTraderApplicationProject.Application.Dto
{
    public class SymbolWithPricesDto : SymbolDto
    {
        public decimal? LastPrice { get; set; }
        public decimal? LowPrice { get; set; }
        public decimal? HighPrice { get; set; }
        public decimal? BidPrice { get; set; }
        public decimal? AskPrice { get; set; }
    }
}
