import React, { useState } from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
// import axios from "axios";
import {
  BrowserRouter as Router,
  Route,
  Routes,
  Navigate,
} from "react-router-dom";
import Navbar from "./Components/Navbar/NavigationBar";
import Home from "./Components/Pages/Home";
import Login from "./Components/Login/Login";
import useToken from "./Components/App/useToken";
import Register from "./Components/Register/Register";

import GameGetAll from "./Components/Games/GameGetAll";
import GameGet from "./Components/Games/GameGet";

import CampaignGetAll from "./Components/Campaigns/CampaignGetAll";
import CampaignGet from "./Components/Campaigns/CampaignGet";
import { Container, Row } from "react-bootstrap";

function App() {
  const { token, setToken } = useToken();

  return (
    <Router>
      <Container>
        <Row id="header">
          <Navbar token={token} setToken={setToken} />
        </Row>
        <Row id="content">
          <Routes>
            <Route path="/" element={<Navigate to="/login" />} />
            <Route path="/home" element={<Home />} />

            {/* User */}
            <Route path="/register" element={<Register />} />
            <Route path="/login" element={<Login setToken={setToken} />} />

            {/* Games */}
            <Route path="/games" element={<GameGetAll token={token} />} />
            <Route path="/games/:id" element={<GameGet token={token} />} />

            {/* Campaigns */}
            <Route
              path="/games/:gameId/campaigns"
              element={<CampaignGetAll token={token} />}
            />
            <Route
              path="/games/:gameId/campaigns/:id"
              element={<CampaignGet token={token} />}
            />
          </Routes>
        </Row>
        {/* <Row id="footer" style={footerStyle}>
          Bing chilling
        </Row> */}
        <Row></Row>
        <footer className="footer-custom">
          Vilius Birštonas IFF-8/10 | copyright LOLOLOL
        </footer>
      </Container>
    </Router>
  );
}

export default App;
