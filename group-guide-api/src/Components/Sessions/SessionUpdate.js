import React from "react";
import useAxios from "axios-hooks";
import { Form, Button, Modal, Dropdown } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function SessionUpdate({
  token,
  gameId,
  campaignId,
  session,
  manualGet,
}) {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const { register, handleSubmit } = useForm();

  const navigate = useNavigate();

  const [{ data, loading, error, response }, doPut] = useAxios(
    {
      method: `PUT`,
      url: `http://localhost:5000/api/games/${gameId}/campaigns/${campaignId}/sessions/${session.id}`,
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
        startingTime: data.startingTime,
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

  return (
    <>
      <Dropdown.Item onClick={handleShow}>Update</Dropdown.Item>

      <Modal show={show} onHide={handleClose} centered>
        <Modal.Header closeButton>
          <Modal.Title>Update session</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form id="updateForm" onSubmit={handleSubmit(onSubmit)}>
            <Form.Group className="mb-3">
              <Form.Label htmlFor="name">Name</Form.Label>
              <Form.Control
                required
                type="text"
                placeholder="Enter session name"
                defaultValue={session.name}
                {...register("name")}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label htmlFor="startingTime">Starting Time</Form.Label>
              <Form.Control
                required
                type="date"
                placeholder="Enter session starting time"
                defaultValue={session.startingTime}
                {...register("startingTime")}
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
