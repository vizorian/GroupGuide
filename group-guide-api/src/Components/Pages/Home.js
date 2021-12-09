import React from "react";
import { Image, Container } from "react-bootstrap";
import homeImage from "../Images/homeImage.jpg";

const Home = () => {
  return (
    <Container style={{ height: "9000" }}>
      <Image src={homeImage} fluid />
    </Container>
  );
};

export default Home;
