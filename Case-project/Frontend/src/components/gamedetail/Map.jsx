import { MapContainer } from "react-leaflet/MapContainer";
import { TileLayer } from "react-leaflet/TileLayer";
import { Marker, Popup } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import { useContext, useRef, useState } from "react";
import L from "leaflet";
import useGeoLocation from "../../hooks/useGeoLocation";
import { UserContext } from "../../contexts/userContext";
import "./Map.css";
import { MarkerSelectedContext } from "../../contexts/adminContext";
import { GameStartedContext } from "../../contexts/gameContext";
import { useMapEvents } from "react-leaflet/hooks";
import { useParams } from "react-router-dom";
import React from "react";
import { client } from "../../utils/client";

const Map = ({ squadMarkers, setSquadMarkers }) => {
    const mapRef = useRef();
    const { userContext } = useContext(UserContext);
    const { gameStartedContext } = useContext(GameStartedContext);
    const { markerSelectedContext, setMarkerSelectedContext } = useContext(
        MarkerSelectedContext
    );

    let { gameId } = useParams();

    const [ gravestoneList, setGravestoneList ] = useState([]);
    const [ missionList, setMissionList ] = useState([]);

    const config = { headers: { "Content-Type": "application/json" } };

    const getGravestoneList = async () => {
        const res = await client.get(`/Games/${gameId}/GravestoneVictims`).then((r) => {
            setGravestoneList(r.data);
        });
    }
    const getMissionList = async () => {
        const res = await client.get(`/Games/Missions/Game/${gameId}`).then((r) => {
            setMissionList(r.data);
        });
    }

    React.useEffect(() => {
        getGravestoneList();
        getMissionList();
    }, []);

    //get gravestones
    //get missions


    React.useEffect(() => {
        async function getGravestoneList() {
            const res = await client.get(`/Games/${ gameId }/GravestoneVictims`);
            setGravestoneList(res.data);
        }
        getGravestoneList();
        if (userContext.squadId) {
            fetchLocations();
        }
    }, [ userContext ]);

    const fetchLocations = () => {
        client.get(`/Players/Squad/${userContext.squadId}/CheckIn`)
            .then(res => {
                setSquadMarkers(res.data);
            });
    }

    const gravestoneMarker = new L.Icon({
        iconUrl:
            "https://www.shareicon.net/download/2015/09/26/107761_dead_512x512.png",
        iconSize: [ 50, 50 ],
        iconAnchor: [ 25, 50 ], // [Left/Right, Top/Bottom]
        popupAnchor: [ 0, -50 ],
    });

    const supplyMarker = new L.Icon({
        iconUrl:
            "https://cdn1.iconfinder.com/data/icons/battle-royale-colored/80/drop-Crate-airDrop-fortnite-pubg-supply-512.png",
        iconSize: [ 50, 50 ],
        iconAnchor: [ 25, 50 ], // [Left/Right, Top/Bottom]
        popupAnchor: [ 0, -50 ],
    });

    const missionMarker = new L.Icon({
        iconUrl:
            "https://www.iconpacks.net/icons/1/free-target-icon-777-thumb.png",
        iconSize: [ 50, 50 ],
        iconAnchor: [ 25, 50 ], // [Left/Right, Top/Bottom]
        popupAnchor: [ 0, -50 ],
    });

    const playerMarker = new L.Icon({
        iconUrl:
            "https://cdn4.iconfinder.com/data/icons/small-n-flat/24/map-marker-512.png",
        iconSize: [ 50, 50 ],
        iconAnchor: [ 25, 50 ], // [Left/Right, Top/Bottom]
        popupAnchor: [ 0, -50 ],
    });


    const postNewMission = async (name, is_Human, is_Zombie, description, startTime, endTime, lat, lng) => {
        client.post(`/Games/Missions`, {
            name: name,
            is_Human_Visible: is_Human,
            is_Zombie_Visible: is_Zombie,
            description: description,
            startTime: startTime,
            endTime: endTime,
            lat: lat,
            lng: lng,
            gameId: gameId,
        })
            .then((res) => {
                setMissionList(
                    (prevstate) => {
                        return [
                            ...prevstate,
                            res.data,
                        ]
                    }
                )
            });
    }

    const postNewGravestone = async (graveDescription, lat, lng) => {
        client.post(`/Games/EmptyKill`, {
            timeOfDeath: new Date().toISOString(),
            biteCode: "",
            description: graveDescription,
            lat: lat,
            lng: lng,
            gameId: gameId,
            killerId: 0,
        })
            .then((res) => {
                setGravestoneList(
                    (prevstate) => {
                        return [
                            ...prevstate,
                            res.data,
                        ]
                    }
                )
            });
    }

    const MapEvents = () => {
        useMapEvents({
            click(e) {
                //Check which button is clicked
                switch (markerSelectedContext) {
                    case "gravestone":

                        let graveDescription = prompt("Enter Gravestone Description");
                        if (!graveDescription) {
                            break;
                        }
                        let gravestone = prompt("Enter Gravestone Description");
                        if (!graveDescription) {
                            break;
                        }

                        postNewGravestone(graveDescription, e.latlng.lat, e.latlng.lng);
                        setMarkerSelectedContext(null);

                        break;
                    case "mission":

                        let name = prompt("Enter Mission Name");
                        if (!name) {
                            break;
                        }

                        let description = prompt("Enter Mission Description");
                        if (!description) {
                            break;
                        }
                        postNewMission(name, true, true, description, new Date().toISOString(), new Date().toISOString(), e.latlng.lat, e.latlng.lng);
                        setMarkerSelectedContext(null);

                        break;
                    default:
                        return null;
                }
            },
        });
        return false;
    };

    const squadMemberMarker = new L.Icon({
        iconUrl:
            "https://upload.wikimedia.org/wikipedia/commons/thumb/8/88/Map_marker.svg/1334px-Map_marker.svg.png",
        iconSize: [ 30, 50 ],
        iconAnchor: [ 15, 50 ], // [Left/Right, Top/Bottom]
        popupAnchor: [ 0, -50 ],
    });

    //hooks
    const location = useGeoLocation();

    return (
        <>
            <div
                className={ `${ userContext.isHuman === false ?
                    "bg-red-900"
                    :
                    " bg-blue-500" } z-10 w-screen h-screen opacity-10 absolute pointer-events-none` }
            />

            <MapContainer
                className="z-0"
                ref={ mapRef }
                center={ [ 59.926118, 10.751181 ] }
                zoom={ 100 }
                scrollWheelZoom={ true }
            >
                <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />

                <MapEvents />

                { location.loaded && !location.error && (
                    <Marker
                        position={ [
                            location.coordinates.lat,
                            location.coordinates.lng,
                        ] }
                        icon={ playerMarker }
                    >
                        <Popup>
                            <div className="text-center">You are here</div>
                        </Popup>
                    </Marker>

                ) }

                { gravestoneList.map((gravestone, index) => (
                    <Marker
                        position={ [ gravestone.lat, gravestone.lng ] }
                        icon={ gravestoneMarker }
                        key={ index }
                        eventHandlers={ {
                            mouseover: (event) => event.target.openPopup(),
                            mouseout: (event) => event.target.closePopup(),
                            click: (event) => event.target.openPopup(),
                        } }
                    >
                        <Popup>{ gravestone.description }</Popup>
                    </Marker>
                )) }

                { missionList.map((mission, index) => (
                    <Marker
                        position={ [ mission.lat, mission.lng ] }
                        icon={ missionMarker }
                        key={ index }
                        eventHandlers={ {
                            mouseover: (event) => event.target.openPopup(),
                            mouseout: (event) => event.target.closePopup(),
                            click: (event) => event.target.openPopup(),
                        } }
                    >
                        <Popup>
                            <div className="text-center">
                                <strong>{ mission.name }</strong> <br />
                                { mission.description }
                            </div>
                        </Popup>
                    </Marker>
                )) }

                { (gameStartedContext === true && userContext.isHuman === true) &&

                    squadMarkers.map((member, index) => {

                        if (member.checkinLat != null && member.checkinLon != null && userContext.id != member.id) {

                            return (
                                <Marker
                                    position={ [ member.checkinLat, member.checkinLon ] }
                                    icon={ squadMemberMarker }
                                    key={ index }
                                    eventHandlers={ {
                                        mouseover: (event) => event.target.openPopup(),
                                        mouseout: (event) => event.target.closePopup(),
                                        click: (event) => event.target.openPopup(),
                                    } }
                                >
                                    <Popup>
                                        <div className="text-center">{ member.firstName }</div>
                                    </Popup>
                                </Marker>
                            )

                        }
                    }
                    ) }
            </MapContainer>
        </>
    );
};

export default Map
