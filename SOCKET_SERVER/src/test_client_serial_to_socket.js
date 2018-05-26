const config = require('./config.json');
const io = require('socket.io-client');
const socket = io.connect('http://' + config.host + ':' + config.port + '/');
const SerialPort = require("serialport");
const serial = new SerialPort(config.serial, {
   baudRate: config.baud,
   parser: new SerialPort.parsers.Readline("\n")

});

socket.on('connect', () => {
	console.log('socket connected');
});


socket.on('connect_failed', () => {
	console.log('failed to connect to socket');
});


socket.on('reconnect_attempt', () => {
	console.log('trying to reconnect');
});


socket.on('lift', (data) => {
	console.log('should lift');
});


//socket.emit('trigger_lift', {});


serial.on('open', function(err) {
    console.log('Serial Port open.');
    serial.on('data', function(data) {
		let uid = data.readUInt32BE(0);
        console.log("uid: " + uid);
		socket.emit('token_detected', {"uid":uid});
    });
    if (err) {
		console.log('Error when trying to open:' + err);
	}
});
