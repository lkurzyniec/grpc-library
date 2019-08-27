const MongoClient = require('mongodb').MongoClient;

const url = 'mongodb://localhost:27017';
const dbName = 'grpc-library';

// Create a new MongoClient
const client = new MongoClient(url);
var db = null;

// Use connect method to connect to the Server
client.connect(function (err) {
    if (err) {
        console.log(err);
        throw new Error('unable to connect to DB');
    }
    console.log("Connected successfully to DB");

    db = client.db(dbName);

    const books = db.collection('books');
    books.countDocuments({}, function (err, count) {
        if (err) {
            throw err;
        }
        if (count < 1) {
            books.insertMany([
                {
                    id: 1,
                    isbn: '123',
                    title: 'Book title 1',
                    authorId: 1,
                },
                {
                    id: 2,
                    isbn: '456',
                    title: 'Book title 2',
                    authorId: 2,
                },
                {
                    id: 3,
                    isbn: '789',
                    title: 'Book title 3',
                    authorId: 1,
                }
            ], (err, result) => {
                if (err) {
                    throw err;
                }
                //console.log(result);
            });
        }
    });

    const authors = db.collection('authors');
    authors.countDocuments({}, function (err, count) {
        if (err) {
            throw err;
        }
        if (count < 1) {
            authors.insertMany([
                {
                    id: 1,
                    full_name: 'John Smith',
                    gender: 'MALE',
                },
                {
                    id: 2,
                    full_name: 'Ann Lee',
                    gender: 'FEMALE',
                },
            ], (err, result) => {
                if (err) {
                    throw err;
                }
                //console.log(result);
            });
        }
    });

    //client.close();
});

const get = (id, isbn, callback) => {
    var books = db.collection('books');
    const query = {
        $or: [
            { id: id },
            { isbn: isbn },
        ]
    };
    books.findOne(query)
        .then(book => {
            if (!book) {
                callback(null, null);
                return;
            }

            delete book._id;

            var authors = db.collection('authors');
            authors.findOne({id:book.authorId})
                .then(author => {
                    delete book.authorId;
                    if (!author) {
                        callback(null, book);
                        return;
                    }

                    delete author._id;
                    book.author_obj = author;
                    callback(null, book);
                });
        });
}
module.exports.get = get;
