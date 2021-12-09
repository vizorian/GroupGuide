import React, { useState } from "react";
import Time from "react-time-format";
import "./Test.css";
import { Button, Modal, Container, Row, Col } from "react-bootstrap";
import SessionDelete from "./SessionDelete";
import "../../Styles/ModalStyle.css";

// gets all sessions for a specific campaign
export default function SessionGetAllExpired({
  token,
  expiredSessions,
  gameId,
  campaignId,
}) {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  return (
    <>
      <h3 className="buttonless" onClick={handleShow}>
        View old sessions
      </h3>

      <Modal show={show} onHide={handleClose} centered>
        <Modal.Header closeButton>
          <Modal.Title>Old sessions</Modal.Title>
        </Modal.Header>
        <Modal.Body class="custom-modal-body">
          {expiredSessions &&
            expiredSessions.map((session) => (
              <Container key={session.id}>
                <Row>
                  <Col md="auto">
                    <h4>{session.name}</h4>
                    <p>
                      Started{" "}
                      <Time
                        value={session.startingTime}
                        format="YYYY-MM-DD at hh:mm"
                      />
                    </p>
                  </Col>
                  <Col>
                    <SessionDelete
                      token={token}
                      gameId={gameId}
                      campaignId={campaignId}
                      session={session}
                    />
                  </Col>
                </Row>
              </Container>
            ))}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

<h3>View old sessions</h3>;
