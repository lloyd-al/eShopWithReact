import React from "react";
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { Nav } from 'react-bootstrap'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { AuthenticationService } from '../../services';

import './auth-style.css';


const SignIn = params => {

    const initialValues = {
        email: '',
        password: ''
    };

    const validationSchema = Yup.object().shape({
        email: Yup.string()
            .email('Email is invalid')
            .required('Email is required'),
        password: Yup.string()
            .min(8, "Password is too short - should be 8 chars minimum.")
            .max(16)
            .matches(/^(?=.*[a-z])(?=.*[A-Z])(?=.*d)[a-zA-Zd]/, "Password must contain a number.")
            .required('Password is required')
    });

    function onSubmit({ email, password }, { setSubmitting }) {
        AuthenticationService.login(email, password)
            .then(() => {
                params.handleClose();
            })
            .catch(error => {
                setSubmitting(false);
                toast.error("Incorrect Password");
            });
    }

    return (
        <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit}>
            {({ errors, touched, isSubmitting }) => (
                <div id="logreg-forms">
                    <ToastContainer />
                    <Form className="form-signin">
                        <h1 className="h3 mb-3 font-weight-normal label-header"> Sign in</h1>
                        <div className="social-login">
                            <button className="btn facebook-btn social-btn" type="button"><span><i className="fab fa-facebook-f"></i> Sign in with Facebook</span> </button>
                            <button className="btn google-btn social-btn" type="button"><span><i className="fab fa-google-plus-g"></i> Sign in with Google+</span> </button>
                        </div>
                        <p className="label-header"> OR  </p>
                        <Field name="email" type="text" className={'form-control' + (errors.email && touched.email ? ' is-invalid' : '')} placeholder="" />
                        <ErrorMessage name="email" component="div" className="invalid-feedback" />
                        <Field name="password" type="password" className={'form-control' + (errors.email && touched.email ? ' is-invalid' : '')} placeholder="" />
                        <ErrorMessage name="password" component="div" className="invalid-feedback" />

                        <div className="btn-group btn-block">
                            <button type="submit" disabled={isSubmitting} className="btn btn-success">
                            {isSubmitting && <span className="spinner-border spinner-border-sm mr-1"></span>}
                            <i className="fas fa-sign-in-alt"></i> Sign in
                            </button>
                            <button type="button" className="btn btn-secondary" onClick={() => params.handleClose()}>Cancel</button>
                        </div>
                        <Nav.Link className="btn btn-link" href="/signup">Forgot Password?</Nav.Link>
                        <hr />
                        <button className="btn btn-primary btn-block" type="button" id="btn-signup" onClick={() => params.setName("signup")}>
                            <i className="fas fa-user-plus"></i> Sign up New Account
                        </button>
                    </Form>
                </div>
            )}
        </Formik>
    );
}

export default SignIn;