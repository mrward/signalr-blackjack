using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalrBlackjack.Models
{
    public class Player
    {
        public Card[] cards { get; set; }
        public int score { get; set; }
        public decimal balance { get; set; }
    }
}
