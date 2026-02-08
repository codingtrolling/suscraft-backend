// 1. Connect to your Railway URL
const socket = io("https://suscraft-backend-production.up.railway.app");
const chat = document.getElementById('chat');

socket.on("connect", () => {
    chat.innerHTML = "Connected to SusCraft Server!";
    console.log("Joined with ID:", socket.id);
});

// 2. Setup Three.js Scene (The 3D World)
const scene = new THREE.Scene();
const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
const renderer = new THREE.WebGLRenderer();
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

// 3. Create YOUR Player (A simple Box for now)
const geometry = new THREE.BoxGeometry(1, 1, 1);
const material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
const player = new THREE.Mesh(geometry, material);
scene.add(player);
camera.position.z = 5;

// 4. Send movement to Railway
window.addEventListener('keydown', (e) => {
    if(e.key === "w") player.position.y += 0.1;
    if(e.key === "s") player.position.y -= 0.1;
    
    // Broadcast your new position to everyone else via Railway
    socket.emit("move", { x: player.position.x, y: player.position.y });
});

// 5. Listen for OTHER players moving
socket.on("playerMoved", (data) => {
    console.log("Another player moved:", data);
    // Logic to update/create other player boxes would go here
});

function animate() {
    requestAnimationFrame(animate);
    renderer.render(scene, camera);
}
animate();
      
