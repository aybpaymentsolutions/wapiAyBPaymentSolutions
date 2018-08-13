using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wapiAyBPaymentSolutions.Models.JsonClasses
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class UserActionResponse
    {
        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty("idUser")]
        public int IdUser { get; set; }
    }
}