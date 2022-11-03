import './App.css';
import LoginPage from './myComponents/myViews/LoginPage';
import MainPage from './myComponents/myViews/MainPage';
import ProfilePage from './myComponents/myViews/ProfilePage';
import NavBar from './myComponents/myNavBar/NaviBar';
import {
  BrowserRouter,
  Routes,
  Route
} from 'react-router-dom'


function App() {
    /* Here lies the Navbar and Routes to different part of the websites*/

  return (
    <BrowserRouter> 
        <NavBar/>
        <Routes>
          <Route path="/" element={<LoginPage />} />
          <Route path="/MainPage" element={<MainPage />} />
          <Route path="/ProfilePage" element={<ProfilePage />} />
        </Routes>
    </BrowserRouter>
  );
}

export default App;
