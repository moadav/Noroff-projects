/**
 * A function to save Storage with key and value
 * @param {string} Key key of the local storage 
 * @param {object} value the value saved
 */
export const saveStorage = (key, value) => {
    if (!key)
        throw new Error('No storage key provided');

    if (!value)
        throw new Error('No value provided for ', key);

    localStorage.setItem(key, JSON.stringify(value));
}

/**
 * A function that reads the storage and returns the data
 * 
 * @param {string} key  The key of the storage to read
 * @returns Json object containing the Data
 */
export const readStorage = (key) => {
    const data = localStorage.getItem(key)
    if (data)
        return JSON.parse(data);

    return null;
}
/**
 * a function that deletes the storage data
 * 
 * @param {*} key The key of the storage 
 */

export const deleteStorage = (key) => {
    localStorage.removeItem(key);
}