import React, { useState } from "react";
import PropTypes from "prop-types";
import { Form, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

async function loginUser(credentials) {
  return fetch("http://localhost:5000/api/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(credentials),
  }).then((data) => data.json());
}

export default function Login({ setToken }) {
  const [username, setUsername] = useState();
  const [password, setPassword] = useState();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const token = await loginUser({
      username,
      password,
    });
    // console.log(token);
    setToken(token);
    navigate("/home");
  };

  return (
    <Form onSubmit={handleSubmit}>
      <Form.Group className="mb-3" controlId="formUsername">
        <Form.Label>Username</Form.Label>
        <Form.Control
          type="username"
          placeholder="Enter username"
          onChange={(e) => setUsername(e.target.value)}
        />
      </Form.Group>

      <Form.Group
        className="mb-3"
        controlId="formPassword"
        onChange={(e) => setPassword(e.target.value)}
      >
        <Form.Label>Password</Form.Label>
        <Form.Control type="password" placeholder="Enter password" />
      </Form.Group>

      <Button variant="primary" type="submit">
        {/* <Nav.Link href="/home">Login</Nav.Link> */}
        Login
      </Button>
    </Form>
  );
}

Login.propTypes = {
  setToken: PropTypes.func.isRequired,
};
