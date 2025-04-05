<?php
require_once __DIR__ . '/../../vendor/autoload.php';
require_once 'BaseService.php';
require_once __DIR__ . '/../dao/BookingsDao.php';

class BookingService extends BaseService {
    public function __construct() {
        parent::__construct(new BookingsDao());
    }

    public function add($entity) {
        if (empty($entity['user_id']) || empty($entity['event_id'])) {
            Flight::halt(400, "user_id and event_id are required.");
        }

        // Use DAO method instead of directly calling query_unique
        $existing = $this->dao->get_by_user_and_event($entity['user_id'], $entity['event_id']);

        if ($existing) {
            Flight::halt(409, "You have already booked this event.");
        }

        return parent::add($entity);
    }
}
