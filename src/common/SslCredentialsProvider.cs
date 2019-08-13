using System.Collections.Generic;
using System.IO;
using Grpc.Core;

namespace HappyCode.GrpcSample.Common
{
    public static class SslCredentialsProvider
    {
        private static readonly string _path = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}/";

        private static readonly string _cacert = File.ReadAllText(_path + "ca.crt");
        private static readonly string _serverCert = File.ReadAllText(_path + "server.crt");
        private static readonly string _serverKey = File.ReadAllText(_path + "server.key");
        private static readonly string _clientCert = File.ReadAllText(_path + "client.crt");
        private static readonly string _clientKey = File.ReadAllText(_path + "client.key");
        private static readonly KeyCertificatePair _keyServerPair = new KeyCertificatePair(_serverCert, _serverKey);
        private static readonly KeyCertificatePair _keyClientPair = new KeyCertificatePair(_clientCert, _clientKey);
        private static readonly SslServerCredentials _serverCredentials = new SslServerCredentials(new List<KeyCertificatePair>
        {
            _keyServerPair
        }, _cacert, false);
        private static readonly SslCredentials _clientCredentials = new SslCredentials(_cacert, _keyClientPair);

        public static SslServerCredentials ServerCredentials => _serverCredentials;
        public static SslCredentials ClientCredentials => _clientCredentials;
    }
}
