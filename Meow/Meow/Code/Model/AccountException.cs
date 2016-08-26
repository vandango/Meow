using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meow.Code.Model
{
    public class AccountException : Exception
    {
        public AccountException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}