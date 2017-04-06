var socket, chatViewModel, chatSelector = '.chat-socket';
///Методы которые срабатывают при ответе от сервера
var ResponseActions = {
    //Пришло новое сообщение
    NewChatMessage: function (data) {
        console.log('NewChatMessage',data.NewMessage);
        $(chatSelector + ' .messages').append('<p>' + data.NewMessage.Date + ' ' + data.NewMessage.FromUserName + ' ' + data.NewMessage.Message + '</p>')
    },
    //Сообщение об ошибке
    ErrorMessage:function(data) {
        alert(data.NewMessage);
    },
    ConsoleMessage: function (data) {
        console.log('ChatSocketMessage:',data.NewMessage);
    }
}
//Методы для запросов на сервер
var Requests = {
    //Отправить запрос
    SendRequest: function (data, callback) {
        console.log('SendRequest',data);
        var msg = JSON.stringify(data);
        Requests._waitForConnection(function () {
            socket.send(msg);
            if (typeof callback !== 'undefined') {
                callback();
            }
        }, 1000);
    },
    //Ожидание подключения
    _waitForConnection : function (callback, interval) {
        if (socket.readyState === 1) {
            callback();
        } else {
            setTimeout(function () {
                Requests._waitForConnection(callback, interval);
            }, interval);
        }
    },
    //Отправить соообщение в чат
    SendMessage: function (message, fromUser) {
        Requests.SendRequest({
            MethodName: "SendMessage",
            Message: message,
            Date: new Date(),
            FromUserName: fromUser
        });
    }
}
//knockout модель чата
var ChatViewModel = function() {
    var self = this;
    //Новое сообщение
    self.GetMessage = function () {
        var value = $(chatSelector + ' input[name=text]').val();
        return value;
    };

    self.GetUserName = function () {
        var value = $(chatSelector + ' input[name=user]').val();
        return value;
    };
    //Отправить сообщение
    self.SendMessage = function () {
        var message = self.GetMessage();
        var user = self.GetUserName();
        if (message && user) {
            
            Requests.SendMessage(message, user);
            $(chatSelector + ' input[name=text]').val('');
            //self.NewMessage('');
        }
        
    }
}
chatViewModel = new ChatViewModel();
$(function () {
    if (typeof (WebSocket) !== 'undefined') {
        socket = new WebSocket('ws://localhost:3325/WebSockets/ChatHandler.ashx');
    } else {
        socket = new MozWebSocket('ws://localhost:3325/WebSockets/ChatHandler.ashx');
    }
    socket.onmessage = function (msg) {  
        var data = JSON.parse(msg.data);
        console.log('socket.onmessage', data);
        if (ResponseActions[data.Method] && typeof ResponseActions[data.Method] === 'function')
            ResponseActions[data.Method](data);
    };
    socket.onclose = function (event) {
        alert('Пожалуйста, обновите страницу');
    };
    $(chatSelector + ' button').click(function (e) {
        chatViewModel.SendMessage();
        //if (e.keyCode === 13) {

        //    if (!e.shiftKey) {
        //        chatViewModel.SendMessage();
        //        return false;
        //    }
        //}
    });
    
})