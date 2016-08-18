using System;
using CloudFileTransfer.Amazon;
using CloudFileTransfer.Core;
using CloudFileTransfer.Google;
using NUnit.Framework;

namespace CloudFileTransfer.Tests
{
    [TestFixture]
    public class CloudFileTransferTests
    {
        [Test]
        public void Google_Source_Provider_Test()
        {
            var provider = new FileStorageFactory(new Options()).Get("source-google");
            Assert.IsInstanceOf<GoogleCloudStorage>(provider);
        }

        [Test]
        public void Google_Destination_Provider_Test()
        {
            var provider = new FileStorageFactory(new Options()).Get("dest-google");
            Assert.IsInstanceOf<GoogleCloudStorage>(provider);
        }

        [Test]
        public void Amazon_Source_Provider_Test()
        {
            var provider = new FileStorageFactory(new Options { AwsSourceEndpoint = "us-east-1" }).Get("source-aws");
            Assert.IsInstanceOf<S3>(provider);
        }

        [Test]
        public void Amazon_Destination_Provider_Test()
        {
            var provider = new FileStorageFactory(new Options { AwsDestinationEndpoint = "us-east-1" }).Get("dest-aws");
            Assert.IsInstanceOf<S3>(provider);
        }

        [Test]
        public void Provider_Not_Supported_Test()
        {
            Assert.Throws<NotImplementedException>(() => new FileStorageFactory(new Options()).Get("davids-storage"));
        }
    }
}
