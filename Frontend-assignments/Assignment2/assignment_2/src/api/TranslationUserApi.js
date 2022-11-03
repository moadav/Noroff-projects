import { createApiHeaders } from './HeaderApi';
const apiUrl = process.env.REACT_APP_API_URL;

/**
 * A function that fetches a user with a query
 * 
 * @param {string} username a string value
 * @returns the data or an error if function fails
 */
const checkForTranslationUser = async (username) => {
    try {


        const response = await fetch(`${apiUrl}?username=${username}`);
        if (!response.ok) {
            throw new Error("Could not find user");

        }

        const data = await response.json();
        return [null, data]

    } catch (error) {
        return [error.message, []]

    }
}

/**
 * 
 * A function that does a POST call to the api and creates a new User object
 * @param {string} username 
 * @returns reponse data or an error specifying what happend
 */
const createTranslationUser = async (username) => {
    try {
        const response = fetch(apiUrl, {
            method: 'POST',
            headers: createApiHeaders(),
            body: JSON.stringify({
                username,
                translations: []

            })
        });
        if (!response.ok)
            throw new Error("Could not create user with username: " + username);

        const data = await response.json();
        return [null, data]

    } catch (error) {
        return [error.message, []]

    }
}

/**
 * A function that returns an user object if it exists or creates a new user
 * depending on if the user accepts the request
 * 
 * @param {string} username 
 * @returns if the user exist returns the user object else returns an empty array or
 *  creates the user depending on if the user accepts the register
 */
export const loginTranslationUser = async (username) => {
    const [error, translationUser] = await checkForTranslationUser(username);

    if (error !== null) {
        return [error, null]
    } else if (translationUser.length > 0) {
        return [null, translationUser.pop()];
    }

    if (window.confirm('User does not exist\nDo you want to create new User?')) {
        await createTranslationUser(username);
    }

    return [null, []]

}