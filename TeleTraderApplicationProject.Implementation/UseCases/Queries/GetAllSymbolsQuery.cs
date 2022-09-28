using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TeleTraderApplicationProject.Application.Dto;
using TeleTraderApplicationProject.Application.UseCases.Queries;
using System.IO;
using System.Xml;

namespace TeleTraderApplicationProject.Implementation.UseCases.Queries
{
    public class GetAllSymbolsQuery : IGetAllSymbolsQuery
    {
        public int Id => 1;
        public string Name => "GetAllSymbols.";

        public IEnumerable<SymbolDto> Execute(string path)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            List<SymbolDto> response = new List<SymbolDto>();
            foreach(XmlNode node in xml.SelectNodes("/SymbolList/Symbol"))
            {
                response.Add(new SymbolDto
                {
                    Id = node.Attributes["id"].Value,
                    Name = node.Attributes["name"].Value,
                    Ticker = node.Attributes["ticker"].Value
                });
            }
            return response;
        }
    }
}
