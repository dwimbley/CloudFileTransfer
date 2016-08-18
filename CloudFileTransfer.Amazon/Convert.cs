using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;

namespace CloudFileTransfer.Amazon
{
    public class Convert
    {
        public static RegionEndpoint ToRegionEndpoint(AwsEndpoint value)
        {
            switch (value)
            {
                case AwsEndpoint.USEast1:
                    return RegionEndpoint.USEast1;
                case AwsEndpoint.USWest1:
                    return RegionEndpoint.USWest1;
                case AwsEndpoint.USWest2:
                    return RegionEndpoint.USWest2;
                case AwsEndpoint.EUWest1:
                    return RegionEndpoint.EUWest1;
                case AwsEndpoint.EUCentral1:
                    return RegionEndpoint.EUCentral1;
                case AwsEndpoint.APNortheast1:
                    return RegionEndpoint.APNortheast1;
                case AwsEndpoint.APNortheast2:
                    return RegionEndpoint.APNortheast2;
                case AwsEndpoint.APSoutheast1:
                    return RegionEndpoint.APSoutheast1;
                case AwsEndpoint.APSoutheast2:
                    return RegionEndpoint.APSoutheast2;
                case AwsEndpoint.APSouth1:
                    return RegionEndpoint.APSouth1;
                case AwsEndpoint.SAEast1:
                    return RegionEndpoint.SAEast1;
                case AwsEndpoint.USGovCloudWest1:
                    return RegionEndpoint.USGovCloudWest1;
                case AwsEndpoint.CNNorth1:
                    return RegionEndpoint.CNNorth1;
                default:
                    throw new NotImplementedException(string.Format("Endoint {0} not supported", value));
            }
        }

        public static RegionEndpoint ToRegionEndpoint(string value)
        {
            var sanitize = value.ToLower().Trim();
            switch (sanitize)
            {
                case "us-east-1":
                case "useast1":
                    return RegionEndpoint.USEast1;
                case "us-west-1":
                case "uswest1":
                    return RegionEndpoint.USWest1;
                case "us-west-2":
                case "uswest2":
                    return RegionEndpoint.USWest2;
                case "eu-west-1":
                case "euwest1":
                    return RegionEndpoint.EUWest1;
                case "eu-central-1":
                case "eucentral1":
                    return RegionEndpoint.EUCentral1;
                case "ap-northeast-1":
                case "apnortheast1":
                    return RegionEndpoint.APNortheast1;
                case "ap-northeast-2":
                case "apnortheast2":
                    return RegionEndpoint.APNortheast2;
                case "ap-southeast-1":
                case "apsoutheast1":
                    return RegionEndpoint.APSoutheast1;
                case "ap-southeast-2":
                case "apsoutheast2":
                    return RegionEndpoint.APSoutheast2;
                case "ap-south-1":
                case "apsouth1":
                    return RegionEndpoint.APSouth1;
                case "sa-east-1":
                case "saeast1":
                    return RegionEndpoint.SAEast1;
                case "us-gov-west-1":
                case "usgovwest1":
                    return RegionEndpoint.USGovCloudWest1;
                case "cn-north-1":
                case "cnnorth1":
                    return RegionEndpoint.CNNorth1;
                default:
                    throw new NotImplementedException(string.Format("Endoint {0} not supported", value));
            }
        }
    }
}
