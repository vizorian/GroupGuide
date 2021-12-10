import React from "react";
import useAxios from "axios-hooks";
import { useParams } from "react-router";
import GameDelete from "./GameDelete";
import GameUpdate from "./GameUpdate";
import CampaignCreate from "../Campaigns/CampaignCreate";
import CampaignGetAll from "../Campaigns/CampaignGetAll";
import jwt_decode from "jwt-decode";
import { Container, Dropdown, Row, Col, DropdownButton } from "react-bootstrap";

export default function GameGet({ token }) {
  const { id } = useParams();

  // Get game
  const [{ data: game, loading: gameLoading, error: gameError }, manualGet] = useAxios(
    {
      url: `http://localhost:5000/api/games/${id}`,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    { useCache: false }
  );

  if (gameLoading) {
    return <></>;
  }

  if (token == null) {
    return (
      <Container>
        <Row>
          <Col md="auto">
            <span className="fw-bold" style={{ fontSize: "60px" }}>
              {game.name}
            </span>
          </Col>
          <Dropdown.Divider />
        </Row>
        <Row>
          <Col />
          <Col md="auto"></Col>
        </Row>
        <Row>
          <Col xxl={8} md="auto">
            <h4>{game.description}</h4>
          </Col>
          <Col />
          <Dropdown.Divider />
        </Row>
        <Row>
          <Col md="auto">
            <span className="fw-bold" style={{ fontSize: "50px" }}>
              Campaigns
            </span>
          </Col>
          <Col />
        </Row>
        <Row>
          <Col />
          <Col md="auto">
            <CampaignCreate token={token} gameId={game.id} />
          </Col>
          <Dropdown.Divider />
        </Row>

        <CampaignGetAll token={token} gameId={game.id} />
      </Container>
    );
  }

  if (token != null) {
    var decoded = jwt_decode(token);
    const userId = decoded["userId"];
    const userRole =
      decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    if (!userRole.includes("Admin")) {
      return (
        <Container>
          <Row>
            <Col md="auto">
              <span className="fw-bold" style={{ fontSize: "60px" }}>
                {game.name}
              </span>
            </Col>
            <Dropdown.Divider />
          </Row>
          <Row>
            <Col />
            <Col md="auto"></Col>
          </Row>
          <Row>
            <Col xxl={8} md="auto">
              <h4>{game.description}</h4>
            </Col>
            <Col />
            <Dropdown.Divider />
          </Row>
          <Row>
            <Col md="auto">
              <span className="fw-bold" style={{ fontSize: "50px" }}>
                Campaigns
              </span>
            </Col>
            <Col />
          </Row>
          <Row>
            <Col />
            <Col md="auto">
              <CampaignCreate token={token} gameId={game.id} />
            </Col>
            <Dropdown.Divider />
          </Row>

          <CampaignGetAll gameId={game.id} />
        </Container>
      );
    }
  }

  return (
    <Container>
      <Row>
        <Col md="auto">
          <span className="fw-bold" style={{ fontSize: "60px" }}>
            {game.name}
          </span>
        </Col>
        <Dropdown.Divider />
      </Row>
      <Row>
        <Col />
        <Col md="auto">
          <DropdownButton
            title={
              <span className="buttonless" style={{ fontSize: "28px" }}>
                Actions
              </span>
            }
            variant="none"
          >
            <GameUpdate token={token} game={game} manualGet={manualGet}/>{" "}
            <GameDelete token={token} game={game} manualGet={manualGet}/>
          </DropdownButton>
        </Col>
      </Row>
      <Row>
        <Col xxl={8} md="auto">
          <h4>{game.description}</h4>
        </Col>
        <Col />
        <Dropdown.Divider />
      </Row>
      <Row>
        <Col md="auto">
          <span className="fw-bold" style={{ fontSize: "50px" }}>
            Campaigns
          </span>
        </Col>
        <Col />
      </Row>
      <Row>
        <Col />
        <Col md="auto">
          <CampaignCreate token={token} gameId={game.id} />
        </Col>
        <Dropdown.Divider />
      </Row>

      <CampaignGetAll gameId={game.id} />
    </Container>
  );
}
