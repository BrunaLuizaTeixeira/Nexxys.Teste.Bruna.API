using System;
using Teste.Bruna.Nexxys.WebAPI;

namespace Nexxys.Teste.Bruna.API.Models
{
    public class Validation : IDisposable
    {
        public bool LoginValidate(UserInfo request)
        {            
            if (request.User == "NexxysBruna" && request.Password == "BrunaLuiza@123456789")
                return true;
            else
                return false;
        }

        public void Dispose()
        {
        }
    }
}
