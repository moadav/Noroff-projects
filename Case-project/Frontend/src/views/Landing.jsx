import Card from "../components/gamelist/Card";
import React from "react";
import { client } from "../utils/client";


const Landing = () => {



    const [post, setPost] = React.useState([]);

    React.useEffect(() => {
        async function getPost() {
            let response = await client.get("/Games");
            const arr = [];
            for (const game of response.data) {
                response = await client.get(`/Players/Game/${game.id}`);
                game.playerCount = response.data.length;

                response = await client.get(`/GameConfigs/${game.gameConfigId}`);
                setGameState(game, response.data.duration);

                arr.push(game);
            }
            setPost(arr);
        }
        getPost();
    }, []);

    const setGameState = (game, duration) => {

        let gameDate = new Date(game.startTime);
        let localDate = new Date();
        let endDate = new Date(gameDate.getTime() + (duration * 60 * 60 * 1000));

        if (gameDate > localDate) {
            game.gameState = "Registration";
        }
        else if (gameDate <= localDate && endDate >= localDate) {
            game.gameState = "In Progress"
        }
        else {
            game.gameState = "Complete";
        }
        game.StartTime = gameDate.toLocaleString();
    }


    return (
        <>
            <div className="flex flex-wrap w-screen h-screen justify-center">
                {/* <div className="flex gap-5 p-2 h-screen min-w-max w-screen  md:container md:mx-auto lg:grid-flow-col"> */}
                
                {post.map((game) => (
                    <div className=" mx-2 my-2">


                        <Card
                            id={game.id}
                            key={game.id}
                            title={game.name}
                            description={game.description}
                            gameState={game.gameState}
                            image={game.image}
                            playerCount={game.playerCount}
                            StartTime={game.StartTime}
                            EndTime={game.EndTime}
                        />

                    </div>
                ))}
            </div>
        </>
    );
};

export default Landing;
