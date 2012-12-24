using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalrBlackjack.Models
{
    public class BlackjackHand
    {
        List<int> cards = new List<int>();

        public bool HasCards()
        {
            return cards.Count > 0;
        }

        public void AddCard(int number)
        {
            this.cards.Add(number);
        }

        public char NumberToSuit(int number)
        {
            var suits = new char[] {'C', 'D', 'H', 'S'};
            int index = (int)Math.Floor(number / 13.0);
            return suits[index];
        }

        public Card NumberToCard(int number)
        {
            return new Card {
                rank = (number % 13) + 1,
                suit = NumberToSuit(number)
            };
        }

        public Card[] GetCards()
        {
            var convertedCards = new List<Card>();
            for (int i = 0; i < cards.Count; i++) {
                int number = cards[i];
                convertedCards.Add(NumberToCard(number));
            }
            return convertedCards.ToArray();
        }

        public int GetCardScore(Card card)
        {
            if (card.rank == 1) {
                return 11;
            } else if (card.rank >= 11) {
                return 10;
            }
            return card.rank;
        }

        public int GetScore()
        {
            int score = 0;
            Card[] cards = GetCards();
            var aces = new List<Card>();

            // Sum all cards excluding aces.
            for (int i = 0; i < cards.Length; ++i) {
                var card = cards[i];
                if (card.rank == 1) {
                    aces.Add(card);
                } else {
                    score = score + GetCardScore(card);
                }
            }

            // Add aces.
            if (aces.Count > 0) {
                int acesScore = aces.Count * 11;
                int acesLeft = aces.Count;
                while ((acesLeft > 0) && (acesScore + score) > 21) {
                    acesLeft = acesLeft - 1;
                    acesScore = acesScore - 10;
                }
                score = score + acesScore;
            }

            return score;
        }

        public bool IsBust()
        {
            return GetScore() > 21;
        }
    }
}