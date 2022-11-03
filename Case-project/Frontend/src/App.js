import { BrowserRouter, Routes, Route } from "react-router-dom";
import "./App.css";
import AdminCreate from "./views/admin/AdminCreate";
import GameDetail from "./views/GameDetail";
import Landing from "./views/Landing";
import AdminPlayer from "./views/admin/AdminPlayer";
import NavTop from "./components/nav/NavTop";
import AdminEditPage from "./views/admin/AdminEditPage";
import { GetGameIdContext } from "./contexts/adminContext";
import { useState } from "react";


function App() {
    const [ gameIdContext, setGameIdContext ] = useState(null);
    return (
        <>
            <link
                rel="stylesheet"
                href="https://unpkg.com/leaflet@1.6.0/dist/leaflet.css"
                integrity="sha512-xwE/Az9zrjBIphAcBb3F6JVqxf46+CDLwfLMHloNu6KEQCAWi6HcDUbeOfBIptF7tcCzusKFjFw2yuvEpDL9wQ=="
                crossOrigin=""
            />

            <script
                src="https://unpkg.com/leaflet@1.6.0/dist/leaflet.js"
                integrity="sha512-gZwIG9x3wUXg2hdXF6+rVkLF/0Vi9U8D2Ntg4Ga5I5BZpVkVxlJWbSQtXPSiUTtC0TjtGOmxa1AJPuV0CPthew=="
                crossOrigin=""
            ></script>

            <BrowserRouter>
                <div className="App">
                    <GetGameIdContext.Provider value={ { gameIdContext, setGameIdContext } } >
                        <NavTop />
                        <Routes>
                            <Route path="/" element={ <Landing /> } />
                            <Route path="gamedetail/:gameId" element={ <GameDetail /> } />
                            <Route roles={ [ 'Admin' ] } path="create" element={ <AdminCreate /> } />
                            <Route path="editplayer/:id" element={ <AdminPlayer /> } />
                            <Route path="editgame/:id" element={ <AdminEditPage /> } />
                        </Routes>
                    </GetGameIdContext.Provider>
                </div>
            </BrowserRouter>

        </>
    );
}

export default App;
