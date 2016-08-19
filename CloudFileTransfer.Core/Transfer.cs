using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFileTransfer.Core
{
    public class Transfer
    {
        private readonly IFileStore _source = null;
        private readonly IFileStore _destination = null;

        public Transfer(IFileStore source, IFileStore destination)
        {
            this._source = source;
            this._destination = destination;
        }
    }
}
