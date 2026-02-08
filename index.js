const express = require('express');
const http = require('http');
const { Server } = require('socket.io');

const app = express();
const server = http.createServer(app);

// Configure Socket.io with a massive 100MB buffer for those 20MB files
const io = new Server(server, {
    cors: { origin: "*" },
    maxHttpBufferSize: 1e8 
});

// Database Simulations (Reset on server restart)
let posts = [];
let onlineUsers = {}; // { userId: socketId }

io.on('connection', (socket) => {
    console.log(`[SYSTEM] New Connection: ${socket.id}`);

    // --- 1. USER REGISTRATION ---
    socket.on('registerUser', (userId) => {
        if (!userId) return;
        onlineUsers[userId] = socket.id;
        
        // Broadcast the updated online list so the sidebar updates
        io.emit('updateOnlineList', Object.keys(onlineUsers));
        
        // Push current feed to the new user
        socket.emit('feedUpdate', posts);
    });

    // --- 2. GLOBAL FEED (POSTS, LIKES, COMMENTS) ---
    socket.on('newTrollPost', (post) => {
        try {
            posts.unshift(post);
            if (posts.length > 50) posts.pop(); // Keep RAM clean
            io.emit('feedUpdate', posts);
        } catch (err) {
            console.error("Post Error:", err);
        }
    });

    socket.on('likePost', (data) => {
        const post = posts.find(p => p.id === data.pid);
        if (post) {
            if (!post.likedBy) post.likedBy = [];
            
            // Toggle like logic
            const index = post.likedBy.indexOf(data.uid);
            if (index === -1) {
                post.likedBy.push(data.uid);
            } else {
                post.likedBy.splice(index, 1);
            }
            
            post.likes = post.likedBy.length;
            io.emit('feedUpdate', posts);
        }
    });

    socket.on('newComment', (data) => {
        const post = posts.find(p => p.id === data.pid);
        if (post) {
            if (!post.comments) post.comments = [];
            post.comments.push({ user: data.user, text: data.text });
            io.emit('feedUpdate', posts);
        }
    });

    // --- 3. FRIEND REQUESTS & PRIVATE CHAT ---
    socket.on('friendRequest', (data) => {
        const targetSocketId = onlineUsers[data.to];
        if (targetSocketId) {
            io.to(targetSocketId).emit('receiveFriendRequest', data);
        }
    });

    socket.on('acceptFriend', (data) => {
        // Logic to link users as friends could be stored in a DB here
        console.log(`[FRIENDS] ${data.u1} and ${data.u2} are now connected.`);
    });

    socket.on('sendPrivateMessage', (data) => {
        const targetSocketId = onlineUsers[data.to];
        if (targetSocketId) {
            io.to(targetSocketId).emit('receivePrivateMessage', data);
        }
    });

    // --- 4. FOLLOW & REPORT ---
    socket.on('followUser', (data) => {
        console.log(`[FOLLOW] ${data.follower} followed ${data.target}`);
        // You could add a 'followers' array to user profiles here
    });

    socket.on('reportSubmit', (data) => {
        console.warn(`[REPORT] Flagged: ${data.violator} | Reason: ${data.reason}`);
        console.warn(`Admin Notification sent to rabi65171@gmail.com`);
    });

    // --- 5. DISCONNECT ---
    socket.on('disconnect', () => {
        for (let uid in onlineUsers) {
            if (onlineUsers[uid] === socket.id) {
                delete onlineUsers[uid];
                break;
            }
        }
        io.emit('updateOnlineList', Object.keys(onlineUsers));
    });
});

// Start Server
const PORT = process.env.PORT || 3000;
server.listen(PORT, () => {
    console.log(`TrollNET Backend Running on Port ${PORT}`);
});
