<?php

$token = getenv("EVENTIFY_TEST_JWT");

$curl = curl_init();

curl_setopt_array($curl, [
    CURLOPT_URL => "https://squid-app-lnxkv.ondigitalocean.app/categories",
    CURLOPT_RETURNTRANSFER => true,
    CURLOPT_HTTPHEADER => [
        "Content-Type: application/json",
        "Authorization: Bearer $token"
    ]
]);

$response = curl_exec($curl);
$http_code = curl_getinfo($curl, CURLINFO_HTTP_CODE);

curl_close($curl);

echo "HTTP status: $http_code\n";
echo "Response:\n";
echo $response;

if ($http_code !== 200) {
    exit(1);
}
