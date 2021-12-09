import React from "react";
import useAxios from "axios-hooks";
import { Form, Button, Modal } from "react-bootstrap";
import { useForm } from "react-hook-form";
import jwt_decode from "jwt-decode";
import { useState } from "react";

export default function GameCreate({ token }) {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const { register, handleSubmit } = useForm();

  const [{ data, loading, error }, doPost] = useAxios(
    {
      method: `POST`,
      url: `http://localhost:5000/api/games`,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    { manual: true }
  );

  const onSubmit = (data) => {
    doPost({
      data: {
        name: data.name,
        description: data.description,
      },
    });

    handleClose();
    window.location.reload();
  };

  if (loading) {
    return <></>;
  }

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
      <h4 className="buttonless" onClick={handleShow}>
        Create game
      </h4>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Create game</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form id="createForm" onSubmit={handleSubmit(onSubmit)}>
            <Form.Group className="mb-3">
              <Form.Label htmlFor="name">Name</Form.Label>
              <Form.Control
                required
                type="text"
                placeholder="Enter game name"
                {...register("name")}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label htmlFor="description">Description</Form.Label>
              <Form.Control
                required
                type="text"
                placeholder="Enter game description"
                {...register("description")}
              />
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="primary" type="submit" form="createForm">
            Create
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}
