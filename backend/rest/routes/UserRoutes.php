<?php

/**
 * @OA\Get(
 *     path="/users",
 *     summary="Get all users",
 *     tags={"Users"},
 *     @OA\Response(response=200, description="List of users")
 * )
 */
Flight::route('GET /users', function () {
    Flight::json(Flight::user_service()->get_all());
});

/**
 * @OA\Get(
 *     path="/users/{id}",
 *     summary="Get user by ID",
 *     tags={"Users"},
 *     @OA\Parameter(
 *         name="id",
 *         in="path",
 *         required=true,
 *         @OA\Schema(type="integer")
 *     ),
 *     @OA\Response(response=200, description="User found")
 * )
 */
Flight::route('GET /users/@id', function ($id) {
    Flight::json(Flight::user_service()->get_by_id($id));
});

/**
 * @OA\Post(
 *     path="/users",
 *     summary="Create new user",
 *     tags={"Users"},
 *     @OA\RequestBody(
 *         @OA\JsonContent(
 *             required={"name", "email", "password", "role"},
 *             @OA\Property(property="name", type="string"),
 *             @OA\Property(property="email", type="string"),
 *             @OA\Property(property="password", type="string"),
 *             @OA\Property(property="role", type="string")
 *         )
 *     ),
 *     @OA\Response(response=200, description="User created")
 * )
 */
Flight::route('POST /users', function () {
    $data = Flight::request()->data->getData();
    Flight::json([
        'message' => 'User created successfully',
        'data' => Flight::user_service()->add($data)
    ]);
});

/**
 * @OA\Put(
 *     path="/users/{id}",
 *     summary="Update user",
 *     tags={"Users"},
 *     @OA\Parameter(
 *         name="id",
 *         in="path",
 *         required=true,
 *         @OA\Schema(type="integer")
 *     ),
 *     @OA\RequestBody(
 *         @OA\JsonContent(
 *             @OA\Property(property="name", type="string"),
 *             @OA\Property(property="email", type="string"),
 *             @OA\Property(property="password", type="string"),
 *             @OA\Property(property="role", type="string")
 *         )
 *     ),
 *     @OA\Response(response=200, description="User updated")
 * )
 */
Flight::route('PUT /users/@id', function ($id) {
    $data = Flight::request()->data->getData();
    Flight::json([
        'message' => 'User updated successfully',
        'data' => Flight::user_service()->update($data, $id)
    ]);
});

/**
 * @OA\Delete(
 *     path="/users/{id}",
 *     summary="Delete user",
 *     tags={"Users"},
 *     @OA\Parameter(
 *         name="id",
 *         in="path",
 *         required=true,
 *         @OA\Schema(type="integer")
 *     ),
 *     @OA\Response(response=200, description="User deleted")
 * )
 */
Flight::route('DELETE /users/@id', function ($id) {
    Flight::user_service()->delete($id);
    Flight::json(['message' => 'User deleted successfully']);
});