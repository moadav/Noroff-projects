import UserProvider from "./UserContext"



/**
 * 
 * A function to provide the state for the child components
 * 
 * @param {component} children Child components 
 * @returns UserProvder for children to get the state if needed
 */
const AppContext = ({children}) => {

   
    return(
        < UserProvider>
        {children}
        </UserProvider>
    )
}

export default AppContext