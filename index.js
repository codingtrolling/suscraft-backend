const express = require('express');
const http = require('http');
const { Server } = require('socket.io');

const app = express();
const server = http.createServer(app);
const io = new Server(server, { 
    cors: { origin: "*" },
    maxHttpBufferSize: 1e8 // Allows 20MB+ uploads
});

let posts = []; 

// ALL socket code MUST be inside this block
io.on('connection', (socket) => {
    console.log('A user connected:', socket.id);
    
    // Send existing posts to the new user
    socket.emit('feedUpdate', posts);

    // Handle New Posts
    socket.on('newTrollPost', (data) => {
        posts.unshift(data);
        if (posts.length > 50) posts.pop(); 
        io.emit('feedUpdate', posts);
    });

    // FIX: Moving the report code INSIDE the connection block
    socket.on('reportSubmit', (data) => {
        console.log(`REPORT for rabi65171@gmail.com: ${data.violator}`);
        // Logic for email/banning goes here
    });

    socket.on('disconnect', () => {
        console.log('User disconnected');
    });
});

// Start the server
const PORT = process.env.PORT || 3000;
server.listen(PORT, () => {
    console.log(`Server running on port ${PORT}`);
});
