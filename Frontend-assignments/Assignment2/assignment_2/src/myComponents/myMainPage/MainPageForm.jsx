import { useState } from "react";
import { textTranslation } from "../../util/TextTranslation";
import MainPageImage from "./MainPageImage";
import "../../myStyling/MainPageForm.css";
import { addTranslation } from "../../api/TranslationsApi";
import { useTranslationUser } from "../../context/UserContext";
import SaveState from "../../hook/SaveState";
import Form from "../myForm/Form";

const MainPageForm = () => {
  const [loading, setLoading] = useState(false);
  const [translation, setTranslation] = useState([]);
  const { translationUser, setTranslationUser } = useTranslationUser();

  /**
   * Function to take text from the form and add translation to the user api
   * 
   * @param {text} translationText
   * @returns saves the state context and keeps UI in sync
   */
  const onSubmit = async ({ translationText }) => {
    setLoading(true);
    //Sets the translation texts that is supposed to render
    setTranslation(textTranslation(translationText));
    const [error, updatedUser] = await addTranslation(
      translationUser,
      translationText
    );
    if (error !== null) return;

    //keeps the UI in sync
    SaveState(updatedUser);
    //updates the state context
    setTranslationUser(updatedUser);
    setLoading(false);
  };

  //the images that is going to render
  const imageList = translation.map((element, index) => (
    <MainPageImage
      source={element}
      index={index}
      key={index + "- " + element}
    />
  ));

  //the translation text input requirements
  const translationConfig = {
    required: true,
    maxLength: 40,
  };
  //error message rendering

  const errorMessage = (errors) => {
    if (!errors) return;
    if (!errors.translationText) return null;
    if (errors.translationText.type === "maxLength")
      return <span className="errorMessages">Text too long!</span>;
    if (errors.translationText.type === "required")
      return <span className="errorMessages">Text Required</span>;
  };

  return (
    <>
      <section className="translateTextSection">
        <Form
          config={translationConfig}
          onSubmit={onSubmit}
          text={"translationText"}
          subName={"Translate"}
          error={errorMessage}
          placeH={"What do you want to translate?"}
        />
      </section>
      {loading ? (
        <p> loading...</p>
      ) : (
        <section className="translationSection">{imageList}</section>
      )}
    </>
  );
};

export default MainPageForm;
