using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meow.Code.Model
{
    public class AccountCreationException : Exception
    {
        public AccountCreationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AccountCreationException(string message) : base(message) { }
    }
}