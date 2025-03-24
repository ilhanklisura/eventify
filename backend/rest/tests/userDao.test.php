<?php
require_once __DIR__ . '/../dao/UserDao.php';
$userDao = new UserDao();

echo "<strong>✅ ADD User</strong><br>";
$user = $userDao->add([
    'name' => 'Organizer',
    'email' => 'organizer@example.com',
    'password' => password_hash('password123', PASSWORD_DEFAULT),
    'role' => 'organizer'
]);
print_r($user);
echo "<br><br>";

echo "<strong>🔍 GET ALL Users</strong><br>";
print_r($userDao->get_all());
echo "<br><br>";

echo "<strong>🔍 GET User BY ID</strong><br>";
print_r($userDao->get_by_id($user['id']));
echo "<br><br>";

$user['name'] = 'Updated User';
echo "<strong>✅ UPDATE User</strong><br>";
print_r($userDao->update($user, $user['id']));
echo "<br><br>";

echo "<strong>❌ DELETE User</strong><br>";
$userDao->delete($user['id']);
print_r($userDao->get_all());
echo "<br><br>";
