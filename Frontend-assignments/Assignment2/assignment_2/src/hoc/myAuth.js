import { Navigate } from "react-router-dom"
import { useTranslationUser } from "../context/UserContext"

export const myAuth = Component => props => {
    const { translationUser } = useTranslationUser()

    if (translationUser !== null)
        return <Component {...props} />

    return <Navigate to="/" />
}
