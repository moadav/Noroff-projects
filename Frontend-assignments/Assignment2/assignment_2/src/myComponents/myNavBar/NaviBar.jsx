import { NavLink } from "react-router-dom";
import { useTranslationUser } from "../../context/UserContext";
import Header from "../myHeader/Header";
import "../../myStyling/NaviBar.css";

const Navbar = () => {
  const { translationUser } = useTranslationUser();
  return (
    <nav className="navigationBar">
      {translationUser !== null ? (
        <>
          <Header />

          <ul className="navigations">
            <li>
              <NavLink to={"/MainPage"}> MainPage </NavLink>
            </li>
            <li>
              <NavLink to={"/ProfilePage"}> ProfilePage </NavLink>
            </li>
          </ul>
        </>
      ) : (
        <Header />
      )}
    </nav>
  );
};
export default Navbar;
