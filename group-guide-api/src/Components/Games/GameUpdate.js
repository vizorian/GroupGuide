import React from "react";
import useAxios from "axios-hooks";
import { Form, Button, Modal } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { useState } from "react";

export default function GameUpdate({ token, game }) {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const { register, handleSubmit } = useForm();
  const navigate = useNavigate();

  const [{ data, loading, error }, doPut] = useAxios(
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

    alert(`Game ${data.name} has been updated.`);
    navigate("/games");
  };

  if (loading) {
    return <></>;
  }

  return (
    <>
      <Button variant="primary" onClick={handleShow}>
        Update
      </Button>

      <Modal show={show} onHide={handleClose}>
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
                placeholder="Enter campaign name"
                defaultValue={game.name}
                {...register("name")}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label htmlFor="description">Description</Form.Label>
              <Form.Control
                required
                type="text"
                placeholder="Enter campaign description"
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
