import { BehaviorSubject } from 'rxjs';
import { AxiosWrapper } from '../helpers/axios-wrapper';

export const baseAuthUrl = 'http://localhost:44370/api/v1/user';
const currentUserSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('currentUser')));


export const AuthenticationService = {
    login,
    logout,
    refreshToken,
    register,
    verifyEmail,
    forgotPassword,
    validateResetToken,
    resetPassword,
    getById,
    update,
    delete: _delete,
    currentUser: currentUserSubject.asObservable(),
    get userValue() { return currentUserSubject.value }
};


function login(email, password) {
    return AxiosWrapper.post(`${baseAuthUrl}/authenticate`, { email, password })
        .then((response) => {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            // publish user to subscribers and start timer to refresh token
            localStorage.setItem('currentUser', JSON.stringify(response.data));
            currentUserSubject.next(response.data);
            startRefreshTokenTimer();
            return response.data;
        });
}

function logout() {
    // remove user from local storage to log user out, revoke token, stop refresh timer, publish null to user subscribers
    //AxiosWrapper.post(`${baseAuthUrl}/revoke-token`, {"refreshToken:});
    stopRefreshTokenTimer();
    localStorage.removeItem('currentUser');
    currentUserSubject.next(null);
}

function refreshToken() {
    return AxiosWrapper.post(`${baseAuthUrl}/refresh-token`, {})
        .then(user => {
            // publish user to subscribers and start timer to refresh token
            currentUserSubject.next(user);
            startRefreshTokenTimer();
            return user;
        });
}

function register(params) {
    return AxiosWrapper.post(`${baseAuthUrl}/register`, params);
}

function verifyEmail(token) {
    return AxiosWrapper.post(`${baseAuthUrl}/verify-email`, { token });
}

function forgotPassword(email) {
    return AxiosWrapper.post(`${baseAuthUrl}/forgot-password`, { email });
}

function validateResetToken(token) {
    return AxiosWrapper.post(`${baseAuthUrl}/validate-reset-token`, { token });
}

function resetPassword({ token, oldpassword, newpassword, confirmPassword }) {
    return AxiosWrapper.post(`${baseAuthUrl}/reset-password`, { token, oldpassword, newpassword, confirmPassword });
}

function getById(id) {
    return AxiosWrapper.get(`${baseAuthUrl}/${id}`);
}


function update(id, params) {
    return AxiosWrapper.put(`${baseAuthUrl}/${id}`, params)
        .then(user => {
            // update stored user if the logged in user updated their own record
            if (user.id === currentUserSubject.value.id) {
                // publish updated user to subscribers
                localStorage.setItem('currentUser', JSON.stringify(user));
                user = { ...currentUserSubject.value, ...user };
                currentUserSubject.next(user);
            }
            return user;
        });
}

// prefixed with underscore because 'delete' is a reserved word in javascript
function _delete(id) {
    return AxiosWrapper.delete(`${baseAuthUrl}/${id}`)
        .then(x => {
            // auto logout if the logged in user deleted their own record
            if (id === currentUserSubject.value.id) {
                logout();
            }
            return x;
        });
}

// helper functions

let refreshTokenTimeout;

function startRefreshTokenTimer() {
    // parse json object from base64 encoded jwt token

    const jwtToken = JSON.parse(atob(currentUserSubject.value.token.split('.')[1]));

    // set a timeout to refresh the token a minute before it expires
    const expires = new Date(jwtToken.exp * 1000);
    const timeout = expires.getTime() - Date.now() - (60 * 1000);
    refreshTokenTimeout = setTimeout(refreshToken, timeout);
}

function stopRefreshTokenTimer() {
    clearTimeout(refreshTokenTimeout);
}

