import React from "react";
import useAxios from "axios-hooks";
import Time from "react-time-format";
import SessionGetAllExpired from "./SessionGetAllExpired";
import { Dropdown, Container, Row, Col, DropdownButton } from "react-bootstrap";
import SessionDelete from "./SessionDelete";
import SessionUpdate from "./SessionUpdate";

function dynamicSort(property) {
  var sortOrder = 1;
  if (property[0] === "-") {
    sortOrder = -1;
    property = property.substr(1);
  }
  return function (a, b) {
    var result =
      a[property] < b[property] ? -1 : a[property] > b[property] ? 1 : 0;
    return result * sortOrder;
  };
}

// gets all sessions for a specific campaign
export default function SessionGetAll({ token, gameId, campaignId }) {
  const [{ data, loading, error }] = useAxios(
    {
      url: `http://localhost:5000/api/games/${gameId}/campaigns/${campaignId}/sessions`,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    { useCache: false }
  );

  if (loading) {
    return <></>;
  }

  data.sort(dynamicSort("startingTime"));

  const today = new Date();
  const upcomingSessions = data.filter(function (obj) {
    return new Date(obj.startingTime) > today;
  });

  const expiredSessions = data.filter(function (obj) {
    return new Date(obj.startingTime) < today;
  });

  return (
    <div>
      <SessionGetAllExpired
        token={token}
        expiredSessions={expiredSessions}
        gameId={gameId}
        campaignId={campaignId}
      />
      <Dropdown.Divider />
      <h3 className="fw-bold">Upcoming sessions</h3>

      {upcomingSessions &&
        upcomingSessions.map((session) => (
          <Container key={session.id}>
            <Row>
              <Col md="auto">
                <h4>{session.name}</h4>
                <p>
                  Starts{" "}
                  <Time
                    value={session.startingTime}
                    format="YYYY-MM-DD at hh:mm"
                  />
                </p>
              </Col>
              <Col />
              <Col md="auto">
                <DropdownButton
                  title={
                    <span className="buttonless" style={{ fontSize: "18px" }}>
                      Actions
                    </span>
                  }
                  variant="none"
                >
                  <SessionUpdate
                    token={token}
                    gameId={gameId}
                    campaignId={campaignId}
                    session={session}
                  />
                  <SessionDelete
                    token={token}
                    gameId={gameId}
                    campaignId={campaignId}
                    session={session}
                  />
                </DropdownButton>
              </Col>
            </Row>
          </Container>
        ))}
    </div>
  );
}
