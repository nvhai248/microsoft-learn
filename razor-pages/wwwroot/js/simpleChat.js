console.log("Realtime Chat JS Loaded");

let connection = null;

// Hàm render danh sách user online
function renderUserList(users) {
  let list = document.getElementById("userList");
  list.innerHTML = "";
  users.forEach((u) => {
    let li = document.createElement("li");
    li.textContent = u;
    list.appendChild(li);
  });
}

// Khi nhấn Connect
document.getElementById("connectBtn").addEventListener("click", async () => {
  let username = document.getElementById("usernameInput").value.trim();
  if (!username) {
    alert("Please enter a username");
    return;
  }

  connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub?username=" + encodeURIComponent(username))
    .build();

  // Nhận tin nhắn mới
  connection.on("ReceiveNewMessage", (user, message) => {
    let p = document.createElement("p");
    p.innerHTML = `<strong>${user}:</strong> ${message}`;
    document.getElementById("newMessages").appendChild(p);
  });

  // Khi có user mới online
  connection.on("UserConnected", (username, users) => {
    let p = document.createElement("p");
    p.innerHTML = `<em>${username} joined the chat</em>`;
    document.getElementById("newMessages").appendChild(p);

    renderUserList(users);
  });

  // Khi có user offline
  connection.on("UserDisconnected", (username, users) => {
    let p = document.createElement("p");
    p.innerHTML = `<em>${username} left the chat</em>`;
    document.getElementById("newMessages").appendChild(p);

    renderUserList(users);
  });

  // Nhận toàn bộ danh sách user khi join
  connection.on("OnlineUsers", (users) => {
    renderUserList(users);
  });

  try {
    await connection.start();
    console.log("Connected to SignalR");

    // Enable nút gửi
    document.getElementById("send").disabled = false;

    // Lấy danh sách user đang online
    connection.invoke("GetOnlineUsers");
  } catch (err) {
    console.error(err.toString());
  }
});

// Gửi tin nhắn
document.getElementById("send").addEventListener("click", (event) => {
  let message = document.getElementById("messageInput").value.trim();
  if (message && connection) {
    connection.invoke("PostMessage", message);
    document.getElementById("messageInput").value = "";
  }
  event.preventDefault();
});
