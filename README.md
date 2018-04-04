What is this?
-------------

This presentation holds the source code for the course on secure REST
API in ASP.NET Core, in C#.

This is Git branch `lab/4`, an excersice on how to validate input
data.  The code on the tip of this branch represents the intended end
result of this excersice.

## Run the code

Open two terminal windows and start the product and identity service:

```shell
cd ProductsService
dotnet run --server.urls=http://localhost:5000
```

```shell
cd IdentityService
dotnet run --server.urls=http://localhost:4000
```

If you are running the services from an IDE, like Visual Studio for
Windows, instead of from a terminal, you need to configure the ports
in that IDE.  Each IDE works a little differently, but Google is
probably your friend.

You can now first verify that you will get a 401 from the products
service:

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
