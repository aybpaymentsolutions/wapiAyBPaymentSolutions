using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace wapiAyBPaymentSolutions.Models.JsonClasses
{

    public partial class CategoriesResponse
    {
        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty("infoCategories")]
        public List<InfoCategories> InfoCategories { get; set; }
    }

    public partial class InfoCategories
    {
        [JsonProperty("MenuCategoryID")]
        public int MenuCategoryID { get; set; }
        [JsonProperty("MenuCategoryText")]
        public string MenuCategoryText { get; set; }
        [JsonProperty("MenuCategoryInActive")]
        public bool MenuCategoryInActive { get; set;}
        [JsonProperty("StoreID")]
        public string StoreID { get; set; }
    }

}