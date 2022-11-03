import { map } from "leaflet";
import { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { GameStartedContext, GameTitleContext, GameChatContext } from "../../contexts/gameContext";
import keycloak from "../../keycloak";
import { client } from "../../utils/client";
import React from "react";
import { UserContext } from "../../contexts/userContext";

const PlayButton = () => {

    let { gameId } = useParams();

    const { gameStartedContext, setGameStartedContext } = useContext(GameStartedContext);
    const { setGameTitleContext } = useContext(GameTitleContext);
    const { userContext, setUserContext } = useContext(UserContext);

    const [isUserInGame, setIsUserInGame] = useState(true);


    useEffect(() => {
        async function getPost() {
            let response = await client.get(`/Players/Game/${ gameId }`)
                .then(function (response) {

                for (const user of response.data) {
                    //TODO: Replace 1 with user's id
                    if(user.userId === keycloak.tokenParsed.sub) {
                        setUserContext(user);
                        return;
                    }
                }
                setIsUserInGame(false);
                })
        }
        getPost();
    }, []);


    const startGame = () => {
        if (keycloak.authenticated) {

            client.post('/Players', {
                isHuman: true,
                isPatientZero: false,
                hungerTime: 0,
                biteCode: "",
                isMuted: false,
                gameId: gameId,
                userId: keycloak.tokenParsed.sub, 
                firstName: keycloak.tokenParsed?.given_name,
                lastName: keycloak.tokenParsed?.family_name,
              })
              .then(function (response) {
                setUserContext(response.data);
                setIsUserInGame(true);
              })
              .catch(function (error) {
                console.log(error);
              });
              

            setGameTitleContext(false);
            setGameStartedContext(true);
            
        }
    }

    return (
        <>
            { (isUserInGame === false && gameStartedContext === false) && (
                <div className="mx-auto w-screen text-center absolute z-30 bottom-0 mb-32 pointer-events-none">
                    <button
                        type="button"
                        onClick={ keycloak.authenticated ? startGame : keycloak.login }
                        className="pointer-events-auto inline-flex items-center justify-center p-0.5 mb-2 mr-2 overflow-hidden text-sm font-medium text-gray-900 rounded-lg group bg-gradient-to-br from-purple-600 to-blue-500 group-hover:from-purple-600 group-hover:to-blue-500 hover:text-white dark:text-white focus:ring-4 focus:outline-none focus:ring-blue-300 dark:focus:ring-blue-800">
                        <span className="relative px-5 py-2.5 transition-all ease-in duration-75 bg-white dark:bg-gray-900 rounded-md group-hover:bg-opacity-0">
                            PLAY GAME
                        </span>
                    </button>
                </div>
            ) }
        </>
    );
}

export default PlayButton;