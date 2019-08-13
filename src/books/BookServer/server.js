'use strict';

const PORT = 9000;

const PROTO_PATH = '../../../pb/books/book_messages.proto';

const mongodb = require('./mongodb');

const fs = require('fs');
const grpc = require('grpc');
const serviceDef = grpc.load(PROTO_PATH);

const cert = fs.readFileSync('../../../cert/ca.crt'),
      serverCert = fs.readFileSync('../../../cert/server.crt'),
      serverKey = fs.readFileSync('../../../cert/server.key'),
      certKeyPair = {
        'private_key': serverKey,
        'cert_chain': serverCert
      };
const creds = grpc.ServerCredentials.createSsl(cert, [certKeyPair]);
const server = new grpc.Server();
server.addService(serviceDef.BookService.service, {
    get: get,
});

server.bind(`0.0.0.0:${PORT}`, creds);
console.log(`Starting server on port ${PORT}`);
server.start();

function get (call, callback) {
    const md = call.metadata.getMap();
    for (let key in md) {
        console.log(`${key} : ${md[key]}`);
    }

    mongodb.get(call.request.id, call.request.isbn, function(err, data){
        console.log(data);
        callback(null, {book: data});
    });
};
