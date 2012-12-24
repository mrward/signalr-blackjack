using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using SignalrBlackjack.Models;

namespace SignalrBlackjack
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RouteTable.Routes.MapConnection<BlackjackPersistentConnection>("signalr", "signalr/{*operation}");
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}