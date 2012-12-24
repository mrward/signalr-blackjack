using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalrBlackjack.Models
{
    public class PlayingCards
    {
        List<int> cards;
        int currentPackLocation;
        Random random = new Random();

        public PlayingCards()
        {
            cards = GetShuffledPack();
        }

        public int GetRandomInt(int max)
        {
            return random.Next(max + 1);
        }

        public List<int> GetShuffledPack()
        {
            var cards = new int[52];
            cards[0] = 0;
            for (int i = 1; i < 52; i++) {
                int j = GetRandomInt(i);
                cards[i] = cards[j];
                cards[j] = i;
            }
            return new List<int>(cards);
        }

        public int DealNextCard()
        {
            if (currentPackLocation >= cards.Count)
            {
                cards = GetShuffledPack();
                currentPackLocation = 0;
            }

            int cardNumber = cards[currentPackLocation];
            currentPackLocation++;
            return cardNumber;
        }
    }
}