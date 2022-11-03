import { useContext } from "react";
import { MarkerSelectedContext } from "../../contexts/adminContext";
import keycloak from "../../keycloak";

const AdminSideBar = () => {
    const { markerSelectedContext, setMarkerSelectedContext } = useContext(
        MarkerSelectedContext
    );

    return (

        <>
            {(keycloak.authenticated && keycloak.tokenParsed.roles[0] === "Admin") &&
            
                <div className="absolute z-30 left-0 h-screen w-14 bg-indigo-500 flex flex-col">
                    <button onClick={() => setMarkerSelectedContext("gravestone")}>
                        <div
                            className={`${markerSelectedContext === "gravestone"
                                ? "bg-indigo-200"
                                : ""
                                } w-12 h-12 mt-20  mx-auto rounded-lg `}
                        >
                            <img src="https://www.shareicon.net/download/2015/09/26/107761_dead_512x512.png" />
                        </div>
                    </button>
                    <button onClick={() => setMarkerSelectedContext("mission")}>
                        <div
                            className={`${markerSelectedContext === "mission"
                                ? "bg-indigo-200"
                                : ""
                                } w-12 h-12 mt-6 mx-auto rounded-lg`}
                        >
                            <img src="https://www.iconpacks.net/icons/1/free-target-icon-777-thumb.png" />
                        </div>
                    </button>
                </div>

            }
        </>


    );
};

export default AdminSideBar;
