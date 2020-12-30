import axios from 'axios';

export const catalogInstance = axios.create({
    baseURL: 'http://localhost:44367/api/',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'text/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Origin, Content-Type, Accept'
    }
});


export const authInstance = axios.create({
    baseURL: 'http://localhost:44370/api/',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'text/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Origin, Content-Type, Accept'
    }
});

