import { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { GameChatContext } from "../../contexts/gameContext";
import { UserContext } from "../../contexts/userContext";
import { client } from "../../utils/client";


const Chat = () => {
    let { gameId } = useParams();
    const { gameChatContext, setGameChatContext } = useContext(GameChatContext);
    const config = { headers: { "Content-Type": "application/json" } };
    const { userContext } = useContext(UserContext);

    const [getchat, setGetChat] = useState([])

    const [message, setMessage] = useState("");

    const onMessageChange = (event) => {
        setMessage(event.target.value)
    }


    const PostHumanChatConfig = JSON.stringify({
        "message": message,
        "isHumanGlobal": true,
        "chatTime": new Date(),
        "gameId": gameId,
        "playerId": userContext.id

    });

    const PostZombieChatConfig = JSON.stringify({
        "message": message,
        "isZombieGlobal": true,
        "chatTime": new Date(),
        "gameId": gameId,
        "playerId": userContext.id

    });
    const PostSquadChatConfig = JSON.stringify({
        "message": message,
        "isZombieGlobal": true,
        "chatTime": new Date(),
        "gameId": gameId,
        "squadId": userContext.squadId,
        "playerId": userContext.id

    });

    const PostGlobalChatConfig = JSON.stringify({
        'message': message,
        'chatTime': new Date(),
        'gameId': gameId,
        'playerId': userContext.id
    });


    const [isGlobal, setIsGlobal] = useState(false)
    const [isFaction, setIsFaction] = useState(false)
    const [isSquad, setIsSquad] = useState(false)
    const isGlobalChat = () => {
        setGetChat([])
        setIsGlobal(true);
        setIsFaction(false);
        setIsSquad(false);
        loadGlobal();
    }
    const isSquadChat = () => {
        setGetChat([])
        setIsGlobal(false);
        setIsFaction(false);
        setIsSquad(true);
        loadSquad()
    }
    const isFactionChat = () => {
        setGetChat([])
        setIsGlobal(false);
        setIsFaction(true);
        setIsSquad(false);
        if (userContext.isHuman)
            loadHuman()
        else
            loadZombie()
    }

    const loadGlobal = async () => {
        const result = await client.get(`/Chats/${gameId}/Global`).then(r => {
            setGetChat(r.data)

        });



    }
    const loadSquad = async () => {
        await client.get(`/Chats/${gameId}/Squad/${userContext.squadId}`).then(r => {
            setGetChat(r.data);


        });
    }
    const loadHuman = async () => {
        await client.get(`/Chats/${gameId}/HumanChat`).then(r => {
            setGetChat(r.data);

        });


    }
    const loadZombie = async () => {
        await client.get(`/Chats/${gameId}/ZombieChat`).then(r => {
            setGetChat(r.data);

        });


    }


    const postChat = () => {
        if (isGlobal) {
            client.post(
                `/Chats/Player/Global`,
                PostGlobalChatConfig,
                config
            ).then(() => {
                loadGlobal()
            });


        } else if (isFaction) {
            if (userContext.isHuman) {
                client.post(
                    `/Chats/Local/Human`,
                    PostHumanChatConfig,
                    config
                ).then(() => {
                    loadHuman()
                });
            } else {
                client.post(
                    `/Chats/Local/Zombie`,
                    PostZombieChatConfig,
                    config
                ).then(() => {
                    loadZombie()
                });
            }

        } else if (isSquad) {
            client.post(
                `/Chats/Squad`,
                PostSquadChatConfig,
                config
            ).then(() => {
                loadSquad()
            });
        }

        setMessage("");
    }



    return (
        <>
            {gameChatContext &&
                (
                    <div className="z-30 absolute w-screen h-screen px-4 pt-40 pb-24 pointer-events-none drop-shadow-xl">
                        <div className=" bg-white w-full h-fit max-h-full mx-auto rounded-lg shadow-lg pointer-events-auto  lg:w-8/12 overflow-scroll">

                            <section className="flex justify-between bg-sky-600 border-gray-500 parent p-2 sticky top-0 px-4 lg:text-3xl lg:h-16 ">
                                
                                {userContext.squadId !== 0 ?
                                    <div className="flex gap-8 lg:gap-14">
                                        <button onClick={isSquadChat} className="hover:underline active:underline">Squad</button>

                                        <button onClick={isFactionChat} className="hover:underline active:underline">Faction</button>

                                        <button onClick={isGlobalChat} className="hover:underline active:underline">Global</button>
                                    </div>
                                    :
                                    <div className="flex gap-8 lg:gap-14">
                                        <button onClick={isFactionChat} className="hover:underline active:underline">Faction</button>

                                        <button onClick={isGlobalChat} className="hover:underline active:underline">Global</button>
                                    </div>
                                }


                                <div>
                                    <button onClick={() => setGameChatContext(false)} className="text-3xl rounded-full">
                                        x
                                    </button>
                                </div>
                            </section>

                            <div className="h-screen bg-gray-100 text-gray-500 text-center pt-24">Scroll down to see chat...</div>

                            {getchat ? getchat.map((chats, id) => {

                                return <section key={id} className=" bg-gray-100 p-4">

                                    <div className="max-w-4xl mx-auto space-y-12 grid grid-cols-1">

                                        {chats.playerId === userContext.id ?

                                            <div className="place-self-end text-right">
                                                You
                                                <div className="bg-green-50 text-green-900 p-5 rounded-2xl rounded-tr-none">
                                                    {chats.message}
                                                </div>
                                            </div>
                                            :
                                            <div className="place-self-start">
                                                {chats.firstName + " " + chats.lastName}
                                                <div className="bg-white p-5 rounded-2xl rounded-tl-none">
                                                    {chats.message}
                                                </div>
                                            </div>

                                        }
                                    </div>

                                </section>
                            }) : <h1> alksdjajdsj</h1>}

                            <section className=" flex gap-4 justify-between sticky bottom-0 bg-slate-200 w-full h-auto items-center px-4 ">

                                <textarea value={message} onChange={e => onMessageChange(e)} className="flex-grow m-2 py-2 px-8 my-4 mr-1 rounded-full resize-none" placeholder="Message" rows='1' >
                                </textarea>

                                <button onClick={postChat} className="bg-sky-500 hover:bg-sky-700 rounded-full p-2 border-solid lg:text-xl">Send</button>

                            </section>

                        </div>

                    </div>
                )}

        </>
    );
}

export default Chat;


{/* <div className="z-40 absolute w-screen h-screen px-2 pt-20 pb-24 pointer-events-none">
    <div className=" bg-white w-full h-full m-auto rounded-lg shadow-lg py-2 px-4 pointer-events-auto overflow-y-scroll"></div> */}