## Create the certificate 
The following steps will guide you through creating a self-signed certificate for testing purposes.

### Generate a Private Key:

``` bash
openssl genpkey -algorithm RSA -out client.key -pkeyopt rsa_keygen_bits:2048
```
### Create a Certificate Signing Request (CSR):

``` bash
openssl req -new -key client.key -out client.csr -subj "/CN=MyClientCertificate"
```
Replace /CN=MyClientCertificate with your desired Common Name.

### Generate a Self-Signed Certificate:

``` bash
openssl x509 -req -days 365 -in client.csr -signkey client.key -out client.crt
```
### Create a .pfx File:

``` bash
openssl pkcs12 -export -out client.pfx -inkey client.key -in client.crt -passout pass:password
```

Replace password with your desired password.