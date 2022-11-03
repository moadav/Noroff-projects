import { useContext } from "react";
import { GameTitleContext } from "../../contexts/gameContext";
import { UserContext } from "../../contexts/userContext";

const Statistics = () => {
    const { userContext } = useContext(UserContext);
    const { gameTitleContext } = useContext(GameTitleContext);

    return (
        <>
            {gameTitleContext === false && (
                <div className="absolute w-screen z-10 top-5">
                    <div className="text-3xl flex justify-center items-end">
                        <div>00</div>
                        <div className="text-xl">h</div>
                        <div>00</div>
                        <div className="text-xl">m</div>
                        <div>00</div>
                        <div className="text-xl">s</div>
                    </div>
                </div>
            )}

            {gameTitleContext === false && userContext.isHuman === false && (
                <div className="absolute z-30 top-20 right-3.5 flex border-red-900 border-y-2 p-1">
                    <img
                        className="w-5 h-5"
                        src="https://www.freeiconspng.com/thumbs/skull-icon/healthcare-skull-icon-5.png"
                        alt="killcount icon"
                    />
                    <p className="ml-1 w-4">10</p>
                </div>
            )}
        </>
    );
};

export default Statistics;
