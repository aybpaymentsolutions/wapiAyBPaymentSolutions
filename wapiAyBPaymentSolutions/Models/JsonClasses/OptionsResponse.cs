using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wapiAyBPaymentSolutions.Models.JsonClasses
{

    public partial class OptionsResponse
    {
        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty("options")]
        public List<Option> Options { get; set; }
    }

    public partial class Option
    {
        [JsonProperty("moduleID")]
        public int ModuleId { get; set; }

        [JsonProperty("moduleName")]
        public string ModuleName { get; set; }

        [JsonProperty("optionsList")]
        public List<OptionsList> OptionsList { get; set; }
    }

    public partial class OptionsList
    {
        [JsonProperty("optionID")]
        public int OptionId { get; set; }

        [JsonProperty("optionText")]
        public string OptionText { get; set; }

        [JsonProperty("enabled")]
        public int Enabled { get; set; }
    }
}