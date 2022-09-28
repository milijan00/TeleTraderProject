using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using TeleTraderApplicationProject.Application.Dto;
using TeleTraderApplicationProject.Application.UseCases.Queries;

namespace TeleTraderApplicationProject.Implementation.UseCases.Queries
{
    public class GetSymbolsWithQuotesQuery : IGetSumbolsWithQuotesQuery
    {
        private IHttpClientFactory _factory;
        public GetSymbolsWithQuotesQuery(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        public int Id => 2;

        public string Name => "GetSymbolsWithQuotesQuery";

        public async Task<IEnumerable<SymbolWithPricesDto>> Execute(SymbolsIdsAndPathDto request)
        {
            var xmlSymbols = this.GetSymbols(request);
            if(xmlSymbols.Count == 0)
            {
                return new List<SymbolWithPricesDto>();
            }

            return await this.FetchBitfinexData(xmlSymbols);
        }
        private List<SymbolWithPricesDto> GetSymbols(SymbolsIdsAndPathDto dto)
        {
            XmlDocument xml = new XmlDocument();

            xml.Load(dto.Path);
            List<SymbolWithPricesDto> response = new List<SymbolWithPricesDto>();
            foreach(XmlNode node in xml.SelectNodes("/SymbolList/Symbol"))
            {
                if (dto.Id.Contains(node.Attributes["id"].Value))
                {
                    response.Add(new SymbolWithPricesDto
                    {
                        Id = node.Attributes["id"].Value,
                        Name = node.Attributes["name"].Value,
                        Ticker = node.Attributes["ticker"].Value
                    });
                }
            }
            return response;
        }
        private List<string> Tickers(List<SymbolWithPricesDto> symbols) => symbols.Select(x => "t" + x.Ticker ).ToList();
        private async Task<IEnumerable<SymbolWithPricesDto>> FetchBitfinexData(List<SymbolWithPricesDto> xmlSymbols)
        {
            string apiPath = "https://api-pub.bitfinex.com/v2/tickers?symbols=" + string.Join(",",string.Join(",", this.Tickers(xmlSymbols)));
            var client = this._factory.CreateClient();
            var response = await client.GetAsync(apiPath);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var apiSymbols = JsonConvert.DeserializeObject<List<ArrayList>>(responseString);

                #region Indexes
                const int SYMBOL = 0;
                const int ASK = 3;
                const int LAST_PRICE = 7;
                const int HIGH = 9;
                const int LOW = 10;
                const int BID = 1;
                #endregion 
                const int indexFromWhichSymbolNameBegins = 1; 
                foreach (var apiSymbol in apiSymbols)
                {
                    foreach(var xmlSymbol in xmlSymbols)
                    {
                        if (apiSymbol[SYMBOL].ToString().Substring(indexFromWhichSymbolNameBegins) == xmlSymbol.Ticker)
                        {
                            xmlSymbol.AskPrice = Convert.ToDecimal(apiSymbol[ASK]);
                            xmlSymbol.BidPrice = Convert.ToDecimal(apiSymbol[BID]);
                            xmlSymbol.HighPrice = Convert.ToDecimal(apiSymbol[HIGH]);
                            xmlSymbol.LowPrice = Convert.ToDecimal(apiSymbol[LOW]);
                            xmlSymbol.LastPrice = Convert.ToDecimal(apiSymbol[LAST_PRICE]);
                        }
                    }
                }
                return xmlSymbols;
            }
            else
            {
                throw new InvalidOperationException();
            };
        }
    }
}
