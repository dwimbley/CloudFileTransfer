using System;
using System.IO;
using System.Threading;
using CloudFileTransfer.Core;
using CommandLine;

namespace CloudFileTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();

            if (Parser.Default.ParseArguments(args, options))
            {
                options.Validate();

                var sourceprovider = new FileStorageFactory(options).Get(options.SourceProvider);
                var destinationprovider = new FileStorageFactory(options).Get(options.DestinationProvider);

                var transfer = new Transfer(sourceprovider, destinationprovider);

                var file = File.ReadLines(options.Filename);

                var counter = 1;
                foreach (var line in file)
                {
                    try
                    {
                        var fileargs = line.Split(',');

                        if (fileargs.Length < 2)
                        {
                            throw new CommandLineArgumentException(string.Format(@"Line {0} is not formatted correctly. {1}", counter, line));
                        }

                        var currentfolder = fileargs[0];
                        var destinationfolder = fileargs[1];
                        var filename = fileargs[2];
                        var tcounter = counter;

                        var thread = new Thread(() =>
                        {
                            transfer.Copy(options.SourceBucketName, options.DestinationBucketName, currentfolder,
                                destinationfolder, filename);
                        });

                        thread.Start();

                        counter += 1;
                    }
                    catch (ArgumentException aex)
                    {
                        Console.WriteLine(aex.Message);
                    }
                    catch (CommandLineArgumentException claex)
                    {
                        Console.WriteLine(claex.Message);
                    }
                }
            }
        }
    }
}
