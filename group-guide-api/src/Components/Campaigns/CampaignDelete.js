import React from "react";
import useAxios from "axios-hooks";
import { Button, Modal, Dropdown } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { useState } from "react";

export default function CampaignDelete({ token, gameId, campaign }) {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const navigate = useNavigate();

  // Delete game
  const [{ error: deleteError }, doDelete] = useAxios(
    {
      method: `DELETE`,
      url: `http://localhost:5000/api/games/${gameId}/campaigns/${campaign.id}`,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    { manual: true }
  );

  const handleDelete = () => {
    doDelete();
    navigate(`/games/${gameId}/campaigns`);
  };

  return (
    <>
      <Dropdown.Item variant="none" onClick={handleShow}>
        Delete
      </Dropdown.Item>

      <Modal show={show} onHide={handleClose} centered>
        <Modal.Header closeButton>
          <Modal.Title>Delete confirmation</Modal.Title>
        </Modal.Header>
        <Modal.Body>Do you wish to delete campaign {campaign.name}?</Modal.Body>
        <Modal.Footer>
          <Button variant="danger" onClick={(handleClose, handleDelete)}>
            Delete
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}
