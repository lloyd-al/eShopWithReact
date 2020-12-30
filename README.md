# eShopWithReact
Build e-commerce website using ASP.NET core and ReactJS based on Microservice Architecture.
Other tool stack include Docker, RabbitMQ, Ocelot API Gateway, MongoDB, Redis, SqlServer.

## Web/Shop - Frontend application built using ReactJS
State Management using Redux, Thunk, Saga, Reselect, Hooks \
Local Storage using Persist \
API Calls using Axios \
Logging using Logger

## Services
APIs build based on Onion Architecture\
Authentication using IdentityServer4

![plot](./src/Web/Shop/ClientApp/src/assets/eShop-Website.PNG)


## MongoDB Set-up for the Catalog Database 


` docker pull mongo `

` docker run -d --name mongodb  -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=secret mongo `

```
-d                                       - runs the container in background;
--name mongodb                           - defines a friendly name for the container;
-p 27888:27017                           - declares that the local port 27888 is mapped to the internal 27017 port;
-e MONGO_INITDB_ROOT_USERNAME=mongoadmin - sets the root username (-e sets the environment variables);
-r MONGO_INITDB_ROOT_PASSWORD=secret     - sets the root password;
mongo                                    - name of the image to run;
```

` docker exec -it mongodb bash `

` mongo --host localhost -u mongoadmin -p secret --authenticationDatabase admin `

#
Following packages need to be installed in each project


**Web/Shop**

>npm install --save react-bootstrap

>npm install --save redux react-redux redux-logger redux-thunk

>npm install --save reselect

>npm install --save redux-persist

>npm install --save redux-saga

>npm install --save axios

>npm install --save react-stripe-checkout

#

