What is this?
-------------

This presentation holds the source code for the course on secure REST
API in ASP.NET Core, in C#.

This is Git branch `lab/6`, a refactored solution of branch `lab/5`
that shows one way to compose your classes to make testing and
responsibilities a little more clear, compared to the previous
branches, which has each focused on clarity of a specific point.

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
