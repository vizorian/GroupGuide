import React from "react";
import useAxios from "axios-hooks";
import { Button, Modal, Dropdown } from "react-bootstrap";
import { useState } from "react";

export default function SessionDelete({
  token,
  gameId,
  campaignId,
  session,
  manualGet,
}) {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  // Delete game
  const [{ error, response }, doDelete] = useAxios(
    {
      method: `DELETE`,
      url: `http://localhost:5000/api/games/${gameId}/campaigns/${campaignId}/sessions/${session.id}`,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    { manual: true }
  );

  if (response) {
    manualGet();
  }

  return (
    <>
      <Dropdown.Item variant="none" onClick={handleShow}>
        Delete
      </Dropdown.Item>

      <Modal show={show} onHide={handleClose} centered>
        <Modal.Header closeButton>
          <Modal.Title>Delete confirmation</Modal.Title>
        </Modal.Header>
        <Modal.Body>Do you wish to delete session {session.name}?</Modal.Body>
        <Modal.Footer>
          <Button variant="danger" onClick={(handleClose, doDelete)}>
            Delete
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}
