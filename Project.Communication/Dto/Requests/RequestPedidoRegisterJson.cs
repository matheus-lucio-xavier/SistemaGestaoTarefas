using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Communication.Enum;

namespace Project.Communication.Dto.Requests
{
    public class RequestPedidoRegisterJson
    {
        public string Descricao { get; set; } = string.Empty;
        public AreaAtuacaoEnum AreaAtuacao { get; set; }
    }
}