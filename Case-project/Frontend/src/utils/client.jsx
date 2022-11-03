import axios from "axios";
import keycloak from "../keycloak";

export const client = axios.create({
    baseURL: "https://api-hvz.azurewebsites.net/api/v1"
});

