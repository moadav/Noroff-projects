import { loginTranslationUser } from "../../api/TranslationUserApi";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useTranslationUser } from "../../context/UserContext";
import saveState from "../../hook/SaveState";
import Form from "../myForm/Form";

function LoginForm() {
  const [loading, setLoading] = useState(false);
  const { translationUser, setTranslationUser } = useTranslationUser();
  const navigate = useNavigate();




  //side effects
  useEffect(() => {
    if (translationUser !== null) navigate("/MainPage");
  }, [translationUser, navigate]);

  //event handler
  const onSubmit = async ({ username}) => {

    setLoading(true);
    const [error, translationUserResponse] = await loginTranslationUser(
      username
    );
    if (error !== null) {
     return;
    }
    if (
      translationUserResponse !== null &&
      translationUserResponse.length !== 0
    ) {
      saveState(translationUserResponse);
      setTranslationUser(translationUserResponse);
    }
    setLoading(false);
  };
  const usernameConfig = {
    required: true,
    minLength: 3,
  };

  //render function

  const errorMessage = (errors) => {
    if (!errors) return
    if (!errors.username) return null;
    if (errors.username.type === "minLength")
      return <span className="errorMessages">Username Too short!</span>;
    if (errors.username.type === "required")
      return <span className="errorMessages">Username Required</span>;
  };

  return (
    <>
      <Form config={usernameConfig} onSubmit={onSubmit} text={"username"} subName={"Login"}
       error={errorMessage} placeH = {"Whats your username?"} />
      {loading && <p> loading...</p>}
    </>
  );
}

export default LoginForm;
