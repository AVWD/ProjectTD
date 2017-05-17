'use strict';

// ------------------------------------------------------------------
// Dependencies
// ------------------------------------------------------------------
const net = require('net') // network sockets
    ,shortid = require('shortid') // user id server side
    ,cron = require('cron') // sched server messages / cleanup etc
    ,moment = require('moment') // dates
    ,_ = require('underscore') // collection utilities
    ,config = require('./app/conf');

// ------------------------------------------------------------------
// Sockets
// ------------------------------------------------------------------
var clients = [];

var server = net.Server(function(socket) {
	// Mimicking what I've used in socket.io previously
	// Not very well thought out currently
	socket.broadcast = function(event, data){
		for(var i in clients) {
			clients[i].write(JSON.stringify({ evt: event, dat: data, uid:socket.uid }));
		}
	};
	socket.emit = function(event, data){
		socket.write(JSON.stringify({ evt: event, dat: data, uid:socket.uid }));
	}
	// --------------------------------------------------
	socket.uid = shortid.generate();
	clients.push(socket);
	console.log('connection established', socket.uid);
	socket.broadcast('spawn', {});
	console.log('notifying other players', socket.uid);
	// --------------------------------------------------

	socket.on('end', function(){
		console.log('disconnected', socket.uid);
		socket.broadcast('end', socket);
		
		var idx = clients.indexOf(client);
		clients.splice(idx, 1);
	});

	socket.on('data', function(msg){
		console.log("received data", socket.uid, msg);
		// echo
		socket.broadcast('data', msg);
	});

	socket.emit('chat', 'Hello World!');
});

// ------------------------------------------------------------------
// Start Server
// ------------------------------------------------------------------
server.listen(config.port);
console.log("Server started. Listening on port: " + config.port);

process.on('SIGINT', function(){
	for (var i in clients) {
		clients[i].write("server shutdown");
		clients[i].destroy();
	}

	console.log('SIGINT TERM (ctrl+c)');
	process.exit(0);	
});