using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wapiAyBPaymentSolutions.Models.JsonClasses;
using wapiAyBPaymentSolutions.Models;

namespace wapiAyBPaymentSolutions.Controllers
{
    public class CategoriesController : ApiController
    {
        [Route("api/Categories/{storeID}")]
        [HttpGet]
        public CategoriesResponse getListCategories(string storeID)
        {
            var response = new MenuCatModel();
            return response.getCategories(storeID);
        }
    }
}
