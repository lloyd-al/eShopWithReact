import React from 'react';

import './custom-button-style.css';

const CustomButton = ({ children, isSignIn, inverted, ...otherProps }) => (
    <button
        className={`${inverted ? 'inverted' : ''} ${isSignIn ? 'google-sign-in' : ''
            } custom-button`}
        {...otherProps}
    >
        {children}
    </button>
);

export default CustomButton;