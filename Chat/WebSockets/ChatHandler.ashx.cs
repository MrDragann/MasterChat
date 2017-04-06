using System;
using System.Collections.Generic;
using System.Web;
using Chat.WebSockets.Models.UserSocket;
using Microsoft.Web.WebSockets;

namespace Chat.WebSockets
{
    /// <summary>
    /// Сводное описание для ChatHandler
    /// </summary>
    public class ChatHandler : IHttpHandler
    {

        public static List<SocketUserChat> Users = new List<SocketUserChat>();
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.IsWebSocketRequest)
                {
                    var user = new SocketUserChat(new WebSocketSocketUser());
                    context.AcceptWebSocketRequest(user.WebSocket);
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}