import { createContext } from "react";

export const GameContext = createContext(null);
export const GameStartedContext = createContext(false); 
export const GameTitleContext = createContext(true);
export const SquadListContext = createContext(false);
export const GameChatContext = createContext();
export const GameBiteCodeContext = createContext();

//////////TEST

// export const MyContext = createContext();

// export const useMyProvider = () => {
//     return useContext(MyContext); // { user, setUser }
// }

// const MyProvider = props => {
//     const [gameStartedContext, setGameStartedContext] = useState(false);
//     const [gameTitleContext, setGameTitleContext] = useState(true);

//     return (
//         <MyContext.Provider
//             value={{
//                 value: [gameStartedContext, setGameStartedContext],
//                 value2: [gameTitleContext, setGameTitleContext]
//             }}
//         >

//             {props.children}
//         </MyContext.Provider>
//     );
// }

// export default MyProvider