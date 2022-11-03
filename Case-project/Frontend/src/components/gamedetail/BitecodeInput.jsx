
import { useContext, useState } from "react";
import { useParams } from "react-router-dom";
import { GameBiteCodeContext } from "../../contexts/gameContext";
import { UserContext } from "../../contexts/userContext";
import useGeoLocation from "../../hooks/useGeoLocation";
import { client } from "../../utils/client";

const BitecodeInput = () => {

    let { gameId } = useParams();

    const { userContext,  } = useContext(UserContext);
    const { setGameBiteCodeContext } = useContext(GameBiteCodeContext);

    const [biteCode, setBitecode] = useState("");
    const [description, setDescription] = useState("");

    const location = useGeoLocation();

    const handleSubmit = (event) => {
        event.preventDefault();

        if (biteCode) {
            async function getBittenUser() {

                client.post(`/Games/Kill`, {
                    biteCode: biteCode,
                    timeOfDeath: new Date(),
                    description: description,
                    lat: location.coordinates.lat,
                    lng: location.coordinates.lng,
                    gameId: gameId,
                    killerId: userContext.id
                  })
                  .then(function (response) {
                    setGameBiteCodeContext(false);
                  })
                  .catch(function (error) {
                    alert("Invalid Bitecode");
                  });

            }
            getBittenUser();
        }

    }

    return (
        <div className="w-screen absolute z-50 bottom-0 mb-24 drop-shadow-lg">
            <form onSubmit={handleSubmit}>
                <div className="flex flex-col justify-center w-11/12 mx-auto py-4 px-8 rounded-lg bg-white">

                    <label>Bite</label>
                    <input
                        type="number"
                        className="border-2 text-3xl w-full text-center mx-auto mb-4 text-transform uppercase"
                        placeholder="Bitecode..."
                        maxLength={6}
                        value={biteCode}
                        onChange={(e) => e.target.value.length > 6 ? null : setBitecode(e.target.value)}>
                    </input>
                    <label>Kill Description (optional)</label>
                    <textarea
                        className="border-2 w-full mx-auto mb-4 px-1 resize-none"
                        maxLength={200}
                        placeholder="Kill description..."
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}>
                    </textarea>

                    {/* TODO: Replace btn */}
                    <button type="submit" value="Submit" className="w-8/12 mx-auto bg-lime-500 py-1 rounded-lg border-lime-800 border-2">
                        <div className="bg-neutral-800 text-white border-neutral-800 border-2 rounded-lg mx-auto w-36" >
                            <div className="flex justify-evenly">
                                <div className="w-4 h-4 rounded-b-lg bg-yellow-100"></div>
                                <div className="w-4 h-2 rounded-b-sm bg-yellow-100 mr-6"></div>
                                <div className="w-4 h-3 rounded-b-lg bg-yellow-100"></div>
                            </div>
                            <div className="absolute text-2xl left-1/2 transform -translate-x-1/2 -translate-y-1/2 font-normal mt-1">
                                BITE
                            </div>
                            <div className="h-4"></div>
                            <div className="flex">
                                <div className="w-10 h-1  mx-auto rounded-md bg-red-800"></div>
                            </div>
                        </div>
                    </button>
                </div>
            </form>


            <div className="flex justify-between">
                <div className="w-20 mx-auto"></div>
                <div className="w-20 mx-auto"></div>
                <div className="w-20 mx-auto">
                    <div className="overflow-hidden">
                        <div className="mx-auto h-4 w-4 bg-white -rotate-45 transform origin-top-left"></div>
                    </div>
                </div>
            </div>

        </div>
    )
}

export default BitecodeInput;