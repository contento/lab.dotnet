# README 

A set of samples on how to use X509 certificates, symetric encryption and asymetric encryption in .NET and .NET Framework.

> Use [certificate.pfx](./files/certificate.pfx) for the samples below 

## Generating a Self-Sign certificate + Private Key with openssl

``` shell
# Generate certificate + public key
openssl req -x509 -newkey rsa:4096 -keyout key.pem -out certificate.cer -sha256 -days 365 -subj "/C=US/OU=web/CN=api.example.com" -nodes
# Convert to PFX format if you need to move it around (PRIVATE key included)
openssl pkcs12 -export -in certificate.cer -inkey key.pem -out certificate.pfx 
# Export public key
openssl rsa -in key.pem -pubout -out key.pub
# Extract public certificate
openssl pkcs12 -in certificate.pfx -clcerts -nokeys -out certificate-pub.cer
```

## Install openssl [Optional]

``` shell
winget install openssl
# or with other package managers
# scoop install openssl
# choco install openssl

# Important! 
# You may need to add to the environment variable %PATH% ($Env:Path) the path to openssl, for example:
# "C:\Program Files\OpenSSL-Win64\bin"
```