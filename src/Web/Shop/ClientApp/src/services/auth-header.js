export default function authHeader() {
    const user = JSON.parse(localStorage.getItem('currentUser'));

    if (user && user.Token) {
        return { 'Authorization': 'Bearer ' + user.Token };
    } else {
        return {};
    }
}