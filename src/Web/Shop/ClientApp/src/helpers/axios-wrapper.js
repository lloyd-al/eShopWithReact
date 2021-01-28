import axios from 'axios';

import { AuthenticationService, baseAuthUrl } from '../services/auth-service';


export const AxiosWrapper = {
    get,
    post,
    put,
    delete: _delete
}

const axiosApiInstance = axios.create();

function get(url) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };
    return axiosApiInstance(url, requestOptions);
}

function post(apiUrl, body) {
    return axios({
        method: 'post',
        url: apiUrl,
        headers: { 'Content-Type': 'application/json', ...authHeader() },
        data: JSON.stringify(body),
    });
}

function put(url, body) {
    const requestOptions = {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', ...authHeader() },
        body: JSON.stringify(body)
    };
    return axiosApiInstance(url, requestOptions);
}

// prefixed with underscored because delete is a reserved word in javascript
function _delete(url) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader(url)
    };
    return axiosApiInstance(url, requestOptions);
}

// helper functions

function authHeader() {
    // return auth header with jwt if user is logged in and request is to the api url
    const user = AuthenticationService.userValue;
    //const user = JSON.parse(localStorage.getItem('currentUser'));
    const isLoggedIn = user && user.Token;
    if (isLoggedIn) {
        return { Authorization: `Bearer ${user.Token}` };
    } else {
        return {};
    }
}

//// Request interceptor for API calls
//axiosApiInstance.interceptors.request.use(
//    async config => {
//        const value = localStorage.getItem("refreshToken");
//        //const keys = JSON.parse(value)
//        config.headers = {
//            'Content-Type': 'application/json',
//            ...authHeader()
//        }
//        return config;
//    },
//    error => {
//        Promise.reject(error)
//    });


//response interceptor to refresh token on receiving token expired error
axios.interceptors.response.use(
    (response) => {
        // Return a successful response back to the calling service
        return response;
    },
    function (error) {
        // Return any error which is not due to authentication back to the calling service
        if (error.response.status !== 401) {
            return new Promise((resolve, reject) => {
                reject(error);
            });
        }

        // Logout user if token refresh didn't work or user is disabled
        if (error.config.url == '/refresh-token' || error.response.message == 'Account is disabled.') {

            //TokenStorage.clear();

            return new Promise((resolve, reject) => {
                reject(error);
            });
        }

        const originalRequest = error.config;
        let refreshToken = localStorage.getItem("refreshToken");
        if (
            refreshToken &&
            [401].includes(error.response.status) &&
            !originalRequest._retry
        ) {
            originalRequest._retry = true;
            return axios
                .post(`${baseAuthUrl}/refresh_token`, {  })
                .then((res) => {
                    if (res.status === 200) {
                        localStorage.setItem("token", res.data.token);
                        return axios(originalRequest);
                    }
                });
        }
        return Promise.reject(error);
    }
);