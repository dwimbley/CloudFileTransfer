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

        [Option("source-aws-bucket", HelpText = "AWS S3 source bucket name")]
        public string AwsBucketName { get; set; }

        [Option("dest-aws-bucket", HelpText = "AWS S3 destination bucket name")]
        public string AwsDestinationBucketName { get; set; }

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

        #endregion

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
