const srv_config = require("./config.json");
const socket_io = require("socket.io")({
	transports: ["websocket"]
});


socket_io.attach(srv_config.port);


socket_io.on("connection", (socket) => {

	console.log(socket.id + " is connected");


	socket.on("disconnect", (reason) => {
		console.log(socket.id + " is disconnected");
	});


	socket.on("message", (data) => {
		switch (data.topic) {
			// [TODO] add topics to handle
			case "token":
				console.log("received topic: " + data.topic);
				console.log("received: " + JSON.stringify(data));
				socket.broadcast.emit("token", data);
			break;
			default:
				console.log("received unknown topic: " + data.topic);
				console.log("received: " + JSON.stringify(data));
		}
	});
});
