using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace wapiAyBPaymentSolutions
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "addLicence",
                routeTemplate: "api/{controller}/{deviceID}/{commerceID}",
                defaults: new { deviceID = RouteParameter.Optional, commerceID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "getInfoUser",
                routeTemplate: "api/{controller}/{phoneID}/{pinCode}",
                defaults: new { phoneID = RouteParameter.Optional, pinCode = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "getListUsers",
                routeTemplate: "api/{controller}/{deviceID}",
                defaults: new { deviceID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "getUserInfo",
                routeTemplate: "api/{controller}/getUserInfo/{userID}",
                defaults: new { userID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "inactivarUser",
                routeTemplate: "api/{controller}/inactivarUser/{userID}",
                defaults: new { userID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "getPermissions",
                routeTemplate: "api/{controller}/permissions"
            );

            config.Routes.MapHttpRoute(
                name: "getOptions",
                routeTemplate: "api/{controller}/options/{profileID}",
                defaults: new { profileID = RouteParameter.Optional }
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
