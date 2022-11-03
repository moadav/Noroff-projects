import ProfilePageTranslationItem from "./ProfilePageTranslationItem";
import "../../myStyling/ProfilePage.css";
const ProfilePageHistory = ({ translations }) => {
  /**
   * Gets the 10 latest elements and returns li elements
   */
  const translationList = translations
    .slice(-10)
    .map((translation, index) => (
      <ProfilePageTranslationItem
        key={index + "_" + translation}
        item={translation}
      />
    ));
  return (
    <>
      <h4 className="profileTitle">Last ten translations</h4>

      <ol className="translationListSection">{translationList}</ol>
    </>
  );
};

export default ProfilePageHistory;
