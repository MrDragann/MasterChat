namespace Chat.WebSockets.Models
{
    /// <summary>
    /// Базовая модель ответа
    /// </summary>
    public class WebSocketBaseResponse
    {
        public WebSocketBaseResponse(string method)
        {
            Method = method;
        }
        /// <summary>
        /// Метод запускаемый на клиенте
        /// </summary>
        public string Method { get; set; }
    }
}