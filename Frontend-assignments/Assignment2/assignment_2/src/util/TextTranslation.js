import { imagePath } from "../const/StorageKeys";

/**
 * A function that takes in a string and deconstructs it to images
 * 
 * @param {string[]} translationText  
 * @returns a list of images that contains the sign language path
 */
export const textTranslation = (translationText) => {
    return translationText.split(" ").reduce((prev,curr)=>{
        curr = curr.replace(/[^a-zA-Z]/g, '');
        for (let i of curr) {
            prev.push(imagePath + i + ".png");

        }
        return prev;
    },[])
   

}