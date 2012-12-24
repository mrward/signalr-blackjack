using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SignalrBlackjack.Models
{
    public class BlackjackPersistentConnection : PersistentConnection
    {
        public BlackjackPersistentConnection()
        {
        }

        protected override Task OnReceivedAsync(IRequest request, string connectionId, string data)
        {
            Debug.WriteLine("Message received: " + data);

            switch (data)
            {
                case "stand":
                    return Services.Server.Stand(this, connectionId);
                case "hit":
                    return Services.Server.Hit(this, connectionId);
                case "deal":
                    return Services.Server.Deal(this, connectionId);
                default:
                    return null;
            }
        }

        public Task Send(GameData gameData, string connectionId, string messageType)
        {
            var builder = new StringBuilder();
            var writer = new StringWriter(builder);
            base._jsonSerializer.Serialize(gameData, writer);
            string message = messageType + " " + builder.ToString();
            return Connection.Send(connectionId, message);
        }

        protected override Task OnConnectedAsync(IRequest request, string connectionId)
        {
            Debug.WriteLine("User connected");
            Services.Server.NewGame();
            return base.OnConnectedAsync(request, connectionId);
        }

        protected override Task OnDisconnectAsync(IRequest request, string connectionId)
        {
            Debug.WriteLine("User disconnected");
            return base.OnDisconnectAsync(request, connectionId);
        }
    }
}