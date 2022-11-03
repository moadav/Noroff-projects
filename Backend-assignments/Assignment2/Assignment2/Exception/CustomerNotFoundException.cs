using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Exception
{
    public class CustomerNotFoundException : System.Exception
    {

        /// <summary>Initializes a new instance of the <see cref="CustomerNotFoundException" /> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public CustomerNotFoundException(string message)
       : base(message)
        {
        }
    }
}
