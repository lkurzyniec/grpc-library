using Grpc.Core;
using System.IO;
using System.Collections.Generic;

namespace HappyCode.GrpcSample.AuthorServer
{
    public static class SslCredentialsProvider
    {
        private static readonly string _path = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}/";

        private static readonly string _cert = File.ReadAllText(_path + "ca.crt");
        private static readonly string _serverCert = File.ReadAllText(_path + "server.crt");
        private static readonly string _serverKey = File.ReadAllText(_path + "server.key");
        private static readonly KeyCertificatePair _keyCertPair = new KeyCertificatePair(_serverCert, _serverKey);
        private static readonly SslServerCredentials _credentials = new SslServerCredentials(new List<KeyCertificatePair>
        {
            _keyCertPair
        }, _cert, false);

        public static SslServerCredentials Credentials => _credentials;
    }
}
