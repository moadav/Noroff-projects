import { useContext } from "react";
import { GameContext, GameTitleContext, MyContext } from "../../contexts/gameContext";
import { UserContext } from "../../contexts/userContext";
import { GameChatContext, GameBiteCodeContext } from "../../contexts/gameContext";


const Title = () => {

    const { gameTitleContext, setGameTitleContext } = useContext(GameTitleContext);
    const { userContext } = useContext(UserContext);
    const { setGameBiteCodeContext } = useContext(GameBiteCodeContext);
    const { setGameChatContext } = useContext(GameChatContext);
    const { gameContext } = useContext(GameContext);

    const closeTitle = () => {
        setGameTitleContext(false);
    }

    const openTitle = () => {
        setGameTitleContext(true);
        setGameBiteCodeContext(false);
        setGameChatContext(false);
    }

    return (
        <>
            { ((gameTitleContext === false) &&
                <button
                    className={ `${ userContext.isHuman === false ? 'bg-red-900' : ' bg-blue-900' }  absolute z-40 right-0 m-4 w-12 h-12 text-lg text-white rounded-full` }
                    onClick={ openTitle }>
                    <i>i</i>
                </button>
            ) }

            { ((gameTitleContext === true) &&
                <div className="absolute mt-32 w-screen z-20 h-3/5 pointer-events-none">
                    <div className={ `${ userContext.isHuman === false ? 'border-red-900' : ' border-blue-900' } bg-white drop-shadow-lg flex m-auto flex-col max-h-full w-8/12 p-2 border-b-8 rounded pointer-events-auto` }>
                        <button className="ml-auto w-5 h-5" onClick={ closeTitle }>
                            <div className={ `${ userContext.isHuman === false ? 'bg-red-900' : ' bg-blue-900' } text-white w-12 h-12 rounded-full flex items-center justify-center` }>
                                <div>
                                    X
                                </div>
                            </div>
                        </button>
                        <div className="flex justify-center mx-auto w-5/6 overflow-x-auto">
                            <h1 className={ `${ userContext.isHuman === false ? 'border-red-900' : ' border-blue-900' } font-bold text-2xl mb-4 border-b-2` }>{ gameContext.name }</h1>
                        </div>
                        <div className="mx-auto px-4 overflow-y-auto">
                            <h2 className="font-bold text-center m-4">DESCRIPTION</h2>
                            <div className="max-h-32  rounded-lg p-2">
                                <p className="mb-4 text-sm">{ gameContext.description }</p>
                            </div>
                            <h2 className="font-bold text-center m-4">RULES</h2>
                            <div className=" h-40 rounded-lg p-2 mb-4">
                                <p className="mb-4 text-sm">
                                    <strong>The zombies</strong> win if all players are "infected" within the game's time limit.
                                    A player can be "infected" if a zombie touches them and inserts the player's bitecode into the
                                    <em> Bite Code input.</em><br />
                                    <strong>The humans</strong> win if they can outlast the time limit without being "infected".<br /><br />
                                    <strong>Disclaimer </strong> We <u>DO NOT</u> condone any violence and <u>WILL NOT</u> be responsible
                                    for damages caused during a game of any kind. Please be caucious while playing and
                                    be respectful to your fellow players.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            ) }
        </>
    );
}

export default Title;