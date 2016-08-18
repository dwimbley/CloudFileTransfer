using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFileTransfer.Core
{
    public class Transfer
    {
        private IFileStore _sourrce = null;
        private IFileStore _destination = null;

        public Transfer(IFileStore source, IFileStore destination)
        {
            
        }
    }
}
