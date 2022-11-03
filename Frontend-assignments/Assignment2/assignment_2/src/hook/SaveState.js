import { STORAGE_KEY_USER } from "../const/StorageKeys";
import { saveStorage } from "../util/Storage";


/**
 * function to save the state
 */
const SaveState = (updatedUser) => {
 //keeps the UI in sync
 saveStorage(STORAGE_KEY_USER, updatedUser);



}

export default SaveState;