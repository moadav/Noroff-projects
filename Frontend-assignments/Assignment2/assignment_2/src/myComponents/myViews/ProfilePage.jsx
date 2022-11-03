import { useTranslationUser } from "../../context/UserContext";
import { myAuth } from "../../hoc/myAuth";
import ProfilePageActions from "../myProfilePage/ProfilePageActions";
import ProfilePageHistory from "../myProfilePage/ProfilePageHistory";

function ProfilePage() {
  /**
   * Gets the username from the usercontext state
   */
  const { translationUser } = useTranslationUser();
  return (
    <>
      <h4 className="welcomeUser">
        Welcome to profile {translationUser.username}
      </h4>
      <ProfilePageActions />
      <ProfilePageHistory translations={translationUser.translations} />
    </>
  );
}

export default myAuth(ProfilePage);
