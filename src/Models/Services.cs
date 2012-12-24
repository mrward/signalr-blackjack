using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalrBlackjack.Models
{
    public static class Services
    {
        static readonly BlackjackServer server = new BlackjackServer();

        public static BlackjackServer Server
        {
            get { return server; }
        }
    }
}