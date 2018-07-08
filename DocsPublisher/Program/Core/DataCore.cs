using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsPublisher.Program.Core
{
    class DataCore : DBCore
    {
        public DataCore(string serverName = "STAGE") : base(serverName)
        {

        }
    }
}
