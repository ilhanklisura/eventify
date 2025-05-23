<?php

// 1. Login to get JWT token
$login_payload = json_encode([
    'email' => 'demo@gmail.com',
    'password' => 'demo1234'
]);

$login = curl_init();
curl_setopt_array($login, [
    CURLOPT_URL => "http://localhost/eventify/backend/auth/login",
    CURLOPT_RETURNTRANSFER => true,
    CURLOPT_POST => true,
    CURLOPT_POSTFIELDS => $login_payload,
    CURLOPT_HTTPHEADER => [
        "Content-Type: application/json"
    ]
]);

$login_response = curl_exec($login);
curl_close($login);

$decoded = json_decode($login_response, true);
$token = $decoded['data']['token'] ?? null;

if (!$token) {
    echo "Login failed. Response: \n";
    print_r($login_response);
    exit;
}

echo "✅ Login successful. Token:\n$token\n\n";

// 2. Use token to call a protected route
$curl = curl_init();
curl_setopt_array($curl, [
    CURLOPT_URL => "http://localhost/eventify/backend/users",
    CURLOPT_RETURNTRANSFER => true,
    CURLOPT_HTTPHEADER => [
        "Authentication: $token",
        "Accept: application/json"
    ]
]);

$response = curl_exec($curl);
curl_close($curl);

echo "✅ Protected Route Response:\n";
echo $response;
