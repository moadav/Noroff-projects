import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import keycloak from "../../keycloak";

import { GiRaiseZombie } from "react-icons/gi";
import { GrAddCircle } from "react-icons/gr";
import { FaUser } from "react-icons/fa";
import { useLocation } from "react-router-dom";
import { GetGameIdContext } from "../../contexts/adminContext";
import { useContext } from "react";

const NavTop = () => {
    const [isNavOpen, setIsNavOpen] = useState(false);
    const location = useLocation();
    const [roles, setRoles] = useState("NoUser");
    const { gameIdContext } = useContext(GetGameIdContext);

    useEffect(() => {
        if (keycloak.tokenParsed) {
            let roller = keycloak.tokenParsed.roles[0];
            setRoles(roller);
        }
    }, []);

    return (
        <>
            {!(
                location.pathname === `/gamedetail/${gameIdContext}` &&
                roles !== "Admin"
            ) && (
                <div className="flex items-center justify-between border-gray-400 py-6 bg-indigo-500 px-8 absolute z-50 h-20 w-screen">
                    <div className="flex z-50">
                        <Link to="/">
                            <img
                                className=" mx-4 absolute left-12 top-0"
                                src="/img/—Pngtree—plants vs. zombies pictures_5751838.png"
                                alt="logo"
                                width={"80"}
                            />
                            <p
                                id="logoname"
                                className="mt-2 ml-28 text-white lg:text-2xl"
                            >
                                Human vs Zombie
                            </p>
                        </Link>
                    </div>

                    <nav>
                        <section className="MOBILE-MENU flex lg:hidden">
                            <div
                                className="HAMBURGER-ICON space-y-2"
                                onClick={() => setIsNavOpen((prev) => !prev)}
                            >
                                <span className="block h-0.5 w-8 animate-pulse bg-white"></span>
                                <span className="block h-0.5 w-8 animate-pulse bg-white"></span>
                                <span className="block h-0.5 w-8 animate-pulse bg-white"></span>
                            </div>

                            <div
                                className={
                                    isNavOpen ? "showMenuNav" : "hideMenuNav"
                                }
                            >
                                <div
                                    className="absolute top-0 right-0 px-8 py-8"
                                    onClick={() => setIsNavOpen(false)}
                                >
                                    <svg
                                        className="h-8 w-8 text-gray-800"
                                        viewBox="0 0 24 24"
                                        fill="none"
                                        stroke="currentColor"
                                        strokeWidth="2"
                                        strokeLinecap="round"
                                        strokeLinejoin="round"
                                    >
                                        <line x1="18" y1="6" x2="6" y2="18" />
                                        <line x1="6" y1="6" x2="18" y2="18" />
                                    </svg>
                                </div>

                                {/* hamburger bar */}
                                {keycloak.authenticated &&
                                keycloak.tokenParsed.roles[0] === "Admin" &&
                                location.pathname !== "/" &&
                                location.pathname !== "/create" ? (
                                    <ul className="flex flex-col items-center justify-between min-h-[250px]">
                                        <li className="flex mb-8">
                                            <FaUser size={25} />
                                            {
                                                keycloak.tokenParsed
                                                    .preferred_username
                                            }
                                        </li>
                                        <Link to={`editgame/${gameIdContext}`}>
                                            <li
                                                className=" border-blue-400 my-2 uppercase w-20 bg-slate-400 focus:bg-blue-400 text-white text-center font-bold py-2 border-b-4 focus:border-blue-700 hover:border-blue-500 rounded"
                                                onClick={() =>
                                                    setIsNavOpen(false)
                                                }
                                            >
                                                Game
                                            </li>
                                        </Link>
                                        <Link
                                            to={`editplayer/${gameIdContext}`}
                                        >
                                            <li
                                                className=" border-blue-400 my-2 uppercase w-20 bg-slate-400 focus:bg-blue-400 text-white text-center font-bold py-2 border-b-4 focus:border-blue-700 hover:border-blue-500 rounded"
                                                onClick={() =>
                                                    setIsNavOpen(false)
                                                }
                                            >
                                                Player
                                            </li>
                                        </Link>

                                        <Link
                                            to={`gamedetail/${gameIdContext}`}
                                        >
                                            <li
                                                className=" border-blue-400 my-2 uppercase w-20 bg-slate-400 focus:bg-blue-400 text-white text-center font-bold py-2 border-b-4 focus:border-blue-700 hover:border-blue-500 rounded"
                                                onClick={() =>
                                                    setIsNavOpen(false)
                                                }
                                            >
                                                Map
                                            </li>
                                        </Link>

                                        <li
                                            onClick={() => keycloak.logout()}
                                            className="uppercase text-white mt-12 bg-purple-800 hover:bg-purple-600 focus:ring-4 focus:outline-none focus:ring-purple-300 font-medium rounded-lg px-5 py-2.5 text-center"
                                        >
                                            Logout
                                        </li>
                                    </ul>
                                ) : (
                                    <ul className="flex flex-col items-center justify-between min-h-[250px] ">
                                        {keycloak.authenticated &&
                                            keycloak.tokenParsed.roles[0] ===
                                                "Admin" && (
                                                <>
                                                    <li
                                                        className=" border-blue-400 my-2 uppercase p-2 bg-slate-400 focus:bg-blue-400 text-white text-center font-bold py-2 border-b-4 focus:border-blue-700 hover:border-blue-500 rounded"
                                                        onClick={() =>
                                                            setIsNavOpen(false)
                                                        }
                                                    >
                                                        <Link to={"create"}>
                                                            Create Game
                                                        </Link>
                                                    </li>
                                                </>
                                            )}

                                        {!keycloak.authenticated && (
                                            <li
                                                onClick={() => keycloak.login()}
                                                className="uppercase text-white mt-12 bg-purple-800 hover:bg-purple-600 focus:ring-4 focus:outline-none focus:ring-purple-300 font-medium rounded-lg px-5 py-2.5 text-center "
                                            >
                                                Login
                                            </li>
                                        )}

                                        {keycloak.authenticated && (
                                            <>
                                                <li className="flex uppercase text-bold">
                                                    {" "}
                                                    <FaUser size={25} />{" "}
                                                    {
                                                        keycloak.tokenParsed
                                                            .preferred_username
                                                    }
                                                </li>

                                                <li
                                                    onClick={() =>
                                                        keycloak.logout()
                                                    }
                                                    className="uppercase text-white mt-12 bg-purple-800 hover:bg-purple-600 focus:ring-4 focus:outline-none focus:ring-purple-300 font-medium rounded-lg px-5 py-2.5 text-center"
                                                >
                                                    Logout
                                                </li>
                                            </>
                                        )}
                                    </ul>
                                )}
                            </div>
                        </section>

                        {/* Hvis brukeren er admin og ikke i landing page viser det en annerledes navbar - Må endre user roles tilbake til admin. Isteden for pathname !== / kan kanskje endres til pathname === 'admin' ? */}
                        {keycloak.authenticated &&
                        keycloak.tokenParsed.roles[0] === "Admin" &&
                        location.pathname !== "/" &&
                        location.pathname !== "/create" ? (
                            <>
                                <div className="DESKTOP-MENU hidden  space-x-8 lg:flex">
                                    <Link to={`editgame/${gameIdContext}`}>
                                        <button className=" border-blue-400 my-2 uppercase w-20 bg-slate-400 focus:bg-blue-400 text-white text-center font-bold py-2 border-b-4 focus:border-blue-700 hover:border-blue-500 rounded">
                                            Game
                                        </button>
                                    </Link>
                                    <Link to={`editplayer/${gameIdContext}`}>
                                        <button className=" border-blue-400 my-2 uppercase w-20 bg-slate-400 focus:bg-blue-400 text-white text-center font-bold py-2 border-b-4 focus:border-blue-700 hover:border-blue-500 rounded">
                                            Player
                                        </button>
                                    </Link>
                                    <Link to={`gamedetail/${gameIdContext}`}>
                                        <button className=" border-blue-400 my-2 uppercase w-20 bg-slate-400 focus:bg-blue-400 text-white text-center font-bold py-2 border-b-4 focus:border-blue-700 hover:border-blue-500 rounded">
                                            Map
                                        </button>
                                    </Link>
                                    <span className="flex text-white p-2 my-2 uppercase">
                                        {" "}
                                        <FaUser size={25} />{" "}
                                        {
                                            keycloak.tokenParsed
                                                .preferred_username
                                        }{" "}
                                    </span>
                                    <div>
                                    <button
                                        className="text-white hover:bg-red-700 rounded-lg my-2 p-2 bg-blue-400"
                                        onClick={() => keycloak.logout()}
                                    >
                                        Logout
                                    </button>
                                    </div>
                                </div>
                            </>
                        ) : (
                            <div className="DESKTOP-MENU hidden  space-x-8 lg:flex">
                                {!keycloak.authenticated && (
                                    <>
                                        <button
                                            onClick={() => keycloak.login()}
                                            className="flex text-white hover:bg-green-400 rounded-lg p-2 bg-blue-400"
                                        >
                                            <FaUser size={25} /> Login
                                        </button>
                                    </>
                                )}

                                {keycloak.authenticated && (
                                    <>
                                        <span className="flex text-white p-2 uppercase">
                                            {" "}
                                            <FaUser size={25} />{" "}
                                            {
                                                keycloak.tokenParsed
                                                    .preferred_username
                                            }{" "}
                                        </span>
                                    </>
                                )}

                                {/* Viser create game i navbar hvis brukeren er admin - Må endre user roles tilbake til admin */}
                                {keycloak.authenticated &&
                                    keycloak.tokenParsed.roles[0] ===
                                        "Admin" && (
                                        <div className="flex">
                                            <button className="flex text-white hover:bg-purple-600 rounded-lg p-2 bg-blue-400">
                                            <GrAddCircle className="m-1 text-white" />
                                                <Link to={"create"}>
                                                    Create Game
                                                </Link>
                                            </button>
                                        </div>
                                    )}

                                {keycloak.authenticated && (
                                    <>
                                        <button
                                            className="text-white hover:bg-red-700 rounded-lg p-2 bg-blue-400"
                                            onClick={() => keycloak.logout()}
                                        >
                                            Logout
                                        </button>
                                    </>
                                )}
                            </div>
                        )}
                    </nav>

                    <style>
                        {`
                        .hideMenuNav {
                        display: none;
                        }
                        .showMenuNav {
                        display: block;
                        position: absolute;
                        width: 100%;
                        height: 100vh;
                        top: 0;
                        left: 0;
                        background: white;
                        z-index: 10;
                        display: flex;
                        flex-direction: column;
                        justify-content: space-evenly;
                        align-items: center;
                        }`}
                    </style>
                </div>
            )}
        </>
    );
};

export default NavTop;
