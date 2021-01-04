import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { HomePage, ShopPage, CheckoutPage } from './pages';

import {AuthenticationService} from './services/auth-service'

import './app-style.css';

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);

        this.state = {
            currentUser: null
        };
    }

    componentDidMount() {
        AuthenticationService.currentUser.subscribe(x => this.setState({ currentUser: x }));
    }

    logout() {
        AuthenticationService.logout();
    }

  render () {
    return (
      <Layout>
            <Route exact path='/' component={HomePage} />
            <Route path='/shop' component={ShopPage} />
            <Route exact path='/checkout' component={CheckoutPage} />
      </Layout>
    );
  }
}
