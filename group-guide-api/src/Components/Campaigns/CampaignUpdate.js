import React from "react";
import useAxios from "axios-hooks";
import { Form, Button, Modal, Dropdown } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useState } from "react";

export default function CampaignUpdate({ token, gameId, campaign, manualGet }) {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const { register, handleSubmit } = useForm();

  const [{ data, loading, error, response }, doPut] = useAxios(
    {
      method: `PUT`,
      url: `http://localhost:5000/api/games/${gameId}/campaigns/${campaign.id}`,
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

  return (
    <>
      <Dropdown.Item onClick={handleShow}>Update</Dropdown.Item>

      <Modal show={show} onHide={handleClose} centered>
        <Modal.Header closeButton>
          <Modal.Title>Update campaign</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form id="updateForm" onSubmit={handleSubmit(onSubmit)}>
            <Form.Group className="mb-3">
              <Form.Label htmlFor="name">Name</Form.Label>
              <Form.Control
                required
                type="text"
                placeholder="Enter campaign name"
                defaultValue={campaign.name}
                {...register("name")}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label htmlFor="description">Description</Form.Label>
              <Form.Control
                required
                type="text"
                placeholder="Enter campaign description"
                defaultValue={campaign.description}
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
