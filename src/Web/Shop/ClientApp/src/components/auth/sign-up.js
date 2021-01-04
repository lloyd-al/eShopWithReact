import React from 'react';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { Nav } from 'react-bootstrap'

import './auth-style.css';

const SignUp = params => {
    const initialValues = {
        title: '',
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        confirmPassword: '',
        acceptTerms: false
    };

    const validationSchema = Yup.object().shape({
        firstName: Yup.string()
            .required('First Name is required'),
        lastName: Yup.string()
            .required('Last Name is required'),
        email: Yup.string()
            .email('Email is invalid')
            .required('Email is required'),
        password: Yup.string()
            .min(8, "Password is too short - should be 8 chars minimum.")
            .max(16)
            .matches(/^(?=.*[a-z])(?=.*[A-Z])(?=.*d)[a-zA-Zd]/, "Password must contain a number.")
            .required('Password is required'),
        confirmPassword: Yup.string()
            .oneOf([Yup.ref('password'), null], 'Passwords must match')
            .required('Confirm Password is required'),
        acceptTerms: Yup.bool()
            .oneOf([true], 'Accept Terms & Conditions is required')
    });

    return (
        <Formik initialValues={initialValues} validationSchema={validationSchema} >
            {({ errors, touched, isSubmitting }) => (
                <div id="logreg-forms">
                    <Form className="form-signup">
                        <h1 className="h3 mb-3 font-weight-normal label-header"> Sign in</h1>
                        <div className="social-login">
                            <button className="btn facebook-btn social-btn" type="button"><span><i className="fab fa-facebook-f"></i> Sign up with Facebook</span> </button>
                            <button className="btn google-btn social-btn" type="button"><span><i className="fab fa-google-plus-g"></i> Sign up with Google+</span> </button>
                        </div>

                        <p className="label-header">OR</p>
                        <Field name="firstName" type="text" placeholder="Firstname" className={'form-control' + (errors.firstName && touched.firstName ? ' is-invalid' : '')} />
                        <ErrorMessage name="firstName" component="div" className="invalid-feedback" />

                        <Field name="lastName" type="text" placeholder="Lastname" className={'form-control' + (errors.lastName && touched.lastName ? ' is-invalid' : '')} />
                        <ErrorMessage name="lastName" component="div" className="invalid-feedback" />

                        <Field name="email" type="text" placeholder="Email" className={'form-control' + (errors.email && touched.email ? ' is-invalid' : '')} />
                        <ErrorMessage name="email" component="div" className="invalid-feedback" />

                        <Field name="password" type="password" placeholder="Password" className={'form-control' + (errors.password && touched.password ? ' is-invalid' : '')} />
                        <ErrorMessage name="password" component="div" className="invalid-feedback" />

                        <Field name="confirmPassword" type="password" placeholder="Confirm Password" className={'form-control' + (errors.confirmPassword && touched.confirmPassword ? ' is-invalid' : '')} />
                        <ErrorMessage name="confirmPassword" component="div" className="invalid-feedback" />

                        <div className="form-group form-check-control">
                        <Field type="checkbox" name="acceptTerms" id="acceptTerms" className={(errors.acceptTerms && touched.acceptTerms ? ' is-invalid' : '')} />
                        <label htmlFor="acceptTerms" className="form-check-label">&nbsp; Accept Terms & Conditions</label>
                            <ErrorMessage name="acceptTerms" component="div" className="invalid-feedback" />
                        </div>

                        <button type="submit" disabled={isSubmitting} className="btn btn-primary btn-block">
                                {isSubmitting && <span className="spinner-border spinner-border-sm mr-1"></span>}
                            <i className="fas fa-user-plus"></i> Sign Up
                        </button>

                        <Nav.Link className="btn btn-link" onClick={() => params.setName("signin")}>&#60;&#60; Back</Nav.Link>
                    </Form>
                </div>  
            )}
        </Formik>
    )
}

export default SignUp;