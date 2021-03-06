import React from "react";
import { useLocation, useNavigate, Link } from "react-router-dom";
import { Navbar, Container, Nav, NavDropdown, Dropdown } from "react-bootstrap";
import jwt_decode from "jwt-decode";
import "../../Styles/NavbarStyle.css";

export default function NavigationBar({ token }) {
  const location = useLocation();

  const navigate = useNavigate();

  const logoutUser = () => {
    sessionStorage.clear();
    navigate("home");
    window.location.reload();
  };

  if (location.pathname === "/login") {
    return <></>;
  }
  if (location.pathname === "/register") {
    return <></>;
  }

  var username = null;
  if (token != null) {
    var decoded = jwt_decode(token);
    username =
      decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
  } else {
    return (
      <Container>
        <Navbar expand="lg">
          <Container>
            <Navbar.Brand>Group Guide</Navbar.Brand>
            <Navbar.Toggle />
            <Navbar.Collapse>
              <Nav className="me-auto">
                <Link
                  className="nav-link a-custom"
                  to="home"
                  style={{ fontSize: "18px" }}
                >
                  Home
                </Link>
                <Link
                  className="nav-link a-custom"
                  to="games"
                  style={{ fontSize: "18px" }}
                >
                  Games
                </Link>
                <Link
                  className="nav-link a-custom"
                  to="/register"
                  style={{ fontSize: "18px" }}
                >
                  Register
                </Link>
              </Nav>
              <NavDropdown
                title={
                  <span className="me-auto" style={{ fontSize: "18px" }}>
                    Dashboard
                  </span>
                }
              >
                <Link className="nav-link a-custom" to="/login">
                  Login
                </Link>
                <Dropdown.Divider />
                <Link className="nav-link a-custom" to="/register">
                  New around here? Sign up
                </Link>
              </NavDropdown>
            </Navbar.Collapse>
          </Container>
        </Navbar>
      </Container>
    );
  }

  return (
    <Container>
      <Navbar expand="lg">
        <Container>
          <Navbar.Brand>Group Guide</Navbar.Brand>
          <Navbar.Toggle />
          <Navbar.Collapse>
            <Nav className="me-auto">
              <Link
                className="nav-link a-custom"
                to="home"
                style={{ fontSize: "18px" }}
              >
                Home
              </Link>
              <Link
                className="nav-link a-custom"
                to="games"
                style={{ fontSize: "18px" }}
              >
                Games
              </Link>
            </Nav>
            <NavDropdown
              title={
                <span className="me-auto" style={{ fontSize: "18px" }}>
                  {username}
                </span>
              }
            >
              <NavDropdown.Item onClick={logoutUser}>
                Sign Out{" "}
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="18"
                  height="18"
                  fill="currentColor"
                  class="bi bi-box-arrow-right"
                  viewBox="0 0 16 16"
                >
                  <path
                    fill-rule="evenodd"
                    d="M10 12.5a.5.5 0 0 1-.5.5h-8a.5.5 0 0 1-.5-.5v-9a.5.5 0 0 1 .5-.5h8a.5.5 0 0 1 .5.5v2a.5.5 0 0 0 1 0v-2A1.5 1.5 0 0 0 9.5 2h-8A1.5 1.5 0 0 0 0 3.5v9A1.5 1.5 0 0 0 1.5 14h8a1.5 1.5 0 0 0 1.5-1.5v-2a.5.5 0 0 0-1 0v2z"
                  />
                  <path
                    fill-rule="evenodd"
                    d="M15.854 8.354a.5.5 0 0 0 0-.708l-3-3a.5.5 0 0 0-.708.708L14.293 7.5H5.5a.5.5 0 0 0 0 1h8.793l-2.147 2.146a.5.5 0 0 0 .708.708l3-3z"
                  />
                </svg>
              </NavDropdown.Item>
            </NavDropdown>
          </Navbar.Collapse>
        </Container>
      </Navbar>
    </Container>
  );
}
