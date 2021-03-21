using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tema1CNAServer.Exceptions
{
    [Serializable]
    public class InvalidPersonCnpException : Exception
    {
        public InvalidPersonCnpException()
        {

        }

        public InvalidPersonCnpException(string message) : base(message)
        {

        }

    }
}
