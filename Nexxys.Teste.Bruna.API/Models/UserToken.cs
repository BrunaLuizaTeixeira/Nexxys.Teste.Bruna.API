using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Bruna.Nexxys.WebAPI
{
    public class UserToken
    {
        public string UserName { get; set; }
        public bool IsAuthenticate { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
