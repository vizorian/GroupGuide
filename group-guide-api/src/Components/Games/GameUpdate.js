import React from "react";
import useAxios from "axios-hooks";
import { Form, Button, Modal, Dropdown } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useState } from "react";
import jwt_decode from "jwt-decode";

export default function GameUpdate({ token, game, manualGet }) {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const { register, handleSubmit } = useForm();

  const [{ data, loading, error, response }, doPut] = useAxios(
    {
      method: `PUT`,
      url: `http://localhost:5000/api/games/${game.id}`,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    { manual: true }
  );

  const onSubmit = (data) => {
    doPut({
      data: {
        name: data.name,
        description: data.description,
      },
    });

    handleClose();
  };

  if (loading) {
    return <></>;
  }

  if (response) {
    manualGet();
  }

  if (token != null) {
    var decoded = jwt_decode(token);
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
        Update
      </Dropdown.Item>

      <Modal show={show} onHide={handleClose} centered>
        <Modal.Header closeButton>
          <Modal.Title>Update game</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form id="updateForm" onSubmit={handleSubmit(onSubmit)}>
            <Form.Group className="mb-3">
              <Form.Label htmlFor="name">Name</Form.Label>
              <Form.Control
                required
                type="text"
                placeholder="Enter game name"
                defaultValue={game.name}
                {...register("name")}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label htmlFor="description">Description</Form.Label>
              <Form.Control
                required
                type="text"
                placeholder="Enter game description"
                defaultValue={game.description}
                {...register("description")}
              />
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="primary" type="submit" form="updateForm">
            Update
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}
