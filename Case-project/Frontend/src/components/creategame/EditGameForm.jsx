import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";
import { client } from "../../utils/client";
import { useEffect, useState } from "react";

const EditGameForm = () => {
    let { id } = useParams();
    const [gameData, setData] = useState({});
    useEffect(() => {
        loadGame();
    }, []);

    const loadGame = async () => {
        const result = await client.get(`/Games/${id}`);
        setData(result.data);
    };

    const formatDate = (date) => {
        return (
            date.getFullYear() +
            "-" +
            parseInt(date.getMonth() + 1) +
            "-" +
            date.getDate()
        );
    };

    const handleChange = (e, field) => {
        setData((prevState) => {
            let obj = { ...prevState };
            if (typeof e === "boolean") {
                obj[field] = e;
                return obj;
            } else {
                obj[field] = e.target.value;
                return obj;
            }
        });
    };

    const { register, handleSubmit } = useForm();

    const onSubmit = async (e) => {
        e.preventDefault();
        await client.put(`/Games/${id}`, gameData).then( r => {if(r.status === 204) alert("Edit succesful!")});
       
    };

    const postNewGame = (
        title,
        description,
        gameLenght,
        is_Started,
        nw_Lat,
        nw_Lng,
        se_Lat,
        se_Lng,
        image
    ) => {};

    return (
        <div className="bg-slate-300 h-screen pt-24 ">
            <div id="editgame" className="text-lg text-center p-6 ">
                Edit Game
            </div>
            <form
                onSubmit={handleSubmit(
                    ({
                        title,
                        description,
                        gameLenght,
                        is_Started,
                        nw_Lat,
                        nw_Lng,
                        se_Lat,
                        se_Lng,
                        image,
                    }) => {
                        postNewGame(
                            title,
                            description,
                            gameLenght,
                            is_Started,
                            nw_Lat,
                            nw_Lng,
                            se_Lat,
                            se_Lng,
                            image
                        );
                    }
                )}
                className="grid bg-slate-200 rounded-xl p-6 lg:w-2/3 md:w-2/3 m-auto"
            >
                <div className="grid gap-6 mb-6 md:grid-cols-2 p-6 ">
                    {/**
                     * Titel input
                     */}
                    <div>
                        <label
                            htmlFor="title"
                            className="block mb-2 text-sm font-bold text-gray-900"
                        >
                            Title
                        </label>
                        <input
                            value={gameData.name || ""}
                            onChange={(e) => handleChange(e, "name")}
                            type="text"
                            id="title"
                            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                            placeholder="Game Title"
                            required
                        />
                    </div>

                    {/**
                     * description input
                     * Description: Game description and local rules.
                     */}
                    <div>
                        <label
                            htmlFor="description"
                            className="block mb-2 text-sm font-bold text-gray-900"
                        >
                            Description
                        </label>
                        <input
                            value={gameData.description || ""}
                            onChange={(e) => handleChange(e, "description")}
                            type="text"
                            id="description"
                            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm w-full rounded-lg focus:ring-blue-500 focus:border-blue-500 block p-2.5 "
                            placeholder="Game description and local rules."
                            required
                        />
                    </div>

                    {/**
                     * ImageURL input
                     * Description: URL to Game Card Image.
                     */}
                    <div>
                        <label
                            htmlFor="image"
                            className="block mb-2 text-sm font-bold text-gray-900"
                        >
                            Image
                        </label>
                        <input
                            value={gameData.image || ""}
                            onChange={(e) => handleChange(e, "image")}
                            type="text"
                            id="image"
                            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm w-full rounded-lg focus:ring-blue-500 focus:border-blue-500 block p-2.5 "
                            placeholder="URL to Game Card Image."
                        />
                    </div>

                    {/**
                     * Game length input
                     * Description: Duration length of game.
                     */}
                    <div>
                        <label
                            htmlFor="gameLenght"
                            className="block mb-2 text-sm font-bold text-gray-900"
                        >
                            Game start time
                        </label>
                        <input
                            value={
                                formatDate(new Date(gameData.startTime)) || ""
                            }
                            onChange={(e) => handleChange(e, "startTime")}
                            type="date"
                            id="gameLength"
                            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 "
                            placeholder="Hours: 0.000"
                            pattern="[0-9]{3}-[0-9]{2}-[0-9]{3}"
                            required
                        />
                    </div>

                    {/**
                     * North West - Latitude input
                     * Description: For Map bounding box, for playarea
                     */}
             {/*        <div>
                        <label
                            htmlFor="nw_Lat"
                            className="block mb-2 text-sm font-bold text-gray-900"
                        >
                            North West - Latitude
                        </label>
                        <input
                            value={gameData.nw_Lat || ""}
                            onChange={(e) => handleChange(e, "nw_Lat")}
                            type="number"
                            id="nw_Lat"
                            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                            placeholder="0.0"
                        />
                    </div> */}

                    {/**
                     * North West - Longitude input
                     * Description: For Map bounding box, for playarea
                     */}
                 {/*    <div>
                        <label
                            htmlFor="nw_Lng"
                            className="block mb-2 text-sm font-bold text-gray-900"
                        >
                            North West - Longitude
                        </label>
                        <input
                            value={gameData.nw_Lng || ""}
                            onChange={(e) => handleChange(e, "nw_Lng")}
                            type="number"
                            id="nw_Lng"
                            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                            placeholder="0.0"
                        />
                    </div> */}

                    {/**
                     * South East - Latitude input
                     * Description: For Map bounding box, for playarea
                     */}
                 {/*    <div>
                        <label
                            htmlFor="se_Lat"
                            className="block mb-2 text-sm font-bold text-gray-900"
                        >
                            South East - Latitude
                        </label>
                        <input
                            value={gameData.se_Lat || ""}
                            onChange={(e) => handleChange(e, "se_Lat")}
                            type="number"
                            id="se_Lat"
                            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                            placeholder="0.0"
                        />
                    </div> */}

                    {/**
                     * South East - Longitude input
                     * Description: For Map bounding box, for playarea
                     */}
                   {/*  <div>
                        <label
                            htmlFor="se_Lng"
                            className="block mb-2 text-sm font-bold text-gray-900"
                        >
                            South East - Longitude
                        </label>
                        <input
                            value={gameData.se_Lng || ""}
                            onChange={(e) => handleChange(e, "se_Lng")}
                            type="number"
                            id="se_Lng"
                            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                            placeholder="0.0"
                        />
                    </div> */}
                    {/**
                     * is_Started input
                     * Description: Bool to tell if game is starting after creation of game
                     */}
                 {/*    <div className="flex items-center mb-4 my-3 bg-white w-full h-10  rounded-lg">
                        <label
                            htmlFor="is_Started"
                            className="block w-full mt-1 ml-2 mr-2 mb-2 text-sm font-bold text-gray-900"
                        >
                            Start game on create?
                        </label>
                        <div className="flex pt-2  pb-2 mr-3 h-10">
                            <input
                                value={gameData.is_Started || false}
                                onChange={(e) =>
                                    handleChange(e.target.checked, "is_Started")
                                }
                                type="checkbox"
                                id="is_Started"
                                className="focus:accent-purple-800 block w-full"
                            />
                        </div>
                    </div> */}

                    <div className="flex space-x-4">
                        <button
                            onClick={onSubmit}
                            type="submit"
                            className=" text-white bg-purple-800 hover:bg-purple-600 focus:ring-4 focus:outline-none focus:ring-purple-300 font-bold rounded-lg text-sm w-full lg:h-10 lg:mt-3 sm:w-auto px-5 py-2.5 text-center "
                        >
                            Edit Game
                        </button>
                    </div>
                </div>
            </form>
        </div>
    );
};

export default EditGameForm;
