<?php

$register_payload = json_encode([
    'name' => 'Demo User',
    'email' => 'demo@gmail.com',
    'password' => 'demo1234'
]);

$curl = curl_init();

curl_setopt_array($curl, [
    CURLOPT_URL => "http://localhost/eventify/backend/auth/register",
    CURLOPT_RETURNTRANSFER => true,
    CURLOPT_POST => true,
    CURLOPT_POSTFIELDS => $register_payload,
    CURLOPT_HTTPHEADER => [
        "Content-Type: application/json"
    ]
]);

$response = curl_exec($curl);
$http_code = curl_getinfo($curl, CURLINFO_HTTP_CODE);

curl_close($curl);

echo "HTTP status: $http_code\n";
echo "Response:\n";
echo $response;
