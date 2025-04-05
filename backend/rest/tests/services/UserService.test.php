<?php
require_once __DIR__ . '/../../services/UserService.php';

$service = new UserService();

// Add
echo "<b>✅ Adding User:</b><br>";
$user = $service->add([
    'name' => 'Service Test User',
    'email' => 'servicetestuser@example.com',
    'password' => password_hash('pass123', PASSWORD_DEFAULT),
    'role' => 'organizer'
]);
print_r($user);
echo "<br>";

// Get All
echo "<b>📦 Get All:</b><br>";
print_r($service->get_all());
echo "<br>";

// Get by ID
echo "<b>🔍 Get Users by ID:</b><br>";
print_r($service->get_by_id($user['id']));
echo "<br>";

// Update
echo "<b>✏️ Updating User:</b><br>";
$user['name'] = 'Updated Service User';
print_r($service->update($user, $user['id']));
echo "<br>";

// Delete
echo "<b>🗑️ Deleting:</b><br>";
$service->delete($user['id']);
print_r($service->get_all());
echo "<br>";
