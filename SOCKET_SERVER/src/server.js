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
			case "discussion":
				console.log("received topic: " + data.topic);
				console.log("received: " + JSON.stringify(data));
				socket.broadcast.emit("discussion", data);
			break;
			case "new_discussion":
				console.log("received topic: " + data.topic);
				console.log("received: " + JSON.stringify(data));
				socket.broadcast.emit("new_discussion", data);
			break;
			case "start_discussion":
				console.log("received topic: " + data.topic);
				console.log("received: " + JSON.stringify(data));
				socket.broadcast.emit("start_discussion", data);
			break;
			case "end_discussion":
				console.log("received topic: " + data.topic);
				console.log("received: " + JSON.stringify(data));
				socket.broadcast.emit("end_discussion", data);
			break;
			case "remaining_time":
				console.log("received topic: " + data.topic);
				console.log("received: " + JSON.stringify(data));
				socket.broadcast.emit("remaining_time", data);
			break;
			case "start_pause":
				console.log("received topic: " + data.topic);
				console.log("received: " + JSON.stringify(data));
				socket.broadcast.emit("start_pause", data);
			break;
			case "end_pause":
				console.log("received topic: " + data.topic);
				console.log("received: " + JSON.stringify(data));
				socket.broadcast.emit("end_pause", data);
			break;
			case "topic":
				console.log("received topic: " + data.topic);
				console.log("received: " + JSON.stringify(data));
				socket.broadcast.emit("topic", data);
			break;
			case "silence":
				console.log("received topic: " + data.topic);
				console.log("received: " + JSON.stringify(data));
				socket.broadcast.emit("silence", data);
			break;
			default:
				console.log("received unknown topic: " + data.topic);
				console.log("received: " + JSON.stringify(data));
		}
	});
});
