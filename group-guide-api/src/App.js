import React, { useState } from "react";
import axios from "axios";
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
import Welcome from "./Components/Pages/Welcome";
import Login from "./Components/Login/Login";
import useToken from "./Components/App/useToken";
import Register from "./Components/Register/Register";

import GamesGetAll from "./Components/Games/GameGetAll";
import GamesGet from "./Components/Games/GameGet";
import GamesCreate from "./Components/Games/GameCreate";
import GamesDelete from "./Components/Games/GameDelete";

function App() {
  const { token, setToken } = useToken();

  return (
    <Router>
      <Navbar token={token} setToken={setToken} />
      <Routes>
        <Route path="/" element={<Navigate to="/login" />} />
        <Route path="/home" element={<Home />} />

        {/* User */}
        <Route path="/register" element={<Register />} />
        <Route path="/login" element={<Login setToken={setToken} />} />

        {/* Games */}
        <Route path="/games" element={<GamesGetAll token={token} />} />
        <Route path="/games/:id" element={<GamesGet token={token} />} />
        <Route
          path="/games/:id/delete"
          element={<GamesDelete token={token} />}
        />
        <Route path="/games/create" element={<GamesCreate token={token} />} />
      </Routes>
    </Router>
  );
}

export default App;
