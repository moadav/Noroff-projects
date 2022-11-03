import { useForm } from "react-hook-form";
import "../../myStyling/Form.css";

function Form({ config, onSubmit, text, subName, error, placeH }) {
  //useForm hook to handle the form html tag
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();


  return (
    <div className="increaseWidth">
      <form className="loginForm" onSubmit={handleSubmit(onSubmit)}>
        <fieldset className="fieldset">
          <input
            type="text"
            name="name"
            placeholder={placeH}
            className="loginInput"
            {...register(text, config)}
            id={text}
          />
          <label htmlFor={text} className="username">
            {text}:
          </label>

        </fieldset>
        <img className="inputImage" src="Assets/Alien_waving.png" alt="alien waving small" />

        <button type="submit" name="submit" className="submitRound">
          {subName}
        </button>
      </form>
      {error(errors)}
    </div>
  );
}

export default Form;
