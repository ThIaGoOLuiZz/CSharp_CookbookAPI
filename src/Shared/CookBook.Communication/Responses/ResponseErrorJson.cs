using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Communication.Responses
{
    public class ResponseErrorJson
    {
        public IEnumerable<string> Errors { get; set; }

        public ResponseErrorJson(IEnumerable<string> _errors) => Errors = _errors;
        public ResponseErrorJson(string error)
        {
            Errors = new List<string> 
            { 
                error
            };
        }
    }
}
