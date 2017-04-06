using System;

namespace Chat.WebSockets.Models
{
    public class ViewModelMessageItem
    {
        public ViewModelMessageItem()
        {

        }

        public ViewModelMessageItem(string message, string fromUser, DateTime date)
        {
            
            Date = date;
            FromUserName = fromUser;
            Message = message;
        }
        public string Message { get; set; }

        public string FromUserName { get; set; }

        public DateTime Date { get; set; }


    }
}