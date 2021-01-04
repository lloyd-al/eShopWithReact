import React, { useState } from 'react';
import { SignIn, SignUp, ForgotPassword, ResetPassword } from '../../components/auth';


const AuthPage = params => {
    const [name, setName] = useState('signin');

    return (
        <>
            <div>
                {(() => {
                    switch (name) {
                        case "signin": return <SignIn setName={setName} handleClose={params.handleClose} />;
                        case "signup": return <SignUp setName={setName} handleClose={params.handleClose} />;
                        case "forgot": return <ForgotPassword setName={setName} handleClose={params.handleClose} />;
                        case "reset": return <ResetPassword setName={setName} handleClose={params.handleClose} />;
                        default: return <SignIn setName={setName} setName={setName} handleClose={params.handleClose} />;
                    }
                })()}
            </div>
        </>
    );
}

export default AuthPage;