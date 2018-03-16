What is this?
-------------

This directory holds the source code that was the result of the
presentation on Service API security at OpKoKo 17.2, Skövde.

Since the presentation, it has been expanded to contain a proper
identity service, that support the OAuth2 client_credentials grant
type , found in folder "IdentityService".  The code that we created in
the presentation, is found in folder "ProductsService".

## Run the code

To run the sample, open two terminal windows.  In the first, start the
product service:

```shell
cd ProductsService
dotnet run
```

Note the host and port where the service starts (we will assume
http://localhost:5000 for the rest of these instructions.)

You can now verify that you will get a 200 and product response from
the products service:

```
GET http://localhost:5000/products HTTP/1.1
```

Update class `Startup` in the products service to require user
authentication and authorization using JWT.

Start the identity service:

```shell
cd IdentityService
dotnet run 
```

Note the host and port where the identity service starts (we will
assume http://localhost:4000 for the rest of these instructions).

Update the Authority URL in class `Startup` of the product service to
point to your identity service.

Verify that you get a 401 from the products service:

```
GET http://localhost:5000/products HTTP/1.1
```

Get an access token from the token endpoint:

```
POST http://localhost:4000/connect/token
Content-Type: application/x-www-form-urlencoded

client_id=myclient&client_secret=secret&scope=read:product&grant_type=client_credentials
```

And use the returned access token on the products endpoint:

```
GET http://localhost:5000/products HTTP/1.1
Authorization: bearer <paste your access token here>
```
