What is this?
-------------

This presentation holds the source code for the course on secure REST
API in ASP.NET Core, in C#.

This is Git branch `lab/1`, an excersice to secure the REST API
declared in class `ProductsController`.

## Run the code

Open a terminal and start the products service:

```shell
cd ProductsService
dotnet run --server.urls=http://localhost:5000
```

You can now verify that you will get a 200 and product response from
the products service:

```
GET http://localhost:5000/products HTTP/1.1
```

Update class `Startup` in the products service to require user
authentication and authorization using JWT.

Open a second terminal window and start the identity service:

```shell
cd IdentityService
dotnet run --server.urls=http://localhost:4000
```

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

Enable local claims transformation using class `ClaimsTransformation`,
set a breakpoint and inspect the incoming and outgoing
`ClaimsPrincipal` object.