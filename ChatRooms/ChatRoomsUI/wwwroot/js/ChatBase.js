"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatRooms").build();
document.getElementById("sendButton").disabled = true;

connection.on("RecieveMessage", function (user, message) {
    var li = document.createElement("li");
    li.style = "color:white";
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} says ${message}`;
});

connection.on("UserJoined", function (userDetails) {
    var li = document.createElement("li");
    li.style = "color:white";
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${userDetails.userName} joined the chat`;
});

connection.on("UserDisconnected", function (user) {
    var li = document.createElement("li");
    li.style = "color:white";
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} left the chat`;
});


function OnSuccessValidation() {
    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
        connection.invoke("NewUser", { ConnectionId: connection.connectionId, UserName: document.getElementById("userInput").value, GroupName: document.getElementById("groupNameInput").value }).catch(function (err) {
            return console.error(err.toString());
        })
    }).catch(function (err) {
        return console.error(err.toString());
    });
};
