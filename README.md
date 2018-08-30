What is this?
-------------

This presentation holds the source code for the course on secure REST
API in ASP.NET Core, in C#.

This particular branch is a sample branch that shows how you can use
the Token Service to create JWTs and how to validate them in the
product service.

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

Then you can use the access token in a request to the product service:

```http
GET http://localhost:5000/products
Authorization: bearer <access token>
```
