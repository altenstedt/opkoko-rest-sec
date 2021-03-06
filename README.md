What is this?
-------------

This presentation holds the source code for the course on secure REST
API in ASP.NET Core, in C#.

There are a number of branches, each representing the different labs
that comprise the course:

  * `lab/1` Authentication & Authorization
  * `lab/2` Error handling
  * `lab/3` Test driven development
  * `lab/4` Input data validation
  * `lab/5` HTTP security related headers
  * `lab/6` Refactor into domain application layer
  
To checkout a branch, you can run the git `checkout` command, for
example: `git checkout lab/4` to checkout the branch for the input
data validation.

## Run the code

To run the sample, open two terminal windows.  In the first, start the
identity service:

```shell
cd IdentityService
dotnet run --server.urls=http://localhost:4000
```

If you choose a different port than 4000, you will need to update the
Authority URL in class Startup to point to your identity service.  In
the second terminal, start the products service:

```shell
cd ProductsService
dotnet run --server.urls=http://localhost:5000
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

## Miscellaneous

The folder `IdentityService` contains a service built on the
[IdentityServer][3] NuGet package, which we feel is the de-facto
standard for building your own identity service.

We can select the port using command line argument `--server.urls`
above since we use [AddCommandLine][1] in class `Program`.

There are many ways to execute an HTTP query.  For example, you can
use Postman, Powershell, curl.  These days, we tend to prefer the
Visual Studio Code extension [rest-client][2].

## The Token Service project

The folder `TokenService` contains an extremely basic identity service
built from scratch.  We include it just as a sample of how you can
create and sign your own JWT tokens, which you might find to be
illuminating for understanding JWT tokens.  The service use a
certificate to sign JWT tokens, and the code assumes that it is has a
Subject (Distinguished Name) of "REST Sec OAuth2 Identity" and is
placed in store CurrentUser.

On Windows, you can create a signing certificate using the following
PowerShell command:

```powershell
New-SelfSignedCertificate `
  -CertStoreLocation cert:\currentuser\my `
  -Provider "Microsoft Enhanced RSA and AES Cryptographic Provider" `
  -Subject "CN=REST Sec OAuth2 Identity" `
  -FriendlyName "OAuth2 token signing for REST Sec course" `
  -Type CodeSigningCert `
  -KeyExportPolicy Exportable `
  -KeyLength 4096 `
  -NotAfter (Get-Date).AddYears(1) `
  -HashAlgorithm SHA256
```

To run the token service, open a terminal windows:

```shell
cd TokenService
dotnet run --server.urls=http://localhost:4001
```

Get a token from the token service:

```http
POST http://localhost:4001/token
```

If you want to the product service to use this token, you can use
branch `feature/token-service` and see how you would make changes in
class `Startup` and use the JWKs endpoint to get the token signing
keys.

[1]: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?tabs=basicconfiguration#commandline-configuration-provider
[2]: https://marketplace.visualstudio.com/items?itemName=humao.rest-client
[3]: https://github.com/IdentityServer/IdentityServer4
