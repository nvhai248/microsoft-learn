// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
console.log("Test JS");

var connection = new signalR.HubConnectionBuilder()
  .withUrl("/messageHub")
  .build();

connection.on("ReceiveNewMEssage", (message) => {
  let p = document.createElement("p");
  p.textContent = message;
  document.getElementById("newMessages").appendChild(p);
});

connection.start();
document.getElementById("send").addEventListener("click", (e) => {
  let message = document.getElementById("messageInput").value;
  connection.invoke("PostMessage", message);
  event.preventDefault();
});
