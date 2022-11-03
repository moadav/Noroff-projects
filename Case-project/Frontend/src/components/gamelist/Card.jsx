import { Link } from "react-router-dom";

const Card = ({ id, title, description, gameState, image, playerCount, StartTime, EndTime }) => {



    function getStatusColor(status) {
        if (status == 'Complete') {
            return 'bg-red-500';
        }
        if (status == 'Registration') {
            return 'bg-yellow-500';
        }

        if (status == 'In Progress') {
            return 'bg-green-500';
        }
    };

    return (
        <>
            <Link to={ `gamedetail/${ id }`}>
      
                <div className="max-w-md rounded overflow-hidden shadow-lg hover:ring hover:ring-indigo-500 mt-20">
                    <img className="w-full h-60" 
                    src={image ? image : ""}
                    onError={({ currentTarget }) => {
                        currentTarget.onerror = null; // prevents looping
                        currentTarget.src="/img/zombie-apocalypse.jpg";
                      }} 
                      alt={description} />
                    <div className="px-6 py-4">
                        <div className="font-bold text-xl mb-2">{ title }</div>
                        <p className="text-gray-700 text-base">{ description }

                        </p>
                    </div>

                    <div className=" px-6 pt-4 pb-2">
                        <span className="text-xs inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2 relative">

                            <span className={ `inline-block h-3 w-3 rounded-full border-0 border-gray-500 mr-1  ${ getStatusColor(gameState) }` } />
                            { gameState }
                        </span>


                        <span className="text-xs inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">
                            👥 { playerCount }
                        </span>


                        <span className="text-xs inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">
                            🗓 { StartTime }
                        </span>

                    </div>
                </div>
            </Link>
        </>

    );
}

export default Card