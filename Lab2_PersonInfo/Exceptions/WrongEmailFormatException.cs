using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_PersonInfo.Exceptions
{
    class WrongEmailFormatException : Exception
    {
        public WrongEmailFormatException() :
            base("The email entered has a wrong format.")
        {

        }

        public WrongEmailFormatException(string message) :
            base(message)
        {
            
        }
    }
}
