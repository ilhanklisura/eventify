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


/**
 * @OA\Get(
 *     path="/dashboard/chart/users-per-day",
 *     summary="Get number of registered users per day (last 30 days)",
 *     tags={"Dashboard"},
 *     @OA\Response(
 *         response=200,
 *         description="User registrations per day",
 *         @OA\JsonContent(type="array",
 *             @OA\Items(
 *                 @OA\Property(property="date", type="string", example="2025-05-01"),
 *                 @OA\Property(property="count", type="integer", example=5)
 *             )
 *         )
 *     ),
 *     security={{"bearerAuth":{}}}
 * )
 */
Flight::route('GET /dashboard/chart/users-per-day', function () {
    $pdo = Flight::db();
    $stmt = $pdo->prepare("
        SELECT DATE(created_at) AS date, COUNT(*) AS count
        FROM users
        WHERE created_at >= CURDATE()
        GROUP BY DATE(created_at)
        ORDER BY DATE(created_at) ASC
    ");
    $stmt->execute();
    $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
    Flight::json($data);
});

/**
 * @OA\Get(
 *     path="/dashboard/chart/events-popular",
 *     summary="Get most popular events by registration count",
 *     tags={"Dashboard"},
 *     @OA\Response(
 *         response=200,
 *         description="Most popular events",
 *         @OA\JsonContent(type="array",
 *             @OA\Items(
 *                 @OA\Property(property="title", type="string", example="Concert - Dragana Mirković"),
 *                 @OA\Property(property="count", type="integer", example=150)
 *             )
 *         )
 *     ),
 *     security={{"bearerAuth":{}}}
 * )
 */
Flight::route('GET /dashboard/chart/events-popular', function () {
    $pdo = Flight::db();
    $stmt = $pdo->prepare("
    SELECT e.title, COUNT(t.id) AS count
    FROM events e
    LEFT JOIN tickets t ON e.id = t.event_id AND LOWER(t.status) = 'sold'
    GROUP BY e.id
    ORDER BY count DESC
    LIMIT 6
");
    $stmt->execute();
    $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
    Flight::json($data);
});
