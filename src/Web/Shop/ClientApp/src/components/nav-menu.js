import React from 'react';
import { Navbar, Nav, Form, FormControl, Button } from 'react-bootstrap'
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';

import CartIcon from './cart/cart-icon';
import CartDropdown from './cart/cart-dropdown';
import { selectCartHidden } from '../redux/cart/cart-selectors';

import { ReactComponent as Logo } from '../assets/stop-shop.svg';

import './nav-menu-style.css';

const NavMenu = ({ hidden }) => {
    return (
        <div>
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
                        <Nav.Link href="#link">Sign-In</Nav.Link>
                        <CartIcon />
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
            {hidden ? null : <CartDropdown />}
        </div>
    );
}

const mapStateToProps = createStructuredSelector({
    hidden: selectCartHidden
});

export default connect(mapStateToProps)(NavMenu);
