<?php
require_once __DIR__ . '/../../dao/TicketDao.php';
$ticketDao = new TicketDao();

echo "<strong>✅ ADD Ticket</strong><br>";
$ticket = $ticketDao->add([
    'event_id' => 1,
    'user_id' => 3,
    'price' => 20,
    'status' => 'Available'
]);
print_r($ticket);
echo "<br><br>";

echo "<strong>🔍 GET ALL Tickets</strong><br>";
print_r($ticketDao->get_all());
echo "<br><br>";

echo "<strong>🔍 GET Ticket BY ID</strong><br>";
print_r($ticketDao->get_by_id($ticket['id']));
echo "<br><br>";

$updated = $ticketDao->update([
    'event_id' => 1,
    'user_id' => 3,
    'price' => 20,
    'status' => 'Sold'
], $ticket['id']);
echo "<strong>✅ UPDATE Ticket</strong><br>";
print_r($updated);
echo "<br><br>";

echo "<strong>❌ DELETE Ticket</strong><br>";
$ticketDao->delete($ticket['id']);
print_r($ticketDao->get_all());
echo "<br><br>";