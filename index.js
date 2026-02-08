const express = require('express');
const multer = require('multer');
const nodemailer = require('nodemailer');
const { OAuth2Client } = require('google-auth-library');
const client = new OAuth2Client("778775023056-dnrr0d6ujirr01cs0vclfd6gpgseh7kl.apps.googleusercontent.com");

const app = express();
// ... (your existing socket.io setup)

// --- UPLOAD CONFIG (20MB LIMIT) ---
const upload = multer({ 
    dest: 'uploads/', 
    limits: { fileSize: 20 * 1024 * 1024 } // 20MB
});

app.post('/upload', upload.single('media'), (req, res) => {
    res.json({ url: `/uploads/${req.file.filename}` });
});

// --- REPORT SYSTEM (NODEMAILER) ---
const transporter = nodemailer.createTransport({
    service: 'gmail',
    auth: { user: 'YOUR_EMAIL@gmail.com', pass: 'YOUR_APP_PASSWORD' }
});

socket.on('reportUser', (data) => {
    const mailOptions = {
        from: 'TrollNET System',
        to: 'rabi65171@gmail.com',
        subject: `REPORT: ${data.targetUser}`,
        text: `Reason: ${data.reason}\nReported by: ${data.reporter}`
    };
    transporter.sendMail(mailOptions);
});

// --- SEARCH & SUBSCRIBE ---
let posts = []; // Simple in-memory search
socket.on('search', (query) => {
    const results = posts.filter(p => p.text.includes(query));
    socket.emit('searchResults', results);
});
