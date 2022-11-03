import { useContext } from "react";
import { NavLink } from "react-router-dom";
import { GameStartedContext, GameChatContext, GameBiteCodeContext, GameTitleContext, SquadListContext } from "../../contexts/gameContext";
import { UserContext, UserFactionContext } from "../../contexts/userContext";
import BitecodeDisplay from "./BitecodeDisplay";
import BiteCodeInput from "./BitecodeInput";
import { BsChatDotsFill, BsFillPeopleFill } from 'react-icons/bs';
import { GiChewedSkull } from 'react-icons/gi';

const BottomNavBar = () => {

    //Hooks
    const { gameBiteCodeContext, setGameBiteCodeContext } = useContext(GameBiteCodeContext);
    const { gameStartedContext } = useContext(GameStartedContext);
    const { userContext } = useContext(UserContext);
    const { gameChatContext, setGameChatContext } = useContext(GameChatContext);
    const { setGameTitleContext } = useContext(GameTitleContext);
    const { squadListContext, setSquadListContext } = useContext(SquadListContext);

    //Functions
    const toggleBiteCode = () => {
        setGameBiteCodeContext(!gameBiteCodeContext);
        setGameChatContext(false);
        setGameTitleContext(false);
        setSquadListContext(false);
    }

    const toggleChat = () => {
        setGameChatContext(!gameChatContext);
        setGameBiteCodeContext(false);
        setGameTitleContext(false);
        setSquadListContext(false);
    }

    const toggleSquadList = () => {
        setSquadListContext(!squadListContext);
        setGameBiteCodeContext(false);
        setGameTitleContext(false);
        setGameChatContext(false);
    }


    return (
        <>
            <NavLink id="LandingLink" to="/">
                <div className="z-40 absolute top-0 left-1 mt-4 h-12 text-lg">
                    <img className="h-12 w-12" src="https://cdn.iconscout.com/icon/free/png-256/back-arrow-1767523-1502427.png" alt="go back" />
                </div>
            </NavLink>

            <nav className={`${userContext.isHuman === false ? 'bg-red-900' : ' bg-blue-900'} block fixed inset-x-0 bottom-0 z-50 shadow rounded-t-lg`}>
                <div className="flex justify-between h-20">
                    {gameStartedContext === true &&
                        <button onClick={toggleChat} className="w-20 h-auto mx-auto text-white text-lg">
                            <div className="flex flex-col">
                                <p className="mx-auto text-2xl"><BsChatDotsFill /></p>
                                <p>Chat</p>
                            </div>
                        </button>
                    }
                    <button onClick={toggleSquadList} className="w-20 h-auto mx-auto text-white text-lg">
                        <div className="flex flex-col">
                            <p className="mx-auto text-2xl"><BsFillPeopleFill /></p>
                            <p>Squad</p>
                        </div>
                    </button>
                    {gameStartedContext === true &&
                        <button onClick={toggleBiteCode} className="w-20 h-auto mx-auto text-white text-lg">
                            <div className="flex flex-col">
                                <p className="mx-auto text-2xl"><GiChewedSkull /></p>
                                <p>Bite code</p>
                            </div>
                        </button>
                    }
                </div>
            </nav>
            {gameBiteCodeContext === true && (userContext.isHuman === true) &&
                <BitecodeDisplay />
            }
            {gameBiteCodeContext === true && (userContext.isHuman === false) &&
                <BiteCodeInput />
            }
        </>
    );
}

export default BottomNavBar;