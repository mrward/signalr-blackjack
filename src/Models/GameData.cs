using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalrBlackjack.Models
{
    public class GameData
    {
        public string result { get; set; }
        public Dealer dealer { get; set; }
        public Player player { get; set; }
    }
}