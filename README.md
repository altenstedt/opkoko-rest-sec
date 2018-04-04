What is this?
-------------

This presentation holds the source code for the course on secure REST
API in ASP.NET Core, in C#.

This is Git branch `lab/3`, an excersice to use tests to verify the
security and function of a secure REST API.  The tip of this branch is
the starting point of the excersice.  You can roll back the last
commit to see the intended end result of the lab.

## Run the code

To run the unit tests, open a terminal window and type:

```shell
cd Tests
dotnet test --filter Category=Unit
```

To run the system tests, you will need both a running identity and
product service.  Open two terminal windows and start the services:

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

Run the system tests in a separate terminal window:

```shell
cd Tests
dotnet test --filter Category=System
```
