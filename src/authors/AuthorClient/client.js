'use strict';

const PORT = 9001;

const PROTO_PATH = '../../../pb/authors/author_messages.proto';

const fs = require('fs');
const process = require('process');
const grpc = require('grpc');
const serviceDef = grpc.load(PROTO_PATH);

const cert = fs.readFileSync('../../../cert/ca.crt'),
      clientCert = fs.readFileSync('../../../cert/client.crt'),
      clientKey = fs.readFileSync('../../../cert/client.key');;
const creds = grpc.credentials.createSsl(cert, clientKey, clientCert);
const client = new serviceDef.AuthorService(`LKURZYNIEC-WRO:${PORT}`, creds);

const option = parseInt(process.argv[2], 10);
switch (option) {
    case 1:
        sendMetadata(client);
        break;

    case 2:
        get(client);
        break;

    case 3:
        getWithFilter(client);
        break;
}

function sendMetadata (client) {
    const md = new grpc.Metadata();
    md.add('username', 'test_user_name');
    md.add('password', 'some_pass');

    client.get({}, md, function(){});
}

function get (client) {
    client.get({
        id: 2
    }, null, function(err, response){
        if (err){
            console.log(err);
        } else {
            console.log('Author: ', response.author);
        }
    });
}

function getWithFilter (client) {
    const call = client.getWithFilter({
        gender: 'MALE'
    });
    call.on('data', function(data){
        console.log('Authors: ', data.author);
    });
}
