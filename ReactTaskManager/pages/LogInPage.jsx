import LogInForm from "../components/LogInForm"

export default function LogInPage({ onLogIn }) {

    function onLogInSucceeded(username) {
        onLogIn(username);
    }

    return (
        <LogInForm onLogInSucceeded={onLogInSucceeded} />
    );
}