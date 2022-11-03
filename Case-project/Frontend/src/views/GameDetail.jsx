import BottomNavBar from "../components/gamedetail/NavElements";
import Map from "../components/gamedetail/Map";
import Title from "../components/gamedetail/Title";
import PlayButton from "../components/gamedetail/PlayButton";
import {
    GameStartedContext,
    GameTitleContext,
    SquadListContext,
    GameChatContext,
    GameBiteCodeContext,
    GameContext,
} from "../contexts/gameContext";
import { UserContext } from "../contexts/userContext";
import { GetGameIdContext, MarkerSelectedContext } from "../contexts/adminContext";
import SquadList from "../components/gamedetail/SquadList";
import Chat from "../components/gamedetail/Chat";
import { useContext, useState } from "react";
import Statistics from "../components/gamedetail/Statistics";
import { useParams } from "react-router-dom";
import React from "react";
import { client } from "../utils/client";
import AdminSideBar from "../components/gamedetail/AdminSideBar";
import keycloak from "../keycloak";
import SquadMemberList from "../components/gamedetail/SquadMemberList";

const GameDetail = () => {
    let { gameId } = useParams();
    const [ userFactionContext, setUserFactionContext ] = useState("human");
    const [ game, setGame ] = useState(null);
    const [gameStartedContext, setGameStartedContext] = useState(null);
    const [gameTitleContext, setGameTitleContext] = useState(true);
    const [gameChatContext, setGameChatContext] = useState(false);
    const [squadListContext, setSquadListContext] = useState(false);
    const [gameBiteCodeContext, setGameBiteCodeContext] = useState(false);
    const [markerSelectedContext, setMarkerSelectedContext] = useState(null);

    const [userContext, setUserContext] = useState({
        isHuman: true,
        isPatientZero: false,
        hungerTime: 0,
        biteCode: "",
        isMuted: false,
        gameId: null,
        userId: null,
        firstName: "",
        lastName: "",
        squadId: null,
    });

    const [gameContext, setGameContext] = useState(GameContext);
    const [player, setPlayer] = useState(null);

    const {setGameIdContext} = useContext(GetGameIdContext);
    setGameIdContext(gameId);


    const [squadMarkers, setSquadMarkers] = useState([]);


    React.useEffect(() => {
        async function getGame() {
            const res = await client.get(`/Games/${ gameId }`);

            setGameContext(res.data);

            let gameDate = new Date(res.data.startTime);
            let localDate = new Date();
            setGameStartedContext(localDate < gameDate ? false : true);
        }
        async function getUser() {
            if (keycloak.tokenParsed?.sub) {
                let response = await client.get(`/Players/Game/${gameId}`)
                    .then(function (response) {

                        for (const user of response.data) {
                            if (user.userId === keycloak.tokenParsed.sub) {
                                setUserContext(user);
                                return;
                            }
                        }
                    })
            }
        }
        getGame();
        getUser();
    }, []);

    console.log(keycloak);

    return (
        <>
            <GameContext.Provider value={{ gameContext, setGameContext }} >
                <GameBiteCodeContext.Provider value={{ gameBiteCodeContext, setGameBiteCodeContext }} >
                    <GameChatContext.Provider value={{ gameChatContext, setGameChatContext }} >
                        <GameStartedContext.Provider value={{ gameStartedContext, setGameStartedContext }} >
                            <GameTitleContext.Provider value={{ gameTitleContext, setGameTitleContext }} >
                                <SquadListContext.Provider value={{ squadListContext, setSquadListContext }} >
                                    <UserContext.Provider value={{ userContext, setUserContext }}>
                                        <MarkerSelectedContext.Provider value={{ markerSelectedContext, setMarkerSelectedContext, }} >
                                            <Title />
                                            <PlayButton />
                                            {!userContext.squadId &&
                                                <SquadList />
                                            }
                                            {userContext.squadId &&
                                                <SquadMemberList setSquadMarkers={setSquadMarkers}/>
                                            }
                                            <AdminSideBar/>
                                            <Chat />
                                            <Map squadMarkers={squadMarkers} setSquadMarkers={setSquadMarkers}/>
                                            {/* <Statistics /> */}
                                            <BottomNavBar />
                                        </MarkerSelectedContext.Provider>
                                    </UserContext.Provider>
                                </SquadListContext.Provider>
                            </GameTitleContext.Provider>
                        </GameStartedContext.Provider>
                    </GameChatContext.Provider>
                </GameBiteCodeContext.Provider>
            </GameContext.Provider>
        </>
    );
};

export default GameDetail;
