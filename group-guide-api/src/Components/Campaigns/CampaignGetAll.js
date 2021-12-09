import React from "react";
import useAxios from "axios-hooks";
import { Dropdown, Row, Col, Button } from "react-bootstrap";

// gets all campaigns for a specific game
export default function CampaignGetAll({ token, gameId }) {
  const [{ data, loading, error }] = useAxios(
    {
      url: `http://localhost:5000/api/games/${gameId}/campaigns`,
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
    <div>
      {data &&
        data.map((campaign) => (
          <>
            <Row key={campaign.id}>
              <Col>
                <Button
                  variant="none"
                  href={`${gameId}/campaigns/${campaign.id}`}
                >
                  <span className="button-text" style={{ fontSize: "35px" }}>
                    {campaign.name}
                  </span>
                </Button>
              </Col>
            </Row>
            <Row>
              <Dropdown.Divider />
            </Row>
          </>
        ))}
    </div>
  );
}
