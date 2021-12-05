import React from "react";
import "./App.css";
// import axios from "axios";
import Navbar from "./Components/Navbar/index.js";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Home from "./Pages";
import Sidebar from "./Components/Sidebar/index.js";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
      </Routes>
    </Router>
  );
}

// testing interactions
/*
const api = axios.create({
  baseURL: "http://localhost:5000/api/games/",
});

class App extends Component {
  state = {
    games: [],
  };

  constructor() {
    super();
    api.get("/").then((res) => {
      console.log(res.data);
      this.setState({ games: res.data });
    });
  }

  render() {
    return (
      <div>
        <header>Yo my man.</header>
        {this.state.games.map((game) => (
          <h2 key={game.id}>{game.name}</h2>
        ))}
      </div>
    );
  }
}
*/
export default App;
