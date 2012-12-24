using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace SignalrBlackjack.Models
{
    public class BlackjackServer
    {
        BlackjackGame game;

        public Task Deal(BlackjackPersistentConnection connection, string connectionId)
        {
            if (!game.IsInProgress())
            {
                game.NewGame();
            }
            return connection.Send(game.ToGameData(), connectionId, "deal");
        }

        public Task Hit(BlackjackPersistentConnection connection, string connectionId)
        {
            game.Hit();
            return connection.Send(game.ToGameData(), connectionId, "hit");
        }

        public Task Stand(BlackjackPersistentConnection connection, string connectionId)
        {
            game.Stand();
            return connection.Send(game.ToGameData(), connectionId, "stand");
        }

        public void NewGame()
        {
            game = new BlackjackGame();
        }
    }
}