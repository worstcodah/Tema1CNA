using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tema1CNAServer.Exceptions
{
    [Serializable]
    public class InvalidPersonNameException : Exception
    {
        public InvalidPersonNameException()
        {

        }

        public InvalidPersonNameException(string name): base(String.Format("Numele persoanei este invalid: {0}", name))
        {

        }

    }
}
