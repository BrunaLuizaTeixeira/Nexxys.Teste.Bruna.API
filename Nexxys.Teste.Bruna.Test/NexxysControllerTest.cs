using NUnit.Framework;
using Teste.Bruna.Nexxys.WebAPI;
using Teste.Bruna.Nexxys.WebAPI.Controllers;

namespace Nexxys.Teste.Bruna.Test
{
    public class NexxysControllerTest
    {
       

        [Test]
        public void AccessToken()
        {
            var controller = new NexxysController();
            var AccessToken = controller.AccessToken(new UserInfo { User = "NexxysBruna", Password = "BrunaLuiza@123456789" });

             Assert.IsNotNull(AccessToken.Result.Value.Token);
           
        }


        [Test]
        public void CheckPassword()
        {
            var controller = new NexxysController();
            var CheckPassword = controller.CheckPassword(new API.ParamRequest { Password = "BrunaLuiza@123456789" });

            Assert.IsTrue(CheckPassword.Result.Value.IsValid);
            
        }

        [Test]
        public void CreatePassword()
        {
            var controller = new NexxysController();
            var CreatePassword = controller.CreatePassword();
            
            Assert.IsNotNull(CreatePassword.Result.Value.Password);
        }
    }
}