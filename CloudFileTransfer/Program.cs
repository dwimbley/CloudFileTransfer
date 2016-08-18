using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            }
        }
    }
}
