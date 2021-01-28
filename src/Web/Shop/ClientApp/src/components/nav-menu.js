import React, { useEffect, useState} from 'react';
import { Navbar, Nav, Form, FormControl, Button, Modal } from 'react-bootstrap'
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';

import CartIcon from './cart/cart-icon';
import CartDropdown from './cart/cart-dropdown';
import AuthPage from '../pages/auth/auth-page';
import { selectCartHidden } from '../redux/cart/cart-selectors';
import { AuthenticationService } from '../services/auth-service';

import { ReactComponent as Logo } from '../assets/stop-shop.svg';

import './nav-menu-style.css';


const NavMenu = ({ hidden }) => {
    const [show, setShow] = useState(false);
    const [user, setUser] = useState();
    const [fullName, setFullName] = useState();

    useEffect(() => {
        setUser(AuthenticationService.userValue);
        if (user) {
            setFullName(`${user.firstName} ${user.lastName}`);
        }
    }, [user, fullName]);



    const handleClose = () => {
        setUser(AuthenticationService.userValue);
        setShow(false);
    }

    const handleShow = () => setShow(true);

    const logout = () => {
        setUser(null);
        setFullName(null);
        AuthenticationService.logout();
    }

    return (
        <>
            <Navbar className="bg-light header" expand="lg">
                <Navbar.Brand href="/" className="nav-start"><Logo /></Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Form inline className="nav-middle">
                        <FormControl type="text" placeholder="Search" className="mr-sm-2" />
                        <Button variant="outline-success">Search</Button>
                    </Form>
                    <Nav className="nav-last">
                        <Nav.Link href="/shop">Shop</Nav.Link>
                        {fullName 
                            ? <Nav.Link href="#" onClick={logout}>{fullName}</Nav.Link>
                            : <Nav.Link href="#" onClick={handleShow}>Sign In</Nav.Link>
                        }
                        <CartIcon />
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
            {hidden ? null : <CartDropdown />}
            <Modal show={show} onHide={handleClose} backdrop="static">
                <Modal.Body>
                    <AuthPage handleClose={handleClose}/>
                </Modal.Body>
            </Modal>
        </>
    );
}

const mapStateToProps = createStructuredSelector({
    hidden: selectCartHidden
});

export default connect(mapStateToProps)(NavMenu);
