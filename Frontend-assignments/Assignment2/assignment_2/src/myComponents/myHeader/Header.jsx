import "../../myStyling/Header.css";


function Header() {
  return (
    <header>
      <section className="h1title">
        <img className="alienLogo alienTheme" src='Assets/Alien_waving.png' alt="alien waving" />
        <h1 className="appTheme">Lost in translation</h1>
      </section>
    </header>
  );
}

export default Header;