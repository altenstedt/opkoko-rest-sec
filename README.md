What is this?
-------------

This directory holds the source code that was the result of the
presentation on Service API security at OpKoKo 17.2, Skövde.

Since the presentation, it has been expanded to contain a proper
identity service, that support the OAuth2 client_credentials grant
type , found in folder "IdentityService".  The code that we created in
the presentation, is found in folder "ProductsService".

## Run the code

To run the unit tests, open a terminal window and type:

```shell
cd Tests
dotnet test --filter Category=Unit
```

To run the system tests, you will first need a running product
service:


```shell
cd ProductsService
dotnet run
```

Note the host and port where the service starts (we will assume
http://localhost:5000 for the rest of these instructions.)

Update field `baseUri` in class `SystemTests` to point to the URI of
the running product service, and run the system tests:

```shell
cd Tests
dotnet test --filter Category=System
```
