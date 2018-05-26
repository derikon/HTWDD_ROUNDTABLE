var serialport = require('serialport');

serialport.list(function (err, ports) {
  ports.forEach(function(port) {
    console.log(port.comName);
  });
});