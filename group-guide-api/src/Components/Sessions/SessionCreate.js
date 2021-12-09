import React from "react";
import useAxios from "axios-hooks";
import { Form, Button, Modal, Dropdown } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useState } from "react";

export default function SessionCreate({ token, gameId, campaignId }) {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const { register, handleSubmit } = useForm();

  const [{ data, loading, error }, doPost] = useAxios(
    {
      method: `POST`,
      url: `http://localhost:5000/api/games/${gameId}/campaigns/${campaignId}/sessions`,
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
        startingTime: data.startingTime,
      },
    });

    handleClose();
    window.location.reload();
  };

  if (loading) {
    return <></>;
  }

  return (
    <>
      <Dropdown.Item variant="none" onClick={handleShow}>
        Create session
      </Dropdown.Item>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Create</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form id="createForm" onSubmit={handleSubmit(onSubmit)}>
            <Form.Group className="mb-3">
              <Form.Label htmlFor="name">Name</Form.Label>
              <Form.Control
                required
                type="text"
                placeholder="Enter session name"
                {...register("name")}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label htmlFor="startingTime">Starting Time</Form.Label>
              <Form.Control
                required
                type="date"
                placeholder="Enter session starting time"
                {...register("startingTime")}
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
