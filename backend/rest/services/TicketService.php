<?php
require_once 'BaseService.php';
require_once __DIR__ . '/../dao/TicketDao.php';

class TicketService extends BaseService {
    public function __construct() {
        parent::__construct(new TicketDao());
    }

    public function add($entity) {
        if (empty($entity['event_id']) || empty($entity['user_id']) || empty($entity['price'])) {
            Flight::halt(400, "event_id, user_id, and price are required.");
        }

        if (!is_numeric($entity['price']) || $entity['price'] < 0) {
            Flight::halt(400, "Price must be a non-negative number.");
        }

        $entity['status'] = $entity['status'] ?? 'Available';
        return parent::add($entity);
    }

    public function update($entity, $id, $id_column = "id") {
        if (!empty($entity['price']) && (!is_numeric($entity['price']) || $entity['price'] < 0)) {
            Flight::halt(400, "Price must be a non-negative number.");
        }

        return parent::update($entity, $id, $id_column);
    }
}
