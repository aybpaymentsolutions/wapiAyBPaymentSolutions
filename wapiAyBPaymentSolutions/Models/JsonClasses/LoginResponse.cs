using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wapiAyBPaymentSolutions.Models.JsonClasses
{
    public partial class LoginResponse
    {
        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty("infoUser")]
        public InfoUser InfoUser { get; set; }
    }

    public partial class InfoUser
    {
        [JsonProperty("userID")]
        public int userID { get; set; }

        [JsonProperty("fname")]
        public string Fname { get; set; }

        [JsonProperty("pinCode")]
        public string pinCode { get; set; }

        [JsonProperty("deviceID")]
        public string deviceID { get; set; }

        [JsonProperty("rol")]
        public string Rol { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("storename")]
        public string StoreName { get; set; }
    }
}