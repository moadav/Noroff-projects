import { useTranslationUser } from "../../context/UserContext";
import { deleteStorage, saveStorage } from "../../util/Storage";
import { STORAGE_KEY_USER } from "../../const/StorageKeys";
import { clearTranslationHistory } from "../../api/TranslationsApi";
import "../../myStyling/ProfilePage.css";

const ProfilePageActions = () => {
  /**
   * Gets the 10 latest elements and returns li elements
   */
  const { translationUser, setTranslationUser } = useTranslationUser();

  /**
   * A function to handle the logout.
   * Here the storage state is deleted and translation user is set to null
   */
  const handleLogout = () => {
    if (window.confirm("Do you really want to logout?")) {
      deleteStorage(STORAGE_KEY_USER);
      setTranslationUser(null);
    }
  };

  /**
   *
   * A function to clear all translations by the user.
   * Here the function does a PATCH call to the api with an empty array
   * @returns null if error
   */
  const handleClearTranslation = async () => {
    if (!window.confirm("Are you sure?")) return;

    const [clearError] = await clearTranslationHistory(translationUser.id);

    if (clearError !== null) {
      return;
    }

    const updatedUser = {
      ...translationUser,
      translations: [],
    };
    saveStorage(STORAGE_KEY_USER, updatedUser);
    setTranslationUser(updatedUser);
  };
  return (
    <section className="profileButtons">
      <button onClick={handleClearTranslation}>Clear History</button>

      <button onClick={handleLogout}>Log out</button>
    </section>
  );
};

export default ProfilePageActions;
