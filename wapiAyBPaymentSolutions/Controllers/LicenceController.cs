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
    public class LicenceController : ApiController
    {
        [Route("api/Licence/{deviceID}/{commerceID}")]
        [HttpGet]
        public LicenceResponse addLicence(string deviceID, string commerceID)
        {
            var response = new LicenceModel();
            return response.insertLicence(deviceID, commerceID);
        }
    }
}
