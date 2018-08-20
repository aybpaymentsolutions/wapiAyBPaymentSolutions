using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wapiAyBPaymentSolutions.Models.JsonClasses
{
    public partial class PermissionsResponse
    {
        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty("profiles")]
        public List<Profile> Profiles { get; set; }
    }

    public partial class Profile
    {
        [JsonProperty("profileID")]
        public long ProfileId { get; set; }

        [JsonProperty("profileText")]
        public string ProfileText { get; set; }
    }
}