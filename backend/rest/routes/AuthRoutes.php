<?php

use Firebase\JWT\JWT;

/**
 * @OA\Post(
 *     path="/auth/register",
 *     summary="Register new user",
 *     tags={"Auth"},
 *     @OA\RequestBody(
 *         required=true,
 *         @OA\JsonContent(
 *             required={"name","email","password"},
 *             @OA\Property(property="name", type="string", example="Ilhan Klisura"),
 *             @OA\Property(property="email", type="string", example="ilhan@example.com"),
 *             @OA\Property(property="password", type="string", example="securepassword")
 *         )
 *     ),
 *     @OA\Response(response=200, description="User registered successfully"),
 *     @OA\Response(response=409, description="Email already exists")
 * )
 */
Flight::route('POST /auth/register', function () {
    $data = Flight::request()->data->getData();

    $response = Flight::auth_service()->register($data);

    if ($response['success']) {
        Flight::json([
            'message' => 'User registered successfully',
            'data' => $response['data']
        ]);
    } else {
        Flight::halt(409, $response['error']);
    }
});


/**
 * @OA\Post(
 *     path="/auth/login",
 *     summary="Login user",
 *     tags={"Auth"},
 *     @OA\RequestBody(
 *         required=true,
 *         @OA\JsonContent(
 *             required={"email","password"},
 *             @OA\Property(property="email", type="string", example="ilhan@example.com"),
 *             @OA\Property(property="password", type="string", example="securepassword")
 *         )
 *     ),
 *     @OA\Response(response=200, description="Login successful with JWT token"),
 *     @OA\Response(response=401, description="Invalid credentials")
 * )
 */
Flight::route('POST /auth/login', function () {
    $data = Flight::request()->data->getData();

    $response = Flight::auth_service()->login($data);

    if ($response['success']) {
        Flight::json([
            'message' => 'Login successful',
            'data' => $response['data']
        ]);
    } else {
        Flight::halt(401, $response['error']);
    }
});
