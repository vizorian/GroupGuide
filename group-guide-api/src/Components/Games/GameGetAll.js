import React from "react";
import useAxios from "axios-hooks";
import GameCreate from "./GameCreate";
import { Container, Dropdown, Row, Col, Button } from "react-bootstrap";
import { Link } from "react-router-dom";
export default function GameGetAll({ token }) {
  const [{ data, loading, error }] = useAxios(
    {
      url: "http://localhost:5000/api/games",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    { useCache: false }
  );

  if (loading) {
    return <></>;
  }

  return (
    <Container>
      <Row>
        <Col md="auto">
          <span className="fw-bold" style={{ fontSize: "100px" }}>
            Games
          </span>
        </Col>
        <Col />
      </Row>
      <Row>
        <Col />
        <Col md="auto">
          <GameCreate token={token} />
        </Col>
        <Dropdown.Divider />
      </Row>

      {data &&
        data.map((game) => (
          <>
            <Row key={game.id}>
              <Col>
                <Link className="a-custom" to={`${game.id}`}>
                  <span className="button-text" style={{ fontSize: "35px" }}>
                    {game.name}
                  </span>
                </Link>
              </Col>
            </Row>
            <Row>
              <Dropdown.Divider />
            </Row>
          </>
        ))}
    </Container>
  );
}
