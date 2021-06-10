"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatRooms").build();
document.getElementById("sendButton").disabled = true;

connection.on("RecieveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} says ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("NewUser", { ConnectionId: connection.connectionId, UserName: document.getElementById("userInput").value }).catch(function (err) {
        return console.err(err.toString());
    })
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("UserJoined", function (userDetails) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${userDetails.userName} joined the chat`;
});

connection.on("UserDisconnected", function (user) {
    debugger;
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} left the chat`;
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});