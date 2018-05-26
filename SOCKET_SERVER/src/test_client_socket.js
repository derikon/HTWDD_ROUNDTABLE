const config = require('./config.json');
const io = require('socket.io-client');
const socket = io.connect('http://' + config.host + ':' + config.port + '/');


socket.on('connect', () => {
	console.log('socket connected');
});


socket.on('connect_failed', () => {
	console.log('failed to connect to socket');
});


socket.on('reconnect_attempt', () => {
	console.log('trying to reconnect');
});


socket.on('reconnect_attempt', () => {
	console.log('trying to reconnect');
});


socket.emit('trigger_lift', {});
