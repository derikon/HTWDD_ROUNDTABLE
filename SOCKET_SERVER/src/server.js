const srv_config = require("./config.json");
const socket_io = require("socket.io")({
	transports: ["websocket"]
});


// establish socket server listening on a given port
socket_io.attach(srv_config.port);


socket_io.on("connection", (socket) => {

	console.log(socket.id + " is connected");


	socket.on("disconnect", (reason) => {
		console.log(socket.id + " is disconnected");
	});


	socket.on("trigger_lift", (data) => {
		socket.broadcast.emit("lift", {});
		console.log("triggered lift");
	});


	socket.on("token_detected", (data) => {
		socket.broadcast.emit("lift", {});
		console.log("token detected: " + data.uid);
	});


	socket.on("ack", (data) => {
		console.log("client received trigger");
	});

});
