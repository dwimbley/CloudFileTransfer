using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace CloudFileTransfer.Core
{
    public class Options
    {
        [ParserState]
        public IParserState LastParserState { get; set; }

        #region Amazon Args

        [Option("aws-access-key", HelpText = "Amazon Web Services Access Key")]
        public string AwsAccessKey { get; set; }

        [Option("aws-secret-key", HelpText = "Amazon Web Services Secret Key")]
        public string AwsSecretKey { get; set; }

        [Option("aws-dest-access-key", HelpText = "Destination Amazon Web Services Access Key")]
        public string AwsDestinationAccessKey { get; set; }

        [Option("aws-dest-secret-key", HelpText = "Destination Amazon Web Services Secret Key")]
        public string AwsDestinationSecretKey { get; set; }

        [Option("aws-endpoint", HelpText = "Amazon Web Services Endpoint")]
        public string AwsSourceEndpoint { get; set; }

        [Option("aws-dest-endpoint", HelpText = "Amazon Web Services Destination Endpoint")]
        public string AwsDestinationEndpoint { get; set; }

        #endregion

        #region Google Args

        

        [Option("google-clientid", HelpText = "Google API Client Id")]
        public string GoogleClientIdKey { get; set; }

        [Option("google-secret-key", HelpText = "Google API Secret Key")]
        public string GoogleSecretKey { get; set; }

        [Option("google-dest-clientid", HelpText = "Destination Google API Client Id")]
        public string GoogleDestinationClientId { get; set; }

        [Option("google-dest-secret-key", HelpText = "Destination Google API Secret Key")]
        public string GoogleDestinationSecretKey { get; set; }

        #endregion

        [Option("source-bucket", HelpText = "Google Cloud Storage source bucket name")]
        public string SourceBucketName { get; set; }

        [Option("dest-google-bucket", HelpText = "Google Cloud Storage destination bucket name")]
        public string DestinationBucketName { get; set; }

        [Option("source-provider", HelpText = "Source cloud storage provider name", Required = true)]
        public string SourceProvider { get; set; }

        [Option("dest-provider", HelpText = "Destination cloud storage provider name", Required = true)]
        public string DestinationProvider { get; set; }
        
        [Option('f', "file", HelpText = "CSV s3 file names, full source and destination directory paths", Required = true)]
        public string Filename { get; set; }

        [Option('v', "verbose", DefaultValue = false, HelpText = "Execute in verbose mode")]
        public bool Verbose { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        public void Validate()
        {
            
        }
    }
}
