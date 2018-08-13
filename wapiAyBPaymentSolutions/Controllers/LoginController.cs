using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wapiAyBPaymentSolutions.Models;
using wapiAyBPaymentSolutions.Models.JsonClasses;

namespace wapiAyBPaymentSolutions.Controllers
{
    public class LoginController : ApiController
    {

        [Route("api/Login/{phoneID}/{pinCode}")]
        [HttpGet]
        public LoginResponse getInfoUser(string phoneID, string pinCode)
        {
            var response = new LoginModel();
            return response.doLogin(phoneID, pinCode);
        }

    }
}
