# grpc-library

Some gRPC simple implementation

## mongodb

```bash
docker run -d -p 27017-27019:27017-27019 --name mongodb mongo:latest
```

## generate certificates

```bash
cert\_generate.cmd
```

## generate messages from proto files

### authors

```bash
pb\authors.cmd
```

### books

1. generate C# messages

    ```bash
    pb\books.cmd
    ```
2. comment C# import, uncomment JS import in [`book_messages.proto`](pb/books/book_messages.proto) file (to make it works with JS server)

    ```
    // C#
    import "author_messages.proto";
    import "protobuf/timestamp.proto";

    // JS
    //import "../authors/author_messages.proto";
    //import "../google/protobuf/timestamp.proto";
    ```

    into

    ```
    // C#
    //import "author_messages.proto";
    //import "protobuf/timestamp.proto";

    // JS
    import "../authors/author_messages.proto";
    import "../google/protobuf/timestamp.proto";
    ```

## install npm dependencies

### authors

```bash
cd src\authors\AuthorClient
npm install
```

### books

```bash
cd src\books\BookServer
npm install
```

## run

### authors

#### server

```bash
cd src\authors\AuthorServer
dotnet run
```

#### client

```bash
cd src\authors\AuthorClient
node client.js 2
```

### books

#### server

```bash
cd src\books\BookServer
node server.js
```

#### client

```bash
cd src\books\BookClient
dotnet run
```