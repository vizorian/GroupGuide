import React from "react";
import useAxios from "axios-hooks";
import { Button, Modal, Dropdown } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import jwt_decode from "jwt-decode";

export default function GameDelete({ token, game }) {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

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
    navigate("/games");
  };

  if (token != null) {
    var decoded = jwt_decode(token);
    const userId = decoded["userId"];
    const userRole =
      decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    if (!userRole.includes("Admin")) {
      return <></>;
    }
  } else {
    return <></>;
  }

  return (
    <>
      <Dropdown.Item variant="none" onClick={handleShow}>
        Delete
      </Dropdown.Item>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Delete confirmation</Modal.Title>
        </Modal.Header>
        <Modal.Body>Do you wish to delete game {game.name}?</Modal.Body>
        <Modal.Footer>
          <Button variant="danger" onClick={(handleClose, handleDelete)}>
            Delete
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}
