using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Common.com.pe.mgr.common.constants
{
    public class MgrServiceException : Exception
    {
        private string v;

        public MgrServiceException()
        {
        }

        public MgrServiceException(string message) : base(message)
        {
        }

        public MgrServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MgrServiceException(string message, string v) : base(message)
        {
            this.v = v;
        }

        protected MgrServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
