import React from "react";
import useAxios from "axios-hooks";
import { Form, Button } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";

export default function GameCreate({ token }) {
  const { register, handleSubmit } = useForm();
  const navigate = useNavigate();

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

    alert(`Game ${data.name} has been added.`);
    navigate("/games");
  };

  if (loading) {
    return <></>;
  }

  return (
    <Form onSubmit={handleSubmit(onSubmit)}>
      <Form.Group className="mb-3">
        <Form.Label htmlFor="name">Name</Form.Label>
        <Form.Control
          required
          type="text"
          placeholder="Enter campaign name"
          {...register("name")}
        />
      </Form.Group>

      <Form.Group className="mb-3">
        <Form.Label htmlFor="description">Description</Form.Label>
        <Form.Control
          required
          type="text"
          placeholder="Enter campaign description"
          {...register("description")}
        />
      </Form.Group>

      <Button variant="primary" type="submit">
        Create
      </Button>
    </Form>
  );
}
