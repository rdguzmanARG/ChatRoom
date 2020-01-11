"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var tr = document.createElement("tr");
    var td1 = document.createElement("td");
    td1.innerText = user;
    var td2 = document.createElement("td");
    td2.innerText = msg;
    var td3 = document.createElement("td");
    td3.innerText = "";
    tr.appendChild(td1);
    tr.appendChild(td2);
    tr.appendChild(td3);
    var list = document.getElementById("messagesList");
    if (list.children.length > 0) {
        list.insertBefore(tr, list.childNodes[0]);
    } else {
        list.appendChild(tr);
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    if (message === "")
        return;
    connection.invoke("SendMessage", user, message).then(function () {
        document.getElementById("messageInput").value = "";
    }).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});