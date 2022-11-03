import { useForm } from "react-hook-form";
import { GetGameIdContext } from "../../contexts/adminContext";
import { useContext, useState, useEffect } from "react";
import { client } from "../../utils/client";
import { Navigate, useParams } from "react-router-dom";

const EditPlayerForm = () => {
    let { id } = useParams();
    const [ playerData, setPlayer ] = useState([]);
    const [ isToggled, setIsToggled ] = useState(false);
    const [ gameData, setData ] = useState({});
    useEffect(() => {
        loadGame();
    }, []);

    const loadGame = async () => {
        const result = await client.get(`/Players/Game/${ id }`);
        setPlayer(result.data);
    };
    const handleChange = (e, field) => {
        setIsToggled(!isToggled)
        setData((prevState) => {
            let obj = { ...prevState };

            obj[ field ] = e;
            return obj;
        });
    };

    const { register, handleSubmit } = useForm();

    const onSubmit = async (e) => {
        e.preventDefault();
        await client.put(`/Players/${ id }`, gameData).then(r => { if (r.status === 204) alert("Edit succesfull!") });

    };

    const handleOption = (e) => {
        e.preventDefault();
        getPlayerInfo(e.target.value);
    };

    const getPlayerInfo = async (id) => {
        await client.get(`/Players/${ id }`).then(r => {
            setData(r.data);
            setIsToggled(r.data.isHuman)

        });
        
    };
    /*
     
    isHuman - checkbox
    patientZero - checkbox
        
    */
    return (
        <div className="bg-slate-300 pt-24 h-screen">
            <div id="editgame" className="text-lg text-center p-6 ">
                PLAYER
            </div>

            <form
                onSubmit={ handleSubmit(({ name, faction, hungerTime }) => {
                    console.log(name, faction, hungerTime);
                }) }
                className="grid bg-slate-200 rounded-xl p-6 lg:w-1/3 md:w-1/3 m-auto"
            >
                <div className="grid gap-4 p-4">
                    <div>
                        <label
                            htmlFor="selectplayer"
                            className="block mb-2 text-sm font-bold text-gray-900"
                        >
                            Player
                        </label>
                        <select

                            id="selectplayer"
                            onChange={ (e) => handleOption(e) }
                            className="inline-flex space-x-6 w-full rounded-lg border border-gray-300 bg-white px-4 py-2 text-bold font-medium text-gray-700 shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 focus:ring-offset-gray-100"
                        >
                            <option selected disabled>
                                { " " }
                                Select player
                            </option>
                            { playerData ? (
                                playerData.map((p, id) => {
                                    return (
                                        <option key={ id } value={ p.id }>
                                            { " " }
                                            { p.firstName }
                                        </option>
                                    );
                                })
                            ) : (
                                <p> error....</p>
                            ) }
                        </select>
                    </div>
                    
          
                    {
                    <div className="flex items-center mb-3 my-3 bg-white w-full h-10  rounded-lg">
                        <label
                            htmlFor="isHuman"
                            className="block w-full mt-1 ml-2 mr-2 mb-2 text-sm font-bold text-gray-900"
                        >
                            Human?
                        </label>
                        <div className="flex pt-2 pb-2 mr-3 h-10">
                            <input
                                checked={ isToggled }
                                value={ isToggled}
                                onChange={(e) =>
                                    handleChange(e.target.checked, "isHuman")
                                }
                                type="checkbox"
                                id="isHuman"
                                className="focus:accent-purple-800 block w-full font-bold"
                            />
                        </div>
                    </div> }

                    <div className="flex space-x-4">
                        <button

                            className="w-full text-white bg-purple-800 hover:bg-purple-600 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center"
                            onClick={ onSubmit }
                        >
                            Edit Player
                        </button>
                    </div>
                </div>
            </form>
        </div>
    );
};

export default EditPlayerForm;
