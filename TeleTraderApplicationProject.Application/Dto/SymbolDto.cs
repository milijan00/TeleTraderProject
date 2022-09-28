using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TeleTraderApplicationProject.Application.Dto
{
    public class SymbolDto
    {
        [XmlAttribute]
        public string Id { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Ticker { get; set; }
    }
}
