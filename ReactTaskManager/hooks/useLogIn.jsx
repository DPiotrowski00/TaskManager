export default function useLogIn() {
    async function validateLogIn(login, password) {
        const response = await fetch("https://localhost:7176/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                username: login,
                password: password
            })
        });
        const result = await response.json();
        return result;
    }

    async function tryRegister(login, password) {
        const response = await fetch("https://localhost:7176/login/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                username: login,
                password: password
            })
        });
        const result = await response.json();
        return result;
    }

    return { validateLogIn, tryRegister };
}