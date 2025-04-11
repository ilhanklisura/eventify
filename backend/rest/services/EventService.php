<?php
require_once 'BaseService.php';
require_once __DIR__ . '/../dao/EventDao.php';

class EventService extends BaseService {
    public function __construct() {
        parent::__construct(new EventDao());
    }

    public function add($entity) {
        $required = ['title', 'description', 'date', 'category_id', 'venue_id', 'organizer_id'];
        foreach ($required as $field) {
            if (empty($entity[$field])) {
                Flight::halt(400, "$field is required.");
            }
        }

        // Validate date format
        if (!strtotime($entity['date'])) {
            Flight::halt(400, "Invalid date format.");
        }

        return parent::add($entity);
    }
}
