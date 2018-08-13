using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace wapiAyBPaymentSolutions.Models.JsonClasses
{

    public partial class UserInfoResponse
    {
        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty("userData")]
        public UserData UserData { get; set; }
    }

    public partial class UserData
    {
        [JsonProperty("personalInfo")]
        public PersonalInfo PersonalInfo { get; set; }

        [JsonProperty("salaryInfo")]
        public SalaryInfo SalaryInfo { get; set; }

        [JsonProperty("contactInfo")]
        public ContactInfo ContactInfo { get; set; }
    }

    public partial class ContactInfo
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("complementary")]
        public string Complementary { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("zip")]
        public int Zip { get; set; }
    }

    public partial class PersonalInfo
    {
        [JsonProperty("userID")]
        public int UserId { get; set; }

        [JsonProperty("fname")]
        public string Fname { get; set; }

        [JsonProperty("lname")]
        public string Lname { get; set; }

        [JsonProperty("pinCode")]
        public string PinCode { get; set; }

        [JsonProperty("deviceID")]
        public string DeviceId { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }

    public partial class SalaryInfo
    {
        [JsonProperty("hourlyRate")]
        public int HourlyRate { get; set; }

        [JsonProperty("payrate")]
        public decimal PayRate { get; set; }
    }
}