<?php
/**
 * @OA\Get(
 *     path="/dashboard/stats",
 *     summary="Get dashboard statistics",
 *     tags={"Dashboard"},
 *     @OA\Response(
 *         response=200,
 *         description="Dashboard statistics retrieved successfully",
 *         @OA\JsonContent(
 *             @OA\Property(property="totalEvents", type="integer", example=10),
 *             @OA\Property(property="totalTicketsSold", type="integer", example=100),
 *             @OA\Property(property="totalUsers", type="integer", example=50),
 *             @OA\Property(property="cancelledTickets", type="integer", example=5)
 *         )
 *     ),
 *     security={{"bearerAuth":{}}}
 * )
 */
Flight::route('GET /dashboard/stats', function () {
    $eventCount = count(Flight::event_service()->get_all());
    $userCount = count(Flight::user_service()->get_all());
    $tickets = Flight::ticket_service()->get_all();

    $totalTickets = count($tickets);
    $cancelledTickets = 0;
    foreach ($tickets as $t) {
        if (strtolower($t['status']) === 'cancelled') {
            $cancelledTickets++;
        }
    }

    Flight::json([
        'totalEvents' => $eventCount,
        'totalTicketsSold' => $totalTickets,
        'totalUsers' => $userCount,
        'cancelledTickets' => $cancelledTickets
    ]);
});