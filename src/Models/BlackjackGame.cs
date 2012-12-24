using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalrBlackjack.Models
{
    public class BlackjackGame
    {
        BlackjackHand dealerHand = new BlackjackHand();
        BlackjackHand playerHand = new BlackjackHand();
        string result = "None";
        PlayingCards cards = new PlayingCards();

        public BlackjackGame()
        {
        }

        public void NewGame()
        {
            dealerHand = new BlackjackHand();
            playerHand = new BlackjackHand();

            playerHand.AddCard(cards.DealNextCard());
            dealerHand.AddCard(cards.DealNextCard());
            playerHand.AddCard(cards.DealNextCard());

            result = "None";
        }

        public bool IsInProgress()
        {
            return (result == "None") && (dealerHand.HasCards());
        }

        public GameData ToGameData()
        {
            return new GameData {
                dealer = new Dealer {
                    cards = dealerHand.GetCards(),
                    score = dealerHand.GetScore()
                },
                player = new Player {
                    cards = playerHand.GetCards(),
                    score = playerHand.GetScore(),
                    balance = 102.50m
                },
                result = result
            };
        }

        public string GetResultForPlayer()
        {
            int score = this.playerHand.GetScore();
            if (score > 21) {
                return "Bust";
            }
            return "None";
        }

        public bool IsGameInProgress()
        {
            return result == "None";
        }

        public void Hit()
        {
            if (IsGameInProgress()) {
                playerHand.AddCard(cards.DealNextCard());
                result = GetResultForPlayer();
            }
        }

        public string GetResult()
        {
            int playerScore = playerHand.GetScore();
            int dealerScore = dealerHand.GetScore();

            if (playerHand.IsBust()) {
                return "Bust";
            } else if (dealerHand.IsBust()) {
                return "Win";
            }

            if (playerScore > dealerScore) {
                return "Win";
            } else if (playerScore == dealerScore) {
                return "Push";
            }
            return "Lose";
        }

        public void Stand()
        {
            if (IsGameInProgress()) {
                while (dealerHand.GetScore() < 17) {
                    dealerHand.AddCard(cards.DealNextCard());        
                }
                result = GetResult();
            }
        }
    }
}