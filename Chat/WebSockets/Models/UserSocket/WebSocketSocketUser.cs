using Microsoft.Web.WebSockets;

namespace Chat.WebSockets.Models.UserSocket
{
    public class WebSocketSocketUser:WebSocketHandler
    {
        
        public delegate void Handler();

        public delegate void MsgHandler(string msg);
        /// <summary>
        /// Событие открытия сокета
        /// </summary>
        public event Handler OnOpenWebScoket;
        /// <summary>
        /// События получения сообщения с клиента
        /// </summary>
        public event MsgHandler OnMessageWebScoket;
        /// <summary>
        /// Событие закрытия сокета
        /// </summary>
        public event Handler OnCloseWebScoket;
        /// <summary>
        /// Событие получения ошибки
        /// </summary>
        public event Handler OnErrorWebScoket;

        public override void OnOpen()
        {
            if (OnOpenWebScoket != null)
                OnOpenWebScoket();
        }

        public override void OnMessage(string message)
        {
            if (OnMessageWebScoket != null)
                OnMessageWebScoket(message);
        }

        public override void OnClose()
        {
            if (OnCloseWebScoket != null)
            {
                OnCloseWebScoket();
            }
        }

        public override void OnError()
        {
            if (OnErrorWebScoket != null)
                OnErrorWebScoket();
        }
    }
}