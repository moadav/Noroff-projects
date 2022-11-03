import "../../myStyling/LoginPage.css";
import LoginForm from "../myLoginPage/LoginForm";

function LoginPage() {
  return (
    <article>
      <img className="movingAlien" src='Assets/Alien_waving.png'  alt="Moving-alien"/>
      <section className="centerInput">
        <p className="frontPageLower">Get Started Today!</p>
       <LoginForm/>
      </section>
    </article>
  );
}

export default LoginPage;
