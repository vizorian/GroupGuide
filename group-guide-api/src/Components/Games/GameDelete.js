import React from "react";
import useAxios from "axios-hooks";
import { Button, Modal } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { useState } from "react";

export default function GameDelete({ token, game }) {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const { register, handleSubmit } = useForm();
  const navigate = useNavigate();

  // Delete game
  const [{ error: deleteError }, doDelete] = useAxios(
    {
      method: `DELETE`,
      url: `http://localhost:5000/api/games/${game.id}`,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    { manual: true }
  );

  const handleDelete = () => {
    doDelete();
    alert(`Game ${game.name} has been deleted.`);
    navigate("/games");
  };

  return (
    <>
      <Button variant="danger" onClick={handleShow}>
        Delete
      </Button>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Delete confirmation</Modal.Title>
        </Modal.Header>
        <Modal.Body>Do you wish to delete game {game.name}?</Modal.Body>
        <Modal.Footer>
          <Button variant="none" onClick={handleClose}>
            Cancel
          </Button>
          <Button variant="danger" onClick={(handleClose, handleDelete)}>
            Delete
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}
