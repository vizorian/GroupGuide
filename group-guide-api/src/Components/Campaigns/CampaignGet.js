import React from "react";
import useAxios from "axios-hooks";
import { useParams } from "react-router";
import CampaignDelete from "./CampaignDelete";
import CampaignUpdate from "./CampaignUpdate";
import SessionGetAll from "../Sessions/SessionGetAll";
import SessionCreate from "../Sessions/SessionCreate";
import jwt_decode from "jwt-decode";
import { Container, Dropdown, DropdownButton, Row, Col } from "react-bootstrap";

export default function CampaignGet({ token }) {
  const { gameId, id } = useParams();

  // Get campaign
  const [{ data, loading, error }, manualGet] = useAxios(
    {
      url: `http://localhost:5000/api/games/${gameId}/campaigns/${id}`,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    { useCache: false }
  );

  if (loading) {
    return <></>;
  }

  const owner = data.players[0];

  if (token === null) {
    return (
      <Container>
        <Col>
          <Row>
            <Col md="auto">
              <span className="fw-bold" style={{ fontSize: "100px" }}>
                {data.name}
              </span>
            </Col>
          </Row>
          <Row>
            <Col />
            <Col md="auto">
              <p>Created by: {owner.userName}</p>
            </Col>
          </Row>
          <Row>
            <Col md="auto">
              <h4>{data.description}</h4>
            </Col>
          </Row>
        </Col>
      </Container>
    );
  }

  auth: if (token != null) {
    var decoded = jwt_decode(token);
    const userId = decoded["userId"];
    const userRole =
      decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    if (userRole.includes("Admin") || owner == userId) {
      break auth;
    }

    if (!data.players.filter((e) => e.id === userId) > 0) {
      return (
        <Container>
          <Col>
            <Row>
              <Col md="auto">
                <span className="fw-bold" style={{ fontSize: "100px" }}>
                  {data.name}
                </span>
              </Col>
            </Row>
            <Row>
              <Col />
              <Col md="auto">
                <p>Created by: {owner.userName}</p>
              </Col>
            </Row>
            <Row>
              <Col md="auto">
                <h4>{data.description}</h4>
              </Col>
            </Row>
          </Col>
        </Container>
      );
    }

    if (data.players.filter((e) => e.id === userId) > 0) {
      return (
        <Container>
          <Col>
            <Row>
              <Col md="auto">
                <span className="fw-bold" style={{ fontSize: "100px" }}>
                  {data.name}
                </span>
              </Col>
            </Row>
            <Row>
              <Col />
              <Col md="auto">
                <p>Created by: {owner.userName}</p>
              </Col>
            </Row>
            <Dropdown.Divider />
            <Row>
              <Col />
              <Col md="auto"></Col>
            </Row>
            <Row>
              <Col md="auto">
                <SessionGetAll
                  token={token}
                  gameId={gameId}
                  campaignId={data.id}
                />
              </Col>
            </Row>
            <Dropdown.Divider />
            <Row>
              <Col md="auto">
                <h4>{data.description}</h4>
              </Col>
            </Row>
          </Col>
        </Container>
      );
    }
  }

  return (
    <Container>
      <Col>
        <Row>
          <Col md="auto">
            <span className="fw-bold" style={{ fontSize: "100px" }}>
              {data.name}
            </span>
          </Col>
        </Row>
        <Row>
          <Col />
          <Col md="auto">
            <p>Created by: {owner.userName}</p>
          </Col>
        </Row>
        <Dropdown.Divider />
        <Row>
          <Col />
          <Col md="auto">
            <DropdownButton
              title={
                <span className="buttonless" style={{ fontSize: "28px" }}>
                  Campaign settings
                </span>
              }
              variant="none"
            >
              <SessionCreate
                token={token}
                gameId={gameId}
                campaignId={data.id}
                manualGet={manualGet}
              />
              <CampaignUpdate
                token={token}
                gameId={gameId}
                campaign={data}
                manualGet={manualGet}
              />
              <CampaignDelete
                token={token}
                gameId={gameId}
                campaign={data}
                manualGet={manualGet}
              />
            </DropdownButton>
          </Col>
        </Row>
        <Row>
          <Col md="auto">
            <SessionGetAll token={token} gameId={gameId} campaignId={data.id} />
          </Col>
        </Row>
        <Dropdown.Divider />
        <Row>
          <Col md="auto">
            <h4>{data.description}</h4>
          </Col>
        </Row>
      </Col>
    </Container>
  );
}
