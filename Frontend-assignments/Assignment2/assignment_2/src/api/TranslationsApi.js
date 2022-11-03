import { createApiHeaders } from "./HeaderApi";

const apiUrl = process.env.REACT_APP_API_URL;

/**
 * A function to add the translation in the api
 * 
 * @param {object} user the user object 
 * @param {any[]} translation the list of translations 
 * @returns error if function fails or the response if succeed
 */
export const addTranslation = async (user, translation) => {
    try {
        const response = await fetch(`${apiUrl}/${user.id}`, {
            method: 'PATCH',
            headers: createApiHeaders(),
            body: JSON.stringify({
                translations: [...user.translations,translation]
            })
        })

        if (!response.ok)
            throw new Error("Could not find user with id: " + user);

        const result = await response.json();
        return [null, result]
    } catch (error) {
        return [error.message, null]
    }

}
/**
 * A function to clear all translations made by the user
 * Uses a PATCH call to the api to change the list to an empty array []
 * 
 * @param {Number} userId 
 * @returns error if function does not succeed or the response data
 */
export const clearTranslationHistory = async (userId) => {
    try {
        const response = await fetch(`${apiUrl}/${userId}`, {
            method: 'PATCH',
            headers: createApiHeaders(),
            body: JSON.stringify({
                translations: []
            })
        })

        if (!response.ok)
            throw new Error("Could not find user with id: " + userId);

        const result = await response.json();
        return [null, result]

    } catch (error) {
        return [error.message, null]
    }

}