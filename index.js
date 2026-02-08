const express = require('express');
const http = require('http');
const { Server } = require('socket.io');

const app = express();
const server = http.createServer(app);
const io = new Server(server, {
    cors: { origin: "*" } // Allows your Android App to connect
});

// Root route so you can check if it's alive in your browser
app.get('/', (req, res) => {
    res.send('CodingTrolling Hub Backend is LIVE');
});

io.on('connection', (socket) => {
    console.log('User joined:', socket.id);

    // Handle SusCraft Player Movement
    socket.on('move', (data) => {
        // Broadcast movement to all OTHER players
        socket.broadcast.emit('playerMoved', { id: socket.id, ...data });
    });

    // Handle Bozin AI or Global Chat
    socket.on('chatMessage', (msg) => {
        io.emit('message', msg);
    });

    socket.on('disconnect', () => {
        console.log('User left:', socket.id);
    });
});

// PORT MUST BE DYNAMIC FOR RENDER
const PORT = process.env.PORT || 3000;
server.listen(PORT, () => {
    console.log(`Server running on port ${PORT}`);
});
