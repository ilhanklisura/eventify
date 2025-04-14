<?php

/**
 * @OA\Get(
 *     path="/tickets",
 *     summary="Get all tickets",
 *     tags={"Tickets"},
 *     @OA\Response(response=200, description="List of tickets")
 * )
 */
Flight::route('GET /tickets', function () {
    Flight::json(Flight::ticket_service()->get_all());
});

/**
 * @OA\Get(
 *     path="/tickets/{id}",
 *     summary="Get ticket by ID",
 *     tags={"Tickets"},
 *     @OA\Parameter(
 *         name="id",
 *         in="path",
 *         required=true,
 *         @OA\Schema(type="integer")
 *     ),
 *     @OA\Response(response=200, description="Ticket found")
 * )
 */
Flight::route('GET /tickets/@id', function ($id) {
    Flight::json(Flight::ticket_service()->get_by_id($id));
});

/**
 * @OA\Post(
 *     path="/tickets",
 *     summary="Create new ticket",
 *     tags={"Tickets"},
 *     @OA\RequestBody(
 *         @OA\JsonContent(
 *             required={"event_id", "user_id", "price"},
 *             @OA\Property(property="event_id", type="integer"),
 *             @OA\Property(property="user_id", type="integer"),
 *             @OA\Property(property="price", type="number"),
 *             @OA\Property(property="status", type="string")
 *         )
 *     ),
 *     @OA\Response(response=200, description="Ticket created")
 * )
 */
Flight::route('POST /tickets', function () {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Ticket added successfully",
        "data" => Flight::ticket_service()->add($data)
    ]);
});

/**
 * @OA\Put(
 *     path="/tickets/{id}",
 *     summary="Update ticket",
 *     tags={"Tickets"},
 *     @OA\Parameter(
 *         name="id",
 *         in="path",
 *         required=true,
 *         @OA\Schema(type="integer")
 *     ),
 *     @OA\RequestBody(
 *         @OA\JsonContent(
 *             @OA\Property(property="price", type="number"),
 *             @OA\Property(property="status", type="string")
 *         )
 *     ),
 *     @OA\Response(response=200, description="Ticket updated")
 * )
 */
Flight::route('PUT /tickets/@id', function ($id) {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Ticket updated successfully",
        "data" => Flight::ticket_service()->update($data, $id)
    ]);
});

/**
 * @OA\Delete(
 *     path="/tickets/{id}",
 *     summary="Delete ticket",
 *     tags={"Tickets"},
 *     @OA\Parameter(
 *         name="id",
 *         in="path",
 *         required=true,
 *         @OA\Schema(type="integer")
 *     ),
 *     @OA\Response(response=200, description="Ticket deleted")
 * )
 */
Flight::route('DELETE /tickets/@id', function ($id) {
    Flight::ticket_service()->delete($id);
    Flight::json(["message" => "Ticket deleted successfully"]);
});