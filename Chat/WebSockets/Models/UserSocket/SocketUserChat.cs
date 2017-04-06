using System;
using Newtonsoft.Json;

namespace Chat.WebSockets.Models.UserSocket
{
    /// <summary>
    /// Пользователь чата
    /// </summary>
    public class SocketUserChat :SocketUserBase
    {
        public SocketUserChat(WebSocketSocketUser webSocket)
            : base(webSocket)
        {
           
        }
        
        public override void OnClose()
        {
            ChatHandler.Users.Remove(this);
        }

        /// <summary>
        /// Пришло новое сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="fromUserName"></param>
        /// <param name="date"></param>
        public void ChatMessage(string message, string fromUserName, DateTime date)
        {
            var m = new ViewModelMessageItem
            {
                    Date = date,
                    FromUserName = fromUserName,
                    Message = message                    
            };
            Send(new ResponseSendMessage(m, "NewChatMessage"));
        }
        
        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void Speek(ViewModelMessageItem message)
        {
            foreach (var u in ChatHandler.Users)
            {
                u.ChatMessage(message.Message,message.FromUserName,message.Date);
            }
        }

        public override void OnMessage(string message)
        {
            var baseRequest = JsonConvert.DeserializeObject<BaseRequest>(message);
            switch (baseRequest.MethodName)
            {
                case "SendMessage": Speek(JsonConvert.DeserializeObject<ViewModelMessageItem>(message)); break;
                default: base.OnMessage(message); break;
            }
        }

    }

    public class BaseRequest
    {
        /// <summary>
        /// Название метода
        /// </summary>
        public string MethodName { get; set; }

    }
}