import userEvent from "@testing-library/user-event";
import { useForm } from "react-hook-form";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { client } from "../../utils/client";

const CreateGameForm = () => {
    const { register, handleSubmit } = useForm();
    const [gameData, setData] = useState({});
    const config = { headers: { "Content-Type": "application/json" } };
    const navigate = useNavigate();
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

    const postNewGame = async (
        title,
        description,
        gameLenght,
        startTime,
        image,
        players
    ) => {
        const NewConfig = JSON.stringify({
            PlayerCount: players,
            InitZombies: 0,
            Duration: gameLenght,
            HungerDuration: 0,
            chatCooldown: 0,
        });
        const responseToconfig = await client
            .post(
                "/GameConfigs",
                NewConfig,
                config
            )
            .then((response) => {                postNewGameCall(
                    response.data.id,
                    title,
                    description,
                    startTime,
                    image
                );
            });
    };

    const postNewGameCall = async (
        ConfigId,
        title,
        description,
        startTime,
        image
    ) => {
        const NewGame = JSON.stringify({
            name: title,
            description: description,
            startTime: startTime,
            is_Started: false,
            nw_Lat: 0,
            nw_Lng: 0,
            se_Lat: 0,
            se_Lng: 0,
            image: image,
            gameConfigId: ConfigId,
        });

        const responseToGame = await client
            .post(
                "/Games",
                NewGame,
                config
            )
            .then(() => {
                navigate(`/`);
            });
    };

    return (
        <div className="bg-slate-300 h-screen pt-24">
            <div id="creategame" className="text-lg text-center p-6 ">
                Create Game
            </div>
            <form
                onSubmit={handleSubmit(
                    ({
                        title,
                        description,
                        gameLenght,
                        startTime,
                        image,
                        players,
                    }) => {
                        postNewGame(
                            title,
                            description,
                            gameLenght,
                            startTime,
                            image,
                            players
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
                            {...register("title")}
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
                            {...register("description")}
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
                            {...register("image")}
                            type="text"
                            id="image"
                            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm w-full rounded-lg focus:ring-blue-500 focus:border-blue-500 block p-2.5 "
                            placeholder="URL to Game Card Image."
                        />
                    </div>

                    {/**
                     * Start Time input
                     * Description: Start time of game.
                     */}
                    <div>
                        <label
                            htmlFor="startTime"
                            className="block mb-2 text-sm font-bold text-gray-900"
                        >
                            Game Start Time
                        </label>
                        <input
                            {...register("startTime")}
                            value={
                                formatDate(new Date(gameData.startTime)) || ""
                            }
                            onChange={(e) => handleChange(e, "startTime")}
                            type="date"
                            id="startTime"
                            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 "
                            placeholder="Hours: 0.000"
                            pattern="[0-9]{3}-[0-9]{2}-[0-9]{3}"
                            required
                        />
                    </div>

                    {/**
                     * Max Players input
                     */}
                    <div>
                        <label
                            htmlFor="players"
                            className="block mb-2 text-sm font-bold text-gray-900"
                        >
                            Max Players
                        </label>
                        <input
                            {...register("players")}
                            type="number"
                            id="players"
                            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                            placeholder="Minimum 2 Players"
                            required
                        />
                    </div>

                    {/**
                     * initial zombies input
                     *<div>
                     *    <label
                     *        htmlFor="zombies"
                     *        className="block mb-2 text-sm font-bold text-gray-900"
                     *    >
                     *        Number of Initial Zombies
                     *    </label>
                     *    <input
                     *        {...register("zombies")}
                     *        type="number"
                     *        id="zombies"
                     *        className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                     *        placeholder="Minimum 1 Initial Zombie"
                     *        required
                     *    />
                     *</div>
                     */}

                    {/**
                     * Game length input
                     * Description: Duration length of game.
                     */}
                    <div>
                        <label
                            htmlFor="gameLenght"
                            className="block mb-2 text-sm font-bold text-gray-900"
                        >
                            Game Length
                        </label>
                        <input
                            {...register("gameLenght")}
                            type="number"
                            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 "
                            placeholder="Hours: 0.000"
                            pattern="[0-9]{3}-[0-9]{2}-[0-9]{3}"
                            required
                        />
                    </div>

                    {/**
                     * Hunger timer input
                     * Description: How long a zombie player can go before zombie dies from hunger.
                     */}

                    {/**
                     *<div>
                     *    <label
                     *        htmlFor="hunger"
                     *        className="block mb-2 text-sm font-bold text-gray-900"
                     *    >
                     *        Zombie Hunger Time
                     *    </label>
                     *    <input
                     *        {...register("hungerTime")}
                     *        type="number"
                     *        className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                     *        placeholder="Hours: 0.000"
                     *        required
                     *    />
                     *</div>
                     * chatCooldown input
                     * Description: Cooldown on chat.
                     */}

                    {/**
                *<div>
                *    <label
                *        htmlFor="chatCooldown"
                *        className="block mb-2 text-sm font-bold text-gray-900"
                *    >
                *        Chat Cooldown
                *    </label>
                *    <input
                *        {...register("chatCooldown")}
                *        type="number"
                *        id="chatCooldown"
                *        className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                *        placeholder="0 seconds"
                *        pattern="[0-9]{3}-[0-9]{2}-[0-9]{3}"
                *        required
                *    />
                *</div>
                 * North West - Latitude input
                 * Description: For Map bounding box, for playarea
                *<div>
                *    <label
                *        htmlFor="nw_Lat"
                *        className="block mb-2 text-sm font-bold text-gray-900"
                *    >
                *        North West - Latitude
                *    </label>
                *    <input
                *        {...register("nw_Lat")}
                *        type="number"
                *        id="nw_Lat"
                *        className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                *        placeholder="0.0"
                *    />
                *</div>
*
                *{/**
                * * North West - Longitude input
                * * Description: For Map bounding box, for playarea
                * 
                *<div>
                *    <label
                *        htmlFor="nw_Lng"
                *        className="block mb-2 text-sm font-bold text-gray-900"
                *    >
                *        North West - Longitude
                *    </label>
                *    <input
                *        {...register("nw_Lng")}
                *        type="number"
                *        id="nw_Lng"
                *        className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                *        placeholder="0.0"
                *    />
                *</div>
*
                *{
                * * South East - Latitude input
                * * Description: For Map bounding box, for playarea
                * }
                *<div>
                *    <label
                *        htmlFor="se_Lat"
                *        className="block mb-2 text-sm font-bold text-gray-900"
                *    >
                *        South East - Latitude
                *    </label>
                *    <input
                *        {...register("se_Lat")}
                *        type="number"
                *        id="se_Lat"
                *        className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                *        placeholder="0.0"
                *    />
                *</div>
*
                {/**
                * * South East - Longitude input
                * * Description: For Map bounding box, for playarea
                * }
                *<div>
                *    <label
                *        htmlFor="se_Lng"
                *        className="block mb-2 text-sm font-bold text-gray-900"
                *    >
                *        South East - Longitude
                *    </label>
                *    <input
                *        {...register("se_Lng")}
                *        type="number"
                *        id="se_Lng"
                *        className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                *        placeholder="0.0"
                *    />
                *</div>
                 */}
                    {/**
                    * is_Started input
                    * Description: Bool to tell if game is starting after creation of game
                    <div className="flex items-center mb-4 my-3 bg-white w-full h-10  rounded-lg">
                        <label
                            htmlFor="is_Started"
                            className=" block w-full mt-1 ml-2 mr-2 mb-2 text-sm font-bold text-gray-900"
                        >
                            Start game on create?
                        </label>
                        <div className="flex pt-2  pb-2  mr-3 h-10">
                            <input
                                {...register("is_Started")}
                                type="checkbox"
                                id="is_Started"
                                className="  focus:accent-purple-800 block w-full ml-"
                            />  
                            
                        </div>
                    </div>
                    */}

                    <div className="flex space-x-4">
                        <button
                            type="submit"
                            className=" text-white bg-purple-800 hover:bg-purple-600 focus:ring-4 focus:outline-none focus:ring-purple-300 font-bold rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center"
                        >
                            Create Game
                        </button>
                    </div>
                </div>
            </form>
        </div>
    );
};
export default CreateGameForm;
