import React, { useState } from "react";
import { Form, Button, Nav } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { register } from "react-scroll/modules/mixins/scroller";

async function registerUser(credentials) {
  return fetch("http://localhost:5000/api/register", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(credentials),
  }).then((data) => data.json());
}

export default function Register() {
  const [username, setUsername] = useState();
  const [email, setEmail] = useState();
  const [password, setPassword] = useState();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const reply = await registerUser({
      username,
      email,
      password,
    });
    console.log(reply);

    // for auto login?
    // setToken(token);
    navigate("/login");
  };

  return (
    <Form onSubmit={handleSubmit}>
      <Form.Group className="mb-3" controlId="formBasicUsername">
        <Form.Label>Username</Form.Label>
        <Form.Control
          required
          type="username"
          placeholder="Enter username"
          onChange={(e) => setUsername(e.target.value)}
        />
        {/* <Form.Control.Feedback type="invalid">
          Please provide a valid username.
        </Form.Control.Feedback> */}
      </Form.Group>

      <Form.Group className="mb-3" controlId="formBasicEmail">
        <Form.Label>Email</Form.Label>
        <Form.Control
          required
          type="email"
          placeholder="Enter email"
          onChange={(e) => setEmail(e.target.value)}
        />
        {/* <Form.Control.Feedback type="invalid">
          Please provide a valid email address.
        </Form.Control.Feedback> */}
      </Form.Group>

      <Form.Group
        required
        className="mb-3"
        controlId="formBasicPassword"
        onChange={(e) => setPassword(e.target.value)}
      >
        <Form.Label>Password</Form.Label>
        <Form.Control type="password" placeholder="Enter password" />
        {/* <Form.Control.Feedback type="invalid">
          Please provide a valid password which containts: 1 uppercase symbol, 1
          lowercase symbol and 1 any symbol.
        </Form.Control.Feedback> */}
      </Form.Group>

      <Button variant="primary" type="submit">
        {/* <Nav.Link href="/home">Login</Nav.Link> */}
        Register
      </Button>
    </Form>
  );
}
