@echo off
set OPENSSL_PATH=c:\OpenSSL-Win64\bin\openssl.exe

echo Generate CA key:
%OPENSSL_PATH% genrsa -passout pass:1235 -des3 -out ca.key 4096

echo Generate CA certificate:
%OPENSSL_PATH% req -passin pass:1235 -new -x509 -days 365 -key ca.key -out ca.crt -subj  "/C=US/ST=CA/L=Cupertino/O=HappyCode/OU=GrpcSample/CN=MyRootCA"

echo Generate server key:
%OPENSSL_PATH% genrsa -passout pass:1235 -des3 -out server.key 4096

echo Generate server signing request:
%OPENSSL_PATH% req -passin pass:1235 -new -key server.key -out server.csr -subj  "/C=US/ST=CA/L=Cupertino/O=HappyCode/OU=GrpcSample/CN=%COMPUTERNAME%"

echo Self-sign server certificate:
%OPENSSL_PATH% x509 -req -passin pass:1235 -days 365 -in server.csr -CA ca.crt -CAkey ca.key -set_serial 01 -out server.crt

echo Remove passphrase from server key:
%OPENSSL_PATH% rsa -passin pass:1235 -in server.key -out server.key

echo Generate client key
%OPENSSL_PATH% genrsa -passout pass:1235 -des3 -out client.key 4096

echo Generate client signing request:
%OPENSSL_PATH% req -passin pass:1235 -new -key client.key -out client.csr -subj  "/C=US/ST=CA/L=Cupertino/O=HappyCode/OU=GrpcSample/CN=%CLIENT-COMPUTERNAME%"

echo Self-sign client certificate:
%OPENSSL_PATH% x509 -passin pass:1235 -req -days 365 -in client.csr -CA ca.crt -CAkey ca.key -set_serial 01 -out client.crt

echo Remove passphrase from client key:
%OPENSSL_PATH% rsa -passin pass:1235 -in client.key -out client.key
