import { useContext, useEffect, useState } from "react";
import { SquadListContext } from "../../contexts/gameContext";
import { AiOutlinePlus } from 'react-icons/ai';
import { useParams } from "react-router-dom";
import React from "react";
import { client } from "../../utils/client";
import { UserContext } from "../../contexts/userContext";

const SquadList = () => {

    let { gameId } = useParams();

    const [squadList, setSquadList] = useState([]);
    const { userContext, setUserContext } = useContext(UserContext);


    React.useEffect(() => {
        async function getSquadList() {
            const res = await client.get(`/Squads/Game/${gameId}`);

            setSquadList(res.data);
        }
        getSquadList();
    }, []);

    const { squadListContext, setSquadListContext } = useContext(SquadListContext);
    const [createInputVisible, setCreateInputVisible] = useState(false);
    const [squadName, setSquadName] = useState("");
    const [squadSize, setSquadSize] = useState("");
    const [joinedSquad, setJoinedSquad] = useState(false);

    React.useEffect(() => {
        if (userContext.squadId == null) {
            setJoinedSquad(false);
        }
        else {
            setJoinedSquad(true);
        }
    }, []);


    const handleSubmit = (event) => {

        event.preventDefault();
        if (squadName === "") {
            alert("Invalid Squad Name");
            return;
        }
        if (squadSize === "") {
            alert("Invalid Squad Size");
            return;
        }
        if (squadSize <= 1) {
            alert("Squad Size must be at least 2 people");
            return;
        }

        client.post(`/Squads`, {
            name: squadName,
            size: squadSize,
            gameId: gameId,
        })
            .then(function (response) {
                client.put(`/Squads/${response.data.id}/player/${userContext.id}`, {})
                    .then(function (response) {
                        setCreateInputVisible(false);
                    })
                setSquadList((prevState) => {
                    response.data.memberCount = 1;
                    return [...prevState, response.data]
                });
                setUserContext((prev) => {
                    return {
                        ...prev,
                        squadId: response.data.id
                    }
                });
            })
            .catch(function (error) {
                alert("Could not create Squad");
            });

    }

    const joinSquad = (squadId) => {
        client.put(`/Squads/${squadId}/player/${userContext.id}`, {})
        .then(() => {
            setUserContext({
                ...userContext,
                squadId:  squadId
            });
        })
        .catch(() => {
            alert("Could not join squad!")
        });
    }

    return (
        <>
            {squadListContext === true &&
                <div className="z-40 absolute w-screen h-screen px-2 pt-20 pb-24 pointer-events-none">
                    <div className=" bg-white w-full h-full rounded-lg shadow-lg py-2 px-4 pointer-events-auto overflow-y-scroll">
                        <div className="flex justify-center">
                            <h1 className="font-bold text-2xl mx-auto mt-2">Squad List</h1>
                            <button onClick={() => { setSquadListContext(false); setCreateInputVisible(false) }} className="absolute ml-auto text-3xl right-8 bg-white rounded-full">x</button>
                        </div>
                        <div className="mt-4">
                            {createInputVisible === false &&
                                <div className="width-full my-6 p-4 bg-white rounded-lg flex justify-between border-2">
                                    <div>
                                        Create New Squad
                                    </div>
                                    <div className="flex">
                                        <button onClick={() => setCreateInputVisible(true)} className="ml-2 bg-lime-500 font-bold text-white rounded-full w-12 h-6 flex">
                                            <p className="m-auto"><AiOutlinePlus /></p>
                                        </button>
                                    </div>
                                </div>
                            }
                            {createInputVisible === true &&
                                <form onSubmit={handleSubmit}>
                                    <div className="width-full my-6 p-4 bg-white rounded-lg flex justify-between border-2">
                                        <div className="w-3/6 flex flex-col mb-4">
                                            <label>Squad Name</label>
                                            <input
                                                type="text"
                                                placeholder="Name"
                                                className="border-2 px-1 rounded-md"
                                                value={squadName}
                                                onChange={(e) => setSquadName(e.target.value)} />
                                        </div>
                                        <div className="w-1/6 flex flex-col">
                                            <label>Size</label>
                                            <input
                                                type="number"
                                                placeholder="0"
                                                className="border-2 px-1 rounded-md"
                                                value={squadSize}
                                                onChange={(e) => setSquadSize(e.target.value)} />
                                        </div>
                                        <div className="flex">
                                            <button type="submit" value="Submit" className="ml-2 my-auto bg-lime-500 text-white rounded-full px-2 h-8">Create</button>
                                        </div>
                                    </div>
                                </form>
                            }



                            {squadList.map((squad) =>
                                <div key={squad.id} className={`${squad.memberCount === squad.memberLimit ? 'bg-gray-200 text-gray-400' : 'bg-white text-black'} width-full my-6 p-4 rounded-lg flex justify-between border-2`}>
                                    <div className="overflow-hidden">
                                        {squad.name}
                                    </div>
                                    <div className="flex">
                                        <div className="w-14 flex">
                                            👥{squad.memberCount}/{squad.size}
                                        </div>
                                        {squad.memberCount >= squad.memberLimit &&
                                            <button disabled className="ml-2 bg-gray-400 text-white rounded-full w-12 h-6">Full</button>
                                        }
                                        {squad.memberCount != squad.memberLimit &&
                                            <button onClick={() => joinSquad(squad.id)} className="ml-2 bg-blue-900 text-white rounded-full w-12 h-6">Join</button>
                                        }
                                    </div>
                                </div>
                            )}

                        </div>
                    </div>
                </div>
            }
        </>
    )
}

export default SquadList;
