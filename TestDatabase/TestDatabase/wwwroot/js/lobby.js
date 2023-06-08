
var connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();

connection.on("UserConnected", function (connectionId) {
    updatePlayerList(connectionId, "Open");
    document.getElementById("connected-players").textContent++;

});

connection.on("UserDisconnected", function (connectionId) {
    updatePlayerList(connectionId, "Open");
    document.getElementById("connected-players").textContent--;
});

connection.on("RedirectToUrl", function (url) {
    window.location.href = url;
});

connection.start().then(function () {
    connection.invoke("GetConnectedClients").then(function (connectedClients) {
        console.log("Connected clients:", connectedClients);
        document.getElementById("connected-players").textContent = connectedClients.length;
        connectedClients.forEach(function (connectionId) {
            updatePlayerList(connectionId, connectionId);
        });
    }).catch(function (err) {
        console.error(err.toString());
    });
}).catch(function (err) {
    console.error(err.toString());
});

function updatePlayerList(connectionId, newText) {
    toastr.success("New user connected: " + connectionId);


    var playersUl = document.getElementById("players");
    console.log(playersUl);
    var playerLiElements = playersUl.getElementsByTagName("li");

    for (var i = 0; i < playerLiElements.length; i++) {
        if (playerLiElements[i].textContent.trim() === "Open") {
            playerLiElements[i].textContent = connectionId;
            break;
        }
    }
}

document.getElementById("start-button").addEventListener("click", function () {
    redirectAllUsers();
});

function redirectAllUsers() {
    // Redirect all connected users to the new page
    var url = "https://localhost:7098/Main/Wedstrijd?ID=5";
    connection.invoke("RedirectUsers", url).then(function () {
        console.log("Redirect command sent to server.");
        window.location.href = url; // Redirect the current user immediately
    }).catch(function (err) {
        console.error(err.toString());
    });
}