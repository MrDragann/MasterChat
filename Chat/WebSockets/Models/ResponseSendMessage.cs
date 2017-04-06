namespace Chat.WebSockets.Models
{


    /// <summary>
    /// Отправление сообщения в чат
    /// </summary>
    public class ResponseSendMessage : WebSocketBaseResponse
    {
        public ResponseSendMessage(object newMessage, string method)
            : base(method)
        {
            NewMessage = newMessage;
        }
        public dynamic NewMessage { get; set; }
    }
}