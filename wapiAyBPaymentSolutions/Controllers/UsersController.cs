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
    public class UsersController : ApiController
    {

        [Route("api/Users/{deviceID}")]
        [HttpGet]
        public UsersResponse getListUsers(string deviceID)
        {
            var response = new UsersModel();
            return response.getUsers(deviceID);
        }

        [Route("api/Users/saveUser")]
        [HttpPost]
        public UserActionResponse saveUserInfo([FromBody]UserInfoResponse infoUser)
        {
            var responseAction = new UsersModel();
            return responseAction.saveUser(infoUser);
        }

        [Route("api/Users/getUserInfo/{userID}")]
        [HttpGet]
        public UserInfoResponse getUserInfo(int userID)
        {
            var responseUser = new UsersModel();
            return responseUser.getInfo(userID);
        }

        [Route("api/Users/inactivarUser/{userID}")]
        [HttpGet]
        public UserActionResponse inactivarUser (int userID)
        {
            var responseAction = new UsersModel();
            return responseAction.inactivateUser(userID);
        }

    }
}
