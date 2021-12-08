import React from "react";
import useAxios from "axios-hooks";
import { useParams } from "react-router";
import GameDelete from "./GameDelete";
import GameUpdate from "./GameUpdate";

export default function GameGet({ token }) {
  const { id } = useParams();

  // Get game
  const [{ data: game, loading: gameLoading, error: gameError }] = useAxios(
    {
      url: `http://localhost:5000/api/games/${id}`,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    { useCache: false }
  );

  // Get campaign list
  const [
    { data: campaigns, loading: campaignsLoading, error: campaignsError },
  ] = useAxios(
    {
      url: `http://localhost:5000/api/games/${id}/campaigns`,
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
    { useCache: false }
  );

  if (gameLoading) {
    return <></>;
  }
  if (campaignsLoading) {
    return <></>;
  }

  console.log(game);

  return (
    <div>
      <h1>
        {game.id} - {game.name}
      </h1>

      <GameDelete token={token} game={game} />
      <GameUpdate token={token} game={game} />

      <h3>{game.description}</h3>
      {campaigns &&
        campaigns.map((campaign) => (
          <p>
            {campaign.id} - {campaign.name}
            <br />
            {game.description}
          </p>
        ))}
    </div>
  );
}
