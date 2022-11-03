import { useContext, useState } from 'react';
import { GiRank1, GiRank2, GiRank3, GiMeepleArmy, GiChewedSkull } from 'react-icons/gi';
import { MdOutlineMilitaryTech } from 'react-icons/md';
import { SquadListContext } from '../../contexts/gameContext';
import { UserContext } from '../../contexts/userContext';
import React from 'react';
import { client } from '../../utils/client';
import { useLocation, useParams } from 'react-router-dom';
import useGeoLocation from '../../hooks/useGeoLocation';


const SquadMemberList = ({ setSquadMarkers }) => {

    let { gameId } = useParams();

    const { squadListContext, setSquadListContext } = useContext(SquadListContext);
    const { userContext, setUserContext } = useContext(UserContext);

    const [ squadMembers, setSquadMembers ] = useState([]);

    const location = useGeoLocation();

    React.useEffect(() => {
        if (userContext.squadId != null) {
            async function getSquadMembers() {
                const res = await client.get(`/Squads/getplayers/${userContext.squadId}`);
                setSquadMembers(res.data);
            }
            getSquadMembers();    
        }
    }, []);

    const leaveSquad = () => {
        client.delete(`/Squads/${userContext.squadId}/player/${userContext.id}`, {})
        .then(() => {
            setUserContext({
                ...userContext,
                squadId:  null
            })
            setSquadMarkers([]);
        })
        .catch(() => {
            alert("Could not leave squad")
        });
    }

    const putLocation = () => {
        client.put(`/Players/CheckIn/${userContext.id}`, {
            id: userContext.id,
            checkinLat: location.coordinates.lat,
            checkinLon: location.coordinates.lng,
        });
    }

    const fetchLocations = () => {
        client.get(`/Players/Squad/${userContext.squadId}/CheckIn`)
        .then(res => {
            setSquadMarkers(res.data);
        });
    }

    return (
        <>
            {squadListContext === true && (
                <div className="z-40 absolute w-screen h-screen px-2 pt-20 pb-24 pointer-events-none">
                    <div className=" bg-white w-full h-full rounded-lg shadow-lg py-2 px-4 pointer-events-auto overflow-auto">
                        <div className="flex justify-center">
                            <h1 className="font-bold text-2xl mx-auto mt-2">Squad Members</h1>
                            <button onClick={() => setSquadListContext(false)} className="absolute ml-auto text-3xl right-8 bg-white rounded-full">x</button>
                        </div>
                        <div className="mt-4">
                            <div className="flex justify-between text-xs">
                                <div className='flex'>
                                    <button onClick={leaveSquad} className='inline-block w-24 mx-1 p-1 h-6 mt-auto bg-red-600 hover:bg-red-700 active:bg-red-800 text-white text-xs leading-tight rounded-full shadow-md'>Leave Squad</button>
                                </div>
                                <div className='flex flex-col'>
                                    <label className='text-center mb-2'>Update marker locations</label>
                                    <div className='flex mx-auto'>
                                        <button onClick={fetchLocations} className='inline-block w-24 mx-1 p-1 bg-yellow-500 hover:bg-yellow-600 active:bg-yellow-700 text-white text-xs leading-tight rounded-full shadow-md'>Squad location</button>
                                        <button onClick={putLocation} className='inline-block w-24 mx-1 p-1 bg-yellow-500 hover:bg-yellow-600 active:bg-yellow-700 text-white text-xs leading-tight rounded-full shadow-md'>My location</button>
                                    </div>
                                </div>
                            </div>
                            {squadMembers.map((member, index) =>
                                <div key={index} className={`${member.isHuman === false ? 'bg-red-900 border-red-700 text-white ' : 'bg-white '}bg-white width-full my-6 px-4 h-11 py-1 rounded-lg flex justify-between border-2`}>
                                    <div className="overflow-hidden my-auto">
                                        {member.firstName} {member.lastName}
                                    </div>
                                    {member.isHuman === false &&
                                        <div className="text-2xl my-auto">
                                            <GiChewedSkull />
                                        </div>
                                    }
                                    {(member.rank < 250 && member.isHuman === true) &&
                                        <div className="text-2xl my-auto">
                                            <GiRank1 />
                                        </div>
                                    }
                                    {(member.rank >= 250 && member.rank < 500 && member.isHuman === true) &&
                                        <div className="text-2xl my-auto">
                                            <GiRank2 />
                                        </div>
                                    }
                                    {(member.rank >= 500 && member.rank < 750 && member.isHuman === true) &&
                                        <div className="text-2xl my-auto">
                                            <GiRank3 />
                                        </div>
                                    }
                                    {(member.rank >= 750 && member.rank < 1000 && member.isHuman === true) &&
                                        <div className="text-2xl my-auto">
                                            <MdOutlineMilitaryTech />
                                        </div>
                                    }
                                    {(member.rank >= 1000 && member.isHuman === true) &&
                                        <div className="text-2xl my-auto">
                                            <GiMeepleArmy />
                                        </div>
                                    }
                                </div>
                            )}
                        </div>
                    </div>
                </div>
            )}

        </>
    )
}

export default SquadMemberList;