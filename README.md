What is this?
-------------


This presentation holds the source code for the course on secure REST
API in ASP.NET Core, in C#.

This is Git branch `lab/2`, an excersice on how to handle errors in a
secure and scaleable manner.  The code on the tip of this branch
represents the intended end result of this excersice.

## Run the code

Open a terminal and start the products service:

```shell
cd ProductsService
dotnet run --server.urls=http://localhost:5000
```

Verify that you get a 500 response from the error resource:

```
GET http://localhost:5000/error HTTP/1.1
```

If you enable the developer exception page, you will get a lot of
error details, if you do not, the response will be empty.  You should
never use `UseDeveloperExceptionPage` for production use since this
_will_ leak sensitive information to a potential attacker.
