<?php
require_once __DIR__ . '/../../services/TicketService.php';

$service = new TicketService();

echo "<b>➕ ADD Ticket</b><br>";
$item = $service->add([
    "event_id" => 1,
    "user_id" => 3,
    "price" => 49.99,
    "status" => "Available"
]);
print_r($item);
echo "<br>";

echo "<b>📥 GET ALL Tickets</b><br>";
print_r($service->get_all());
echo "<br>";

echo "<b>🔍 GET Ticket BY ID</b><br>";
print_r($service->get_by_id($item['id']));
echo "<br>";

echo "<b>✏️ UPDATE Ticket</b><br>";
$item = $service->update(array_merge($item, ["price" => 59.99, "status" => "Sold"]), $item['id']);
print_r($item);
echo "<br>";

echo "<b>❌ DELETE Ticket</b><br>";
$service->delete($item['id']);
print_r($service->get_all());
echo "<br>";
