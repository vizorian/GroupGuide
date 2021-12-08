import React from "react";
import useAxios from "axios-hooks";
import { Link } from "react-router-dom";

export default function GameGetAll({ token }) {
  const [{ data, loading, error }] = useAxios(
    {
      url: "http://localhost:5000/api/games",
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
      <Link to="create">Create game</Link>
      <br />

      {data &&
        data.map((game) => (
          <li className="p-3 hover:bg-green-100 border" key={game.id}>
            <Link to={`${game.id}`}>
              {game.id} - {game.name} <br />
              {game.description}
            </Link>
          </li>
        ))}
    </div>
  );
}
