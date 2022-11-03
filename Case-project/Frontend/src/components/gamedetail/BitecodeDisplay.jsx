import { useContext } from "react";
import { UserContext } from "../../contexts/userContext";

const BitecodeDisplay = () => {

    const { userContext } = useContext(UserContext)

    return (
        <div className="w-screen absolute z-50 bottom-0 mb-24 drop-shadow-lg">
            <div className="w-11/12 mx-auto py-4 rounded-lg bg-white">
                <p className="text-5xl text-center">{userContext.biteCode}</p>
            </div>
            <div className="flex justify-between">
                <div className="w-20 mx-auto"></div>
                <div className="w-20 mx-auto"></div>
                <div className="w-20 mx-auto">
                    <div className="overflow-hidden">
                        <div className="mx-auto h-4 w-4 bg-white -rotate-45 transform origin-top-left"></div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default BitecodeDisplay;