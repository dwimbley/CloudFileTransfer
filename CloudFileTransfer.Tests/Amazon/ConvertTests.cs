using System;
using Amazon;
using CloudFileTransfer.Amazon;
using NUnit.Framework;
using Convert = CloudFileTransfer.Amazon.Convert;

namespace CloudFileTransfer.Tests.Amazon
{
    [TestFixture]
    public class ConvertTests
    {
        [Test]
        public void AwsEndpoint_ToRegionEndpoint_Test()
        {
            var usEast1 = Convert.ToRegionEndpoint(AwsEndpoint.USEast1);
            var uSWest1 = Convert.ToRegionEndpoint(AwsEndpoint.USWest1);
            var uSWest2 = Convert.ToRegionEndpoint(AwsEndpoint.USWest2);
            var eUWest1 = Convert.ToRegionEndpoint(AwsEndpoint.EUWest1);
            var euCentral1 = Convert.ToRegionEndpoint(AwsEndpoint.EUCentral1);
            var apNortheast1 = Convert.ToRegionEndpoint(AwsEndpoint.APNortheast1);
            var apNortheast2 = Convert.ToRegionEndpoint(AwsEndpoint.APNortheast2);
            var apSoutheast1 = Convert.ToRegionEndpoint(AwsEndpoint.APSoutheast1);
            var apSoutheast2 = Convert.ToRegionEndpoint(AwsEndpoint.APSoutheast2);
            var apSouth1 = Convert.ToRegionEndpoint(AwsEndpoint.APSouth1);
            var saEast1 = Convert.ToRegionEndpoint(AwsEndpoint.SAEast1);
            var usGovCloudWest1 = Convert.ToRegionEndpoint(AwsEndpoint.USGovCloudWest1);
            var cnNorth1 = Convert.ToRegionEndpoint(AwsEndpoint.CNNorth1);

            Assert.AreEqual(RegionEndpoint.USEast1, usEast1);
            Assert.AreEqual(RegionEndpoint.USWest1, uSWest1);
            Assert.AreEqual(RegionEndpoint.USWest2, uSWest2);
            Assert.AreEqual(RegionEndpoint.EUWest1, eUWest1);
            Assert.AreEqual(RegionEndpoint.EUCentral1, euCentral1);
            Assert.AreEqual(RegionEndpoint.APNortheast1, apNortheast1);
            Assert.AreEqual(RegionEndpoint.APNortheast2, apNortheast2);
            Assert.AreEqual(RegionEndpoint.APSoutheast1, apSoutheast1);
            Assert.AreEqual(RegionEndpoint.APSoutheast2, apSoutheast2);
            Assert.AreEqual(RegionEndpoint.APSouth1, apSouth1);
            Assert.AreEqual(RegionEndpoint.SAEast1, saEast1);
            Assert.AreEqual(RegionEndpoint.USGovCloudWest1, usGovCloudWest1);
            Assert.AreEqual(RegionEndpoint.CNNorth1, cnNorth1);
        }

        [Test]
        public void String_ToRegionEndpoint_Test()
        {
            var usEast1 = Convert.ToRegionEndpoint("us-east-1");
            var uSWest1 = Convert.ToRegionEndpoint("us-west-1");
            var uSWest2 = Convert.ToRegionEndpoint("us-west-2");
            var eUWest1 = Convert.ToRegionEndpoint("eu-west-1");
            var euCentral1 = Convert.ToRegionEndpoint("eu-central-1");
            var apNortheast1 = Convert.ToRegionEndpoint("ap-northeast-1");
            var apNortheast2 = Convert.ToRegionEndpoint("ap-northeast-2");
            var apSoutheast1 = Convert.ToRegionEndpoint("ap-southeast-1");
            var apSoutheast2 = Convert.ToRegionEndpoint("ap-southeast-2");
            var apSouth1 = Convert.ToRegionEndpoint("ap-south-1");
            var saEast1 = Convert.ToRegionEndpoint("sa-east-1");
            var usGovCloudWest1 = Convert.ToRegionEndpoint("us-gov-west-1");
            var cnNorth1 = Convert.ToRegionEndpoint("cn-north-1");
                
            Assert.AreEqual(RegionEndpoint.USEast1, usEast1);
            Assert.AreEqual(RegionEndpoint.USWest1, uSWest1);
            Assert.AreEqual(RegionEndpoint.USWest2, uSWest2);
            Assert.AreEqual(RegionEndpoint.EUWest1, eUWest1);
            Assert.AreEqual(RegionEndpoint.EUCentral1, euCentral1);
            Assert.AreEqual(RegionEndpoint.APNortheast1, apNortheast1);
            Assert.AreEqual(RegionEndpoint.APNortheast2, apNortheast2);
            Assert.AreEqual(RegionEndpoint.APSoutheast1, apSoutheast1);
            Assert.AreEqual(RegionEndpoint.APSoutheast2, apSoutheast2);
            Assert.AreEqual(RegionEndpoint.APSouth1, apSouth1);
            Assert.AreEqual(RegionEndpoint.SAEast1, saEast1);
            Assert.AreEqual(RegionEndpoint.USGovCloudWest1, usGovCloudWest1);
            Assert.AreEqual(RegionEndpoint.CNNorth1, cnNorth1);
        }

        [Test]
        public void String_ToRegionEndpoint_NotSupported_Test()
        {
            Assert.Throws<NotImplementedException>(() => Convert.ToRegionEndpoint("david"));
        }

    }
}
