const apiKey = process.env.REACT_APP_API_KEY

/**
 * 
 * Creates headers to access the API
 * @returns the API headers requirement
 */
export const createApiHeaders = () => {
return {
    'content-Type': 'application/json',
    'x-api-key': apiKey 
}
}

