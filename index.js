const express = require('express');
const http = require('http');
const { Server } = require('socket.io');

// Initialization
const app = express();
const server = http.createServer(app);

// Configure Socket.io with 100MB buffer for large media
const io = new Server(server, {
    cors: {
        origin: "*", // Allows your mobile browser to connect
        methods: ["GET", "POST"]
    },
    maxHttpBufferSize: 1e8 
});

// Database Simulation (Reset on server restart)
let posts = [];
let onlineUsers = {}; // Maps UserID -> SocketID

io.on('connection', (socket) => {
    console.log(`>>> Connection established: ${socket.id}`);

    // 1. REGISTRATION (Friend List Logic)
    socket.on('registerUser', (userId) => {
        if (!userId) return;
        onlineUsers[userId] = socket.id;
        console.log(`User ${userId} registered at ${socket.id}`);
        
        // Broadcast updated online list to everyone
        io.emit('updateOnlineList', Object.keys(onlineUsers));
        
        // Send existing posts to the new user immediately
        socket.emit('feedUpdate', posts);
    });

    // 2. FEED SYSTEM (Global Posts)
    socket.on('newTrollPost', (data) => {
        try {
            // Memory Guard: Keep only the latest 50 posts
            posts.unshift(data);
            if (posts.length > 50) posts.pop();

            // Broadcast to the whole world
            io.emit('feedUpdate', posts);
            console.log(`New post from ${data.uName}`);
        } catch (err) {
            console.error("Post processing error:", err);
        }
    });

    // 3. PRIVATE CHAT & NOTIFICATIONS
    socket.on('sendPrivateMessage', (data) => {
        const targetSocketId = onlineUsers[data.toUserId];
        
        if (targetSocketId) {
            // Send specifically to the target user's phone
            io.to(targetSocketId).emit('receivePrivateMessage', {
                fromName: data.fromName,
                fromId: data.fromId,
                message: data.message
            });
            console.log(`Private DM: ${data.fromId} -> ${data.toUserId}`);
        } else {
            console.log(`Target user ${data.toUserId} is offline.`);
        }
    });

    // 4. REPORTING SYSTEM
    socket.on('reportSubmit', (data) => {
        console.warn(`[REPORT] Admin Alert: rabi65171@gmail.com`);
        console.warn(`Violator: ${data.violator} | Reason: ${data.reason}`);
        // In a real app, this would trigger an email via Nodemailer here
    });

    // 5. DISCONNECT LOGIC
    socket.on('disconnect', () => {
        // Find and remove user from online list
        for (let uid in onlineUsers) {
            if (onlineUsers[uid] === socket.id) {
                console.log(`<<< User ${uid} disconnected`);
                delete onlineUsers[uid];
                break;
            }
        }
        io.emit('updateOnlineList', Object.keys(onlineUsers));
    });
});

// Global Error Catching (Prevents the 'Railway Crash')
process.on('uncaughtException', (err) => {
    console.error('SYSTEM CRITICAL ERROR:', err);
});

// Port Binding for Railway
const PORT = process.env.PORT || 3000;
server.listen(PORT, () => {
    console.log(`
    ====================================
    TrollNET Backend is LIVE on Port ${PORT}
    Safe-Memory Mode: Enabled
    Max Buffer: 100MB
    ====================================
    `);
});
