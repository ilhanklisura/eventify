<?php
require_once __DIR__ . '/../dao/EventDao.php';
$eventDao = new EventDao();

echo "<strong>✅ ADD Event</strong><br>";
$event = $eventDao->add([
    'title' => 'Oslobađanje bosanske krajine (nezavršena operacija)',
    'description' => 'Održava se u muzeju sa slikovitim prikazom boraca ARBiH i oslobađanju bosanske krajine.',
    'date' => date('Y-m-d H:i:s'),
    'category_id' => 6,
    'venue_id' => 1,
    'organizer_id' => 4
]);
print_r($event);
echo "<br><br>";

echo "<strong>🔍 GET ALL Events</strong><br>";
print_r($eventDao->get_all());
echo "<br><br>";

echo "<strong>🔍 GET Event BY ID</strong><br>";
print_r($eventDao->get_by_id($event['id']));
echo "<br><br>";

$event['title'] = 'Updated Event';
echo "<strong>✅ UPDATE Event</strong><br>";
print_r($eventDao->update($event, $event['id']));
echo "<br><br>";

echo "<strong>❌ DELETE Event</strong><br>";
$eventDao->delete($event['id']);
print_r($eventDao->get_all());
echo "<br><br>";