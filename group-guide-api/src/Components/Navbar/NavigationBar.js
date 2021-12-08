import React from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { Navbar, Container, Nav, NavDropdown, Dropdown } from "react-bootstrap";

export default function NavigationBar({ token }) {
  const location = useLocation();
  const navigate = useNavigate();

  const logoutUser = () => {
    sessionStorage.clear();
    window.location.reload(false);
  };

  if (location.pathname === "/login") {
    return <></>;
  }
  if (location.pathname === "/register") {
    return <></>;
  }
  if (token == null) {
    return (
      <>
        <Navbar bg="light" expand="lg">
          <Container>
            <Navbar.Brand href="/home">GG</Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
              <Nav className="me-auto">
                <Nav.Link href="/games">Games</Nav.Link>
                <Nav.Link href="/register">Register</Nav.Link>
              </Nav>
              <Nav>
                <NavDropdown title="Dashboard">
                  <NavDropdown.Item href="/login">Login</NavDropdown.Item>
                  <Dropdown.Divider />
                  <NavDropdown.Item href="/register">
                    New around here? Sign up
                  </NavDropdown.Item>
                </NavDropdown>
              </Nav>
            </Navbar.Collapse>
          </Container>
        </Navbar>
      </>
    );
  }
  return (
    <>
      <Navbar bg="light" expand="lg">
        <Container>
          <Navbar.Brand href="/home">GG</Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              <Nav.Link href="/games">Games</Nav.Link>
            </Nav>
            <Nav>
              <NavDropdown title="Dashboard">
                {/* <NavDropdown.Item href="/signout">Sign Out</NavDropdown.Item> */}
                <NavDropdown.Item onClick={logoutUser}>
                  Sign Out
                </NavDropdown.Item>
              </NavDropdown>
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
    </>
  );
}

// export default NavigationBar;
