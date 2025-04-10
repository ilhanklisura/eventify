<?php
require_once __DIR__ . '/../../dao/BookingsDao.php';

$dao = new BookingsDao();

echo "<strong>✅ ADD Booking</strong><br>";
$booking = $dao->add([
    'user_id' => 3,
    'event_id' => 1
]);

print_r($booking);
echo "<br><br>";

echo "<strong>🔍 GET ALL Bookings</strong><br>";
print_r($dao->get_all());
echo "<br><br>";

echo "<strong>🔍 GET Booking BY ID</strong><br>";
print_r($dao->get_by_id($booking['id']));
echo "<br><br>";

echo "<strong>❌ DELETE Booking</strong><br>";
$dao->delete($booking['id']);
print_r($dao->get_all());
echo "<br><br>";