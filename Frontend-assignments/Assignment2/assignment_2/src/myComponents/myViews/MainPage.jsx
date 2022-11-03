import { myAuth } from "../../hoc/myAuth";
import MainPageForm from "../myMainPage/MainPageForm";

function MainPage() {
  return <MainPageForm />;
}

export default myAuth(MainPage);
