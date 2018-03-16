What is this?
-------------

This directory holds the source code that was the result of the
presentation on Service API security at OpKoKo 17.2, Skövde.

Since the presentation, it has been expanded to contain a proper
identity service, that support the OAuth2 client_credentials grant
type , found in folder "IdentityService".  The code that we created in
the presentation, is found in folder "ProductsService".

## Run the code

To run the sample, open a terminal window and start the product service:

```shell
cd ProductsService
dotnet run
```

Note the host and port where the service starts (we will assume
http://localhost:5000 for the rest of these instructions.)

Verify that you get a 500 response from the error resource:

```
GET http://localhost:5000/error HTTP/1.1
```

If you do enable the developer exception page, you will get a lot of
error details, if you do not, the response will be empty.  You should
never use `UseDeveloperExceptionPage` for production use since this
_will_ leak sensitive information to a potential attacker.