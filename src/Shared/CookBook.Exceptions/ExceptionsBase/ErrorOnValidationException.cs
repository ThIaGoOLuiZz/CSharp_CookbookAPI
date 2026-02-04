using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : CookBookException
    {
        public IEnumerable<string> ErrorMessages { get; }
        public ErrorOnValidationException(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
