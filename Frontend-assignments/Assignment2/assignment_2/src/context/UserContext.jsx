import { useContext } from "react";
import { createContext, useState } from "react";
import { STORAGE_KEY_USER } from "../const/StorageKeys";
import { readStorage } from "../util/Storage";

/**
 * The context of the storage for the data to pass through
 */
const UserContext = createContext();

/**
 * 
 * A function that returns the context
 * 
 * @returns The context of translation user
 */
export const useTranslationUser = () => {
    return useContext(UserContext)
}

/**
 * 
 * A function that reads the user state and sends it to the children components
 * 
 * @param {Components} children components 
 * @returns A wrapper around child elements for state to provide
 */

const UserProvider = ({children}) => {
    const [translationUser, setTranslationUser] = useState(readStorage(STORAGE_KEY_USER));

    const state = {
        translationUser,
        setTranslationUser
    }

    return (
        <UserContext.Provider value={state}>
            {children}
        </UserContext.Provider>
    )
}
export default UserProvider;