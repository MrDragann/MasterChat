using System;
using Newtonsoft.Json;

namespace Chat.WebSockets.Models.UserSocket
{
    /// <summary>
    /// Пользователь 
    /// </summary>
    public class SocketUserBase : IDisposable
    {

        private WebSocketSocketUser _webSocket;

        /// <summary>
        /// Web socket для подключения 
        /// </summary>
        public WebSocketSocketUser WebSocket
        {
            get { return _webSocket; }
            set
            {

                if (value != null)
                {
                    _webSocket = value;
                    _webSocket.OnCloseWebScoket += OnClose;
                    _webSocket.OnErrorWebScoket += OnError;
                    _webSocket.OnOpenWebScoket += OnOpen;
                    _webSocket.OnMessageWebScoket += OnMessage;
                }
                else
                {
                    if (_webSocket == null) return;
                    _webSocket.OnCloseWebScoket -= OnClose;
                    _webSocket.OnErrorWebScoket -= OnError;
                    _webSocket.OnOpenWebScoket -= OnOpen;
                    _webSocket.OnMessageWebScoket -= OnMessage;
                    _webSocket = null;
                }
                
            }
        }

        #region Конструкторы

        public SocketUserBase(WebSocketSocketUser webSocket)
        {
            LastActivityDate = DateTime.UtcNow;
            WebSocket = webSocket;            
        }
        /// <summary>
        /// Открытие соединения
        /// </summary>
        public virtual void OnOpen()
        {
            
        }
        /// <summary>
        /// Получение сообщения от клиента
        /// </summary>
        /// <param name="message"></param>
        public virtual void OnMessage(string message)
        {
            
        }
        /// <summary>
        /// Закрытие сообщения
        /// </summary>
        public virtual void OnClose()
        {

        }
        /// <summary>
        /// Сообщение об ошибки
        /// </summary>
        public virtual void OnError()
        {
            
        }

        

        #endregion Конструкторы


        #region Информация

        #endregion Информация

        

        #region Отправка сообщений

        /// <summary>
        /// Отправить сообщение
        /// </summary>
        public void Send(object data)
        {
            if (WebSocket != null)
                WebSocket.Send(JsonConvert.SerializeObject(data));
        }
        /// <summary>
        /// Отправить сообщение об ошибке
        /// </summary>
        /// <param name="message"></param>
        public void SendErrorMessage(string message)
        {
            Send(new ResponseSendMessage(message, "ErrorMessage"));
        }

        /// <summary>
        /// Отправить сообщение в консоль
        /// </summary>
        /// <param name="message"></param>
        public void SendConsoleMessage(string message)
        {
            Send(new ResponseSendMessage(message, "ConsoleMessage"));
        }

        #endregion Отправка сообщений


        #region Свойства


        

        private DateTime _lastActivityDate;
        /// <summary>
        /// Дата последней активности
        /// </summary>
        public DateTime LastActivityDate
        {
            get { return _lastActivityDate; }
            set
            {
                _lastActivityDate = value;
            }
        }


        #endregion Свойства




        public virtual void Dispose()
        {
            WebSocket.Close();
        }
    }
}