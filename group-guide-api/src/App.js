import React from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";
// import axios from "axios";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Home from "./Pages/Home";
import Navbar from "./Components/Navbar/NavigationBar";
import Welcome from "./Pages/Welcome";

const api = axios.create({
  baseURL: "http://localhost:5000/api/",
});

function App() {
  return (
    <Router>
      <Navbar />

      <Routes>
        <Route path="/" element={<Welcome />} />
        <Route path="/home" element={<Home />} />
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
